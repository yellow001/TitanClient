using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BaseUITween), true)]
[CanEditMultipleObjects]
public class BaseUITweenEditor : UTweenAllEditor {
    public override void OnInspectorGUI() {
        serializedObject.Update();
        BaseUITween t = target as BaseUITween;

        EditorGUILayout.Space();
        UIEditorHelper.DrawTab("展开动画播放设置", ref showTween);

        if (showTween) {
            UIEditorHelper.BeginBox();
            t.playTime = (BaseUITween.EM_PlayTime)EditorGUILayout.EnumPopup("播放时机", t.playTime);
            t.doubleTween = EditorGUILayout.ToggleLeft("同时附加在 BaseUI 的打开和关闭动画", t.doubleTween, GUILayout.MaxWidth(225));

            TweenAllBaseGUI();

            EditorGUILayout.Space();
            UIEditorHelper.DrawTab("展开设置", ref showSetting);

            if (showSetting) {
                UIEditorHelper.BeginBox();
                UGUITweenBaseGUI();
                UIEditorHelper.EndBox();
            }

            EditorGUILayout.Space();

            
            UIEditorHelper.EndBox();
        }
        EditorGUILayout.Space();
        serializedObject.ApplyModifiedProperties();
    }
}
