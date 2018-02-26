/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.ComponentModel;
using HutongGames.PlayMakerEditor;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(FsmTemplate))]
public class FsmTemplateEditor : Editor
{
    private SerializedProperty categoryProperty;
    private SerializedProperty descriptionProperty;
    private GUIStyle multiline;

    [Localizable(false)]
    public void OnEnable()
    {
        categoryProperty = serializedObject.FindProperty("category");
        descriptionProperty = serializedObject.FindProperty("fsm.description");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(categoryProperty);

        if (multiline == null)
        {
            multiline = new GUIStyle(EditorStyles.textField) { wordWrap = true };
        }
        descriptionProperty.stringValue = EditorGUILayout.TextArea(descriptionProperty.stringValue, multiline, GUILayout.MinHeight(60));

        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button(Strings.FsmTemplateEditor_Open_In_Editor))
        {
            FsmEditorWindow.OpenWindow((FsmTemplate) target);
        }

        EditorGUILayout.HelpBox(Strings.Hint_Exporting_Templates, MessageType.None );
    }
}
