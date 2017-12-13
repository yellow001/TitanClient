using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UTweenAlpha), true)]
[CanEditMultipleObjects]
public class UTweenAlphaEditor : UGUITweenEditor {

    //public float aFrom, aTo;
    //public Color cFrom, cTo;
    //public bool autoPlay = false;

    public override void OnInspectorGUI() {
        Undo.RecordObject(target, "UTweenAlpha");

        UTweenAlpha t = target as UTweenAlpha;

        t.autoPlay=EditorGUILayout.Toggle("自动播放", t.autoPlay);

        UIEditorHelper.BeginBox();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("CanvasGroup的alpha值: ",GUILayout.MaxWidth(160));
        EditorGUILayout.LabelField("从", GUILayout.MaxWidth(16f));
        t.aFrom = EditorGUILayout.FloatField(t.aFrom, GUILayout.MaxWidth(30f));
        EditorGUILayout.LabelField("到", GUILayout.MaxWidth(16f));
        t.aTo = EditorGUILayout.FloatField(t.aTo, GUILayout.MaxWidth(30f));
        EditorGUILayout.EndHorizontal();

        UIEditorHelper.EndBox();


        UIEditorHelper.BeginBox();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("纹理color: ", GUILayout.MaxWidth(160));
        EditorGUILayout.LabelField("从", GUILayout.MaxWidth(16f));
        t.cFrom = EditorGUILayout.ColorField(t.cFrom, GUILayout.MaxWidth(60f));
        //EditorGUILayout.EndHorizontal();

        //EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("到", GUILayout.MaxWidth(16f));
        t.cTo = EditorGUILayout.ColorField(t.cTo, GUILayout.MaxWidth(60f));
        EditorGUILayout.EndHorizontal();

        UIEditorHelper.EndBox();

        base.OnInspectorGUI();
    }

}
