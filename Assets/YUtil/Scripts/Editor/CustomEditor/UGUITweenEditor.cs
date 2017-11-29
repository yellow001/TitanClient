using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UGUITween),true)]
[CanEditMultipleObjects]
public class UGUITweenEditor : Editor {

    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();

        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();

        UGUITween t = target as UGUITween;

        UIEditorHelper.DrawTab("展开设置",ref t.show);

        if (t.show) {
            UIEditorHelper.BeginBox("LODCameraLine");
            t.tweenType=(UGUITween.EM_TweenType)EditorGUILayout.EnumPopup("动画播放时刻 ",t.tweenType);

            UIEditorHelper.BeginBox();
            t.duration = EditorGUILayout.FloatField("动画时间", t.duration);
            UIEditorHelper.EndBox();

            UIEditorHelper.BeginBox();
            t.tweenAlpha = EditorGUILayout.ToggleLeft("alpha动画(需要有 CanvasGroup 脚本)", t.tweenAlpha);
            if (t.tweenAlpha) {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("从", GUILayout.MaxWidth(16f));
                t.aFrom = EditorGUILayout.FloatField(t.aFrom, GUILayout.MaxWidth(30f));
                
                EditorGUILayout.LabelField("到", GUILayout.MaxWidth(16f));
                t.aTo = EditorGUILayout.FloatField(t.aTo, GUILayout.MaxWidth(30f));
                EditorGUILayout.EndHorizontal();
            }
            UIEditorHelper.EndBox();

            UIEditorHelper.BeginBox();
            t.tweenPos = EditorGUILayout.ToggleLeft("位置动画", t.tweenPos);
            if (t.tweenPos) {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("从", GUILayout.MaxWidth(16f));
                t.posFrom = EditorGUILayout.Vector3Field("",t.posFrom);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("到", GUILayout.MaxWidth(16f));
                t.posTo = EditorGUILayout.Vector3Field("",t.posTo);
                EditorGUILayout.EndHorizontal();
            }
            UIEditorHelper.EndBox();

            UIEditorHelper.BeginBox();
            t.tweenScale = EditorGUILayout.ToggleLeft("缩放动画", t.tweenScale);
            if (t.tweenScale) {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("从", GUILayout.MaxWidth(16f));
                t.scaleFrom = EditorGUILayout.Vector3Field("", t.scaleFrom);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("到", GUILayout.MaxWidth(16f));
                t.scaleTo = EditorGUILayout.Vector3Field("", t.scaleTo);
                EditorGUILayout.EndHorizontal();
            }
            UIEditorHelper.EndBox();


            if (t.tweenAlpha || t.tweenPos || t.tweenScale) {
                UIEditorHelper.BeginBox();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("动画曲线", GUILayout.MaxWidth(60));
                //EditorGUILayout.EndHorizontal();

                //EditorGUILayout.BeginHorizontal();
                t.curve = EditorGUILayout.CurveField(t.curve, GUILayout.MaxWidth(100), GUILayout.MinWidth(99), GUILayout.MaxHeight(100), GUILayout.MinHeight(99));
                EditorGUILayout.EndHorizontal();
                UIEditorHelper.EndBox();
            }
            
            UIEditorHelper.EndBox();
        }
    }

}
