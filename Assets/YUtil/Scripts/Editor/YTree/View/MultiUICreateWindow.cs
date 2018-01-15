using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class MultiUICreateWindow : EditorWindow {
    [NonSerialized] bool m_Initialized;
    [SerializeField] TreeViewState m_TreeViewState; // Serialized in the window layout file so it survives assembly reloading
    [SerializeField] MultiColumnHeaderState m_MultiColumnHeaderState;
    MultiUICreateTreeView m_TreeView;
    Dictionary<Transform, UICreateElement> traElementDic = new Dictionary<Transform, UICreateElement>();

    Transform m_tra;

    Rect multiColumnTreeViewRect {
        get { return new Rect(20, 30, position.width - 40, position.height - 60); }
    }

    Rect bottomToolbarRect {
        get { return new Rect(20f, position.height - 18f, position.width - 40f, 16f); }
    }

    public static MultiUICreateWindow GetWindow(Transform tra) {
        var window = GetWindow<MultiUICreateWindow>();
        window.m_tra = tra;
        window.titleContent = new GUIContent("Multi UICreate");
        window.Focus();
        window.Repaint();
        return window;
    }

    void OnGUI() {
        InitIfNeeded();
        m_TreeView.OnGUI(multiColumnTreeViewRect);
        BottomToolBar(bottomToolbarRect);
    }

    void InitIfNeeded() {
        if (!m_Initialized) {
            // Check if it already exists (deserialized from window layout file or scriptable object)
            if (m_TreeViewState == null)
                m_TreeViewState = new TreeViewState();

            bool firstInit = m_MultiColumnHeaderState == null;
            var headerState = MultiUICreateTreeView.CreateDefaultMultiColumnHeaderState(multiColumnTreeViewRect.width);
            if (MultiColumnHeaderState.CanOverwriteSerializedFields(m_MultiColumnHeaderState, headerState))
                MultiColumnHeaderState.OverwriteSerializedFields(m_MultiColumnHeaderState, headerState);
            m_MultiColumnHeaderState = headerState;

            var multiColumnHeader = new MultiColumnHeader(headerState);
            if (firstInit)
                multiColumnHeader.ResizeToFit();

            var treeModel = new TreeModel<UICreateElement>(UICreateElement.CreateUITransformTree(m_tra,ref traElementDic));

            m_TreeView = new MultiUICreateTreeView(m_TreeViewState, multiColumnHeader, treeModel);

            m_Initialized = true;
        }
    }

    void BottomToolBar(Rect rect) {
        GUILayout.BeginArea(rect);

        using (new EditorGUILayout.HorizontalScope()) {

            //var style = "miniButton";
            if (GUILayout.Button("展开",GUILayout.MinWidth(50))) {
                m_TreeView.ExpandAll();
            }

            if (GUILayout.Button("收起", GUILayout.MinWidth(50))) {
                m_TreeView.CollapseAll();
            }

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("生成", GUILayout.MinWidth(50))) {
                //Debug.Log("todo 生成");
                YUIEditor.WriteScript(m_tra, traElementDic);
                Close();
            }
        }
        GUILayout.EndArea();
    }
}
