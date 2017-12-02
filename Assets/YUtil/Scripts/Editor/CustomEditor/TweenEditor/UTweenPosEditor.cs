using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UTweenPos), true)]
[CanEditMultipleObjects]
public class UTweenPosEditor : UGUITweenEditor {


    public override void OnInspectorGUI() {
        UTweenPos t = target as UTweenPos;

        t.autoPlay = EditorGUILayout.Toggle("自动播放", t.autoPlay);
        UIEditorHelper.BeginBox();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("位置: ", GUILayout.MaxWidth(60));
        
        t.pFrom = EditorGUILayout.Vector3Field("从 ", t.pFrom);
        t.pTo = EditorGUILayout.Vector3Field("到 ", t.pTo);

        EditorGUILayout.EndVertical();

        UIEditorHelper.EndBox();

        base.OnInspectorGUI();
    }
}
