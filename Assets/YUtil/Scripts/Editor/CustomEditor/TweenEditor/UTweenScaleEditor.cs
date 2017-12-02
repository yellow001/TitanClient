using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UTweenScale), true)]
[CanEditMultipleObjects]
public class UTweenScaleEditor : UGUITweenEditor {
    public override void OnInspectorGUI() {
        UTweenScale t = target as UTweenScale;
        t.autoPlay = EditorGUILayout.Toggle("自动播放", t.autoPlay);
        UIEditorHelper.BeginBox();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("缩放: ", GUILayout.MaxWidth(60));

        t.sFrom = EditorGUILayout.Vector3Field("从 ", t.sFrom);
        t.sTo = EditorGUILayout.Vector3Field("到 ", t.sTo);

        EditorGUILayout.EndVertical();

        UIEditorHelper.EndBox();

        base.OnInspectorGUI();
    }
}
