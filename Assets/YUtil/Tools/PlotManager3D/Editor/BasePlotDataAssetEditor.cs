using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Events;

[CustomEditor(typeof(BasePlotDataAsset), true)]
public class BasePlotDataAssetEditor : Editor {
    SearchField m_SearchField;
    const string kSessionStateKeyPrefix = "TVS";

    int selectIndex = -1;

    AnimBool showDatas = new AnimBool(true);

    AnimBool showChildAsset=new AnimBool(true);
    
    BasePlotDataAsset asset {
        get { return (BasePlotDataAsset)target; }
    }

    void OnEnable() {
        showDatas.valueChanged.AddListener(new UnityAction(base.Repaint));
        showChildAsset.valueChanged.AddListener(new UnityAction(base.Repaint));
    }

    public override void OnInspectorGUI() {
        Undo.RecordObject(target, "PlotData");
        //GUILayout.Space(5f);
        //GUILayout.Space(3f);

        //const float topToolbarHeight = 20f;
        //const float spacing = 2f;
        //float totalHeight = m_TreeView.totalHeight + topToolbarHeight + 2 * spacing;
        //Rect rect = GUILayoutUtility.GetRect(0, 10000, 0, totalHeight);
        //Rect toolbarRect = new Rect(rect.x, rect.y, rect.width, topToolbarHeight);
        //Rect multiColumnTreeViewRect = new Rect(rect.x, rect.y + topToolbarHeight + spacing, rect.width, rect.height - topToolbarHeight - 2 * spacing);
        //SearchBar(toolbarRect);

        EditorGUI.BeginChangeCheck();

        AssetGUI();

        DoListGUI();

        GUILayout.Space(5);

        ChildAssetGUI();

        if (EditorGUI.EndChangeCheck()) {
            EditorUtility.SetDirty(target);
        }
    }

    void SearchBar(Rect rect) {
        //m_TreeView.searchString = m_SearchField.OnGUI(rect, m_TreeView.searchString);
    }

    void AssetGUI() {
        EditorGUILayout.ObjectField(target, typeof(UnityEngine.Object), false);

        asset.BGM = (AudioClip)EditorGUILayout.ObjectField("背景音乐", asset.BGM, typeof(AudioClip), false);

        GUILayout.Space(5);
    }


    void DoListGUI() {

        showDatas.target = EditorGUILayout.ToggleLeft("显示 对话数据", showDatas.target);


        Event e = Event.current;

        using (var group=new EditorGUILayout.FadeGroupScope(showDatas.faded)) {
            if (group.visible) {
                Rect root = EditorGUILayout.BeginVertical();

                if (asset.datas != null && asset.datas.Count > 0) {
                    for (int i = 0; i < asset.datas.Count; i++) {
                        BasePlotData data = asset.datas[i];
                        Color col = i % 2 == 0 ? Color.white : new Color(0.9f, 0.9f, 0.9f, 1);
                        col = selectIndex == i ? new Color(0.72f, 0.72f, 0.9f, 1) : col;
                        GUI.backgroundColor = col;

                        Rect boxRect = EditorGUILayout.BeginVertical("box");
                        if (e.type == EventType.MouseDown && boxRect.Contains(Event.current.mousePosition)) {
                            selectIndex = i;
                            Repaint();
                        }

                        GUI.backgroundColor = Color.white;

                        if (data.roleNames.Count > 1) {
                            GUILayout.Label("对话 " + (i + 1));
                        }

                        for (int j = 0; j < data.roleNames.Count; j++) {
                            EditorGUILayout.BeginVertical("box");
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("角色名"+(j+1), GUILayout.MaxWidth(50), GUILayout.MinWidth(50));
                            data.em_roleNames[j] = (EM_RoleName)EditorGUILayout.EnumPopup(data.em_roleNames[j], GUILayout.MaxWidth(120));
                            if (data.em_roleNames[j] == EM_RoleName.None) {
                                data.roleNames[j] = EditorGUILayout.TextField(data.roleNames[j], GUILayout.MaxWidth(120));
                            }
                            else {
                                data.roleNames[j] = data.GetRoleNameEditorGUI(j);
                                EditorGUILayout.LabelField(data.roleNames[j], GUILayout.MaxWidth(120));
                            }

                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("内容" + (j + 1), GUILayout.MaxWidth(50), GUILayout.MinWidth(50));
                            data.contents[j] = GUILayout.TextArea(data.contents[j], (int)(EditorGUIUtility.currentViewWidth - 60));
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("语音" + (j + 1), GUILayout.MaxWidth(50), GUILayout.MinWidth(50));
                            data.audios[j] = (AudioClip)EditorGUILayout.ObjectField(data.audios[j], typeof(AudioClip), false);
                            GUILayout.EndHorizontal();

                            EditorGUILayout.BeginHorizontal();
                            GUILayout.Label("头像" + (j + 1), GUILayout.MaxWidth(50), GUILayout.MinWidth(50));
                            data.Icons[j] = (Sprite)EditorGUILayout.ObjectField(data.Icons[j], typeof(Sprite), false, GUILayout.MaxWidth(120));
                            if (data.Icons[j]) {
                                GUILayout.Button(data.Icons[j].texture, GUILayout.MaxHeight(32), GUILayout.MaxWidth(32));
                            }
                            EditorGUILayout.EndHorizontal();

                            if (data.roleNames.Count > 1) {
                                if (GUILayout.Button(" -- ", "minibutton", GUILayout.MaxWidth(30))) {
                                    data.DeleteData(j);
                                }
                            }
                            
                            EditorGUILayout.EndVertical();

                            GUILayout.Space(5);
                        }
                        

                        EditorGUILayout.EndVertical();


                        EditorGUILayout.BeginHorizontal();
                        if (GUILayout.Button(" ++ ", "minibutton")) {
                            data.AddData("", "", null, null);
                            Repaint();
                        }
                        GUILayout.FlexibleSpace();
                        GUI.backgroundColor = col;
                        EditorGUILayout.BeginHorizontal("TE Toolbar");
                        GUI.backgroundColor = Color.white;
                        if (GUILayout.Button(" ▲ ", "minibutton")) {
                            if (i > 0) {
                                asset.datas.RemoveAt(i);
                                asset.datas.Insert(i - 1, data);
                                selectIndex = i - 1;
                            }
                        }
                        GUILayout.Space(8);
                        if (GUILayout.Button(" ▼ ", "minibutton")) {
                            if (i < asset.datas.Count - 1) {
                                asset.datas.RemoveAt(i);
                                asset.datas.Insert(i + 1, data);
                                selectIndex = i + 1;
                            }
                        }
                        GUILayout.Space(8);
                        if (GUILayout.Button(" + ", "minibutton")) {
                            asset.datas.Insert(i + 1, new BasePlotData());
                        }
                        GUILayout.Space(8);
                        if (GUILayout.Button(" - ", "minibutton")) {
                            asset.datas.RemoveAt(i);
                        }
                        GUILayout.Space(5);
                        EditorGUILayout.EndHorizontal();
                        GUILayout.Space(5);
                        EditorGUILayout.EndHorizontal();
                        GUILayout.Space(10);
                    }
                }

                GUI.backgroundColor = Color.white;
                GUILayout.Space(8);
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("添加末端对话", GUILayout.MaxWidth(100))) {
                    asset.datas.Add(new BasePlotData());
                }
                EditorGUILayout.EndHorizontal();

                if (e.type == EventType.MouseDown && !root.Contains(e.mousePosition)) {
                    selectIndex = -1;
                }

                EditorGUILayout.EndVertical();
            }
        }
    }

    void ChildAssetGUI() {

        if (Event.current.commandName.Equals("ObjectSelectorClosed")) {
            BasePlotDataAsset newAsset = EditorGUIUtility.GetObjectPickerObject() as BasePlotDataAsset;
            if (newAsset != null) {
                asset.selString.Add("");
                asset.childAssets.Add(newAsset);
            }
            return;
        }

        showChildAsset.target = EditorGUILayout.ToggleLeft("展示分支", showChildAsset.target);

        using (var group=new EditorGUILayout.FadeGroupScope(showChildAsset.faded)) {
            if (group.visible) {
                if (asset.childAssets != null && asset.childAssets.Count > 0) {
                    for (int i = 0; i < asset.childAssets.Count; i++) {
                        BasePlotDataAsset item = asset.childAssets[i];

                        Color col = i % 2 == 0 ? Color.white : new Color(0.9f, 0.9f, 0.9f, 1);
                        GUI.backgroundColor = col;

                        EditorGUILayout.BeginVertical("box");

                        GUILayout.Space(2);

                        GUI.backgroundColor = Color.white;
                        EditorGUILayout.BeginHorizontal();
                        item = (BasePlotDataAsset)EditorGUILayout.ObjectField(item, typeof(BasePlotDataAsset), false);

                        GUILayout.Label("分支选项", GUILayout.MaxWidth(50), GUILayout.MinWidth(50));

                        asset.selString[i] = EditorGUILayout.TextArea(asset.selString[i], GUILayout.MaxWidth(120));

                        EditorGUILayout.EndHorizontal();


                        EditorGUILayout.BeginHorizontal();
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button(" - ", "minibutton")) {
                            asset.childAssets.RemoveAt(i);
                            asset.selString.RemoveAt(i);
                            Repaint();
                        }
                        EditorGUILayout.EndHorizontal();

                        GUILayout.Space(2);

                        EditorGUILayout.EndVertical();

                        GUILayout.Space(5);
                    }
                }


                GUI.backgroundColor = Color.white;
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("添加剧情分支", GUILayout.MaxWidth(100))) {
                    EditorGUIUtility.ShowObjectPicker<BasePlotDataAsset>(null, false, "", 2);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        
        
    }
}
