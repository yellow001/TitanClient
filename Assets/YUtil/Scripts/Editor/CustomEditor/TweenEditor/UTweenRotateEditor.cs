using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UTweenRotate), true)]
[CanEditMultipleObjects]
public class UTweenRotateEditor : UGUITweenEditor {
    public override void OnInspectorGUI() {
        
        UTweenRotate t = target as UTweenRotate;
        t.autoPlay = EditorGUILayout.Toggle("自动播放", t.autoPlay);
        UIEditorHelper.BeginBox();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("旋转: ", GUILayout.MaxWidth(60));

        t.rFrom = EditorGUILayout.Vector3Field("从 ", t.rFrom);
        t.rTo = EditorGUILayout.Vector3Field("到 ", t.rTo);

        EditorGUILayout.EndVertical();

        UIEditorHelper.EndBox();

        base.OnInspectorGUI();
    }
}
