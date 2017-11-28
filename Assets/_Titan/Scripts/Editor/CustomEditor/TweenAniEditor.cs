using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BaseUI))]
public class TweenAniEditor : Editor {
    
    public override void OnInspectorGUI() {
        
        BaseUI ui = target as BaseUI;

        if (ui.openTweens.Length > 0) {
            for (int i = 0; i < ui.openTweens.Length; i++) {
                BaseUI.TweenAnimation t = ui.openTweens[i];
                t.tweenAlpha = EditorGUILayout.Toggle("播放alpha值动画（需要有CanvasGroup）",t.tweenAlpha);
                if (t.tweenAlpha) {
                    EditorGUILayout.BeginHorizontal();
                    t.aFrom = EditorGUILayout.FloatField("从", t.aFrom);
                    t.aTo = EditorGUILayout.FloatField("到", t.aTo);
                    EditorGUILayout.EndHorizontal();
                }
            }
        }


        serializedObject.ApplyModifiedProperties();
    }
}
