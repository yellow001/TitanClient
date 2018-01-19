using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEditor.TreeViewExamples;
using UnityEngine;
using System.Linq;

public class MultiUICreateTreeView : TreeViewWithTreeModel<UICreateElement> {
    const float kRowHeights = 20f;
    const float kToggleWidth = 18f;

    // All columns
    enum MyColumns {
        Name,
        ReName
    }

    public MultiUICreateTreeView(TreeViewState state, MultiColumnHeader multiColumnHeader, TreeModel<UICreateElement> model) : base(state, multiColumnHeader, model) {
        // Custom setup
        rowHeight = kRowHeights;
        //columnIndexForTreeFoldouts = 2;
        showAlternatingRowBackgrounds = true;
        showBorder = true;
        //customFoldoutYOffset = (kRowHeights - EditorGUIUtility.singleLineHeight) * 0.5f; // center foldout in the row since we also center content. See RowGUI
        extraSpaceBeforeIconAndLabel = kToggleWidth;

        Reload();
    }


    protected override void RowGUI(RowGUIArgs args) {
        var item = (TreeViewItem<UICreateElement>)args.item;

        for (int i = 0; i < args.GetNumVisibleColumns(); ++i) {
            CellGUI(args.GetCellRect(i), item, (MyColumns)args.GetColumn(i), ref args);
        }
    }

    void CellGUI(Rect cellRect, TreeViewItem<UICreateElement> item, MyColumns column, ref RowGUIArgs args) {
        // Center cell rect vertically (makes it easier to place controls, icons etc in the cells)
        CenterRectUsingSingleLineHeight(ref cellRect);

        switch (column) {
            case MyColumns.Name: {
                    Event evt = Event.current;
                    // Do toggle
                    Rect toggleRect = cellRect;
                    toggleRect.x += GetContentIndent(item);
                    toggleRect.width = kToggleWidth;

                    //if (toggleRect.xMax < cellRect.xMax)
                    //    item.data.m_Write = EditorGUI.Toggle(toggleRect, item.data.m_Write); // hide when outside cell rect

                    // Ensure row is selected before using the toggle (usability)
                    if (evt.type == EventType.MouseDown && toggleRect.Contains(evt.mousePosition))
                        SelectionClick(args.item, false);

                    EditorGUI.BeginChangeCheck();
                    bool isWrite = EditorGUI.Toggle(toggleRect, item.data.m_Write);
                    if (EditorGUI.EndChangeCheck()) {
                        item.data.m_Write = isWrite;
                        //将子物体的勾选项与父物体同步
                        SetChildWrited(item.data);
                    }  

                    // Default icon and label
                    args.rowRect = cellRect;
                    base.RowGUI(args);
                }
                break;

            case MyColumns.ReName: {
                    cellRect.xMin += 5f;
                    item.data.m_Rename = GUI.TextField(cellRect, item.data.m_Rename);
                }
                break;
        }
    }

    /// <summary>
    /// 将子物体的勾选项与父物体同步
    /// </summary>
    /// <param name="element">父物体节点</param>
    static void SetChildWrited(UICreateElement element) {
        if(element.hasChildren)
            foreach (var item in element.children) {
                ((UICreateElement)item).m_Write = element.m_Write;
                SetChildWrited((UICreateElement)item);
            }
    }

    public static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState(float treeViewWidth) {
        var columns = new[]
        {
                //new MultiColumnHeaderState.Column
                //{
                //    headerContent = new GUIContent(EditorGUIUtility.FindTexture("FilterByLabel"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. "),
                //    contextMenuText = "Asset",
                //    headerTextAlignment = TextAlignment.Center,
                //    sortedAscending = true,
                //    sortingArrowAlignment = TextAlignment.Right,
                //    width = 30,
                //    minWidth = 30,
                //    maxWidth = 60,
                //    autoResize = false,
                //    allowToggleVisibility = true
                //},
                //new MultiColumnHeaderState.Column
                //{
                //    headerContent = new GUIContent(EditorGUIUtility.FindTexture("FilterByType"), "Sed hendrerit mi enim, eu iaculis leo tincidunt at."),
                //    contextMenuText = "Type",
                //    headerTextAlignment = TextAlignment.Center,
                //    sortedAscending = true,
                //    sortingArrowAlignment = TextAlignment.Right,
                //    width = 30,
                //    minWidth = 30,
                //    maxWidth = 60,
                //    autoResize = false,
                //    allowToggleVisibility = true
                //},
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("物体名"),
                    headerTextAlignment = TextAlignment.Left,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Center,
                    width = 150,
                    minWidth = 60,
                    autoResize = false,
                    allowToggleVisibility = false
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("生成属性名"),
                    headerTextAlignment = TextAlignment.Left,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Center,
                    width = 150,
                    minWidth = 60,
                    autoResize = false,
                    allowToggleVisibility = false
                },
                //new MultiColumnHeaderState.Column
                //{
                //    headerContent = new GUIContent("Multiplier", "In sed porta ante. Nunc et nulla mi."),
                //    headerTextAlignment = TextAlignment.Right,
                //    sortedAscending = true,
                //    sortingArrowAlignment = TextAlignment.Left,
                //    width = 110,
                //    minWidth = 60,
                //    autoResize = true
                //},
                //new MultiColumnHeaderState.Column
                //{
                //    headerContent = new GUIContent("Material", "Maecenas congue non tortor eget vulputate."),
                //    headerTextAlignment = TextAlignment.Right,
                //    sortedAscending = true,
                //    sortingArrowAlignment = TextAlignment.Left,
                //    width = 95,
                //    minWidth = 60,
                //    autoResize = true,
                //    allowToggleVisibility = true
                //},
                //new MultiColumnHeaderState.Column
                //{
                //    headerContent = new GUIContent("Note", "Nam at tellus ultricies ligula vehicula ornare sit amet quis metus."),
                //    headerTextAlignment = TextAlignment.Right,
                //    sortedAscending = true,
                //    sortingArrowAlignment = TextAlignment.Left,
                //    width = 70,
                //    minWidth = 60,
                //    autoResize = true
                //}
            };

        //Assert.AreEqual(columns.Length, Enum.GetValues(typeof(MyColumns)).Length, "Number of columns should match number of enum values: You probably forgot to update one of them.");

        var state = new MultiColumnHeaderState(columns);
        return state;
    }
}
