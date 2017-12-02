﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

[CustomEditor(typeof(UGUITween),true)]
[CanEditMultipleObjects]
public class UGUITweenEditor : Editor {

    public bool showCurve = false;
    public bool showSetting = true;

    public override void OnInspectorGUI() {

        serializedObject.Update();

        EditorGUILayout.Space();
        UIEditorHelper.DrawTab("展开设置", ref showSetting);

        if (showSetting) {

            UIEditorHelper.BeginBox();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("loopCount"), true);

            UGUITweenBaseGUI();

            UIEditorHelper.EndBox();
        }

        EditorGUILayout.Space();
        serializedObject.ApplyModifiedProperties();
    }


    public void UGUITweenBaseGUI() {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("delay"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("duration"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("ignoreTime"), true);


        UGUITween t = target as UGUITween;

        EditorGUILayout.Space();

        UIEditorHelper.DrawTab("展开曲线", ref showCurve);
        if (showCurve) {
            UIEditorHelper.BeginBox();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("动画曲线", GUILayout.MaxWidth(60));
            t.curve = EditorGUILayout.CurveField(t.curve, GUILayout.MaxWidth(100), GUILayout.MinWidth(99), GUILayout.MaxHeight(100), GUILayout.MinHeight(99));
            EditorGUILayout.EndHorizontal();
            UIEditorHelper.EndBox();
        }

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("onFinish"), true);
    }
}