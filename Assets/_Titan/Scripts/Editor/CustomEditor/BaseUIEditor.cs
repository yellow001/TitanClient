using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

[CustomEditor(typeof(BaseUI),true)]
[CanEditMultipleObjects]
public class BaseUIEditor : Editor {
    int openTweenCount=0,closeTweenCount=0;

    bool showOpenTween=false, showCloseTween=false;

    List<bool> openAniList=new List<bool>(), closeAniList=new List<bool>();

    public void OnEnable() {
        BaseUI ui = target as BaseUI;
        openTweenCount = ui.openTweens.Count;
        closeTweenCount = ui.closeTweens.Count;

        for (int i = 0; i < openTweenCount; i++) {
            openAniList.Add(false);
        }

        for (int i = 0; i < closeTweenCount; i++) {
            closeAniList.Add(false);
        }
    }


    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();
        BaseUI ui = target as BaseUI;
        showOpenTween = EditorGUILayout.ToggleLeft("展开UI显示动画设置",showOpenTween);
        if (showOpenTween) {
            OpenTweenGUILayout(ui);
        }
        EditorGUILayout.Space();
        showCloseTween = EditorGUILayout.ToggleLeft("展开UI关闭动画设置", showCloseTween);
        if (showCloseTween) {
            CloseTweenGUILayout(ui);
        }
        
    }

    private void OpenTweenGUILayout(BaseUI ui) {
        openTweenCount = EditorGUILayout.IntField("UI显示动画数", openTweenCount);

        TweenGUILayout(openTweenCount, ui.openTweens,openAniList);
    }

    private void CloseTweenGUILayout(BaseUI ui) {
        closeTweenCount = EditorGUILayout.IntField("UI关闭动画数", closeTweenCount);

        TweenGUILayout(closeTweenCount, ui.closeTweens,closeAniList);
    }

    void TweenGUILayout(int showCount, List<BaseUI.TweenAnimation> tweens,List<bool> boolList) {
        if (showCount > tweens.Count) {
            for (int i = 0; i < showCount - tweens.Count; i++) {
                BaseUI.TweenAnimation t = new BaseUI.TweenAnimation();
                tweens.Add(t);

                boolList.Add(false);
            }
        }
        else if (showCount < tweens.Count) {
            tweens.RemoveRange(showCount, tweens.Count - showCount);
            boolList.RemoveRange(showCount, tweens.Count - showCount);
        }

        if (tweens.Count > 0) {
            for (int i = 0; i < tweens.Count; i++) {
                BaseUI.TweenAnimation t = tweens[i];
                int index = i;
                boolList[index] = EditorGUILayout.ToggleLeft("打开动画 " + index + " 设置", boolList[index]);

                if (boolList[index]) {

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("", GUILayout.MaxWidth(15));
                    t.duration = EditorGUILayout.FloatField("动画时间",t.duration);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("",GUILayout.MaxWidth(15));
                    t.tweenAlpha = EditorGUILayout.Toggle(t.tweenAlpha, GUILayout.MaxWidth(16f));
                    EditorGUILayout.LabelField("播放alpha值动画(需要有CanvasGroup)");
                    EditorGUILayout.EndHorizontal();
                    if (t.tweenAlpha) {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("", GUILayout.MaxWidth(30));
                        EditorGUILayout.LabelField("从", GUILayout.MaxWidth(16f));
                        t.aFrom = EditorGUILayout.FloatField(t.aFrom, GUILayout.MaxWidth(30f));

                        EditorGUILayout.LabelField("", GUILayout.MaxWidth(30));
                        EditorGUILayout.LabelField("到", GUILayout.MaxWidth(16f));
                        t.aTo = EditorGUILayout.FloatField(t.aTo, GUILayout.MaxWidth(30f));
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("", GUILayout.MaxWidth(15));
                    t.tweenPos = EditorGUILayout.Toggle(t.tweenPos, GUILayout.MaxWidth(16f));
                    EditorGUILayout.LabelField("播放位置动画");
                    EditorGUILayout.EndHorizontal();
                    if (t.tweenPos) {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("", GUILayout.MaxWidth(30));
                        EditorGUILayout.LabelField("从", GUILayout.MaxWidth(16f));
                        t.posFrom = EditorGUILayout.Vector3Field("", t.posFrom, GUILayout.MaxWidth(120f));
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("", GUILayout.MaxWidth(30));
                        EditorGUILayout.LabelField("到", GUILayout.MaxWidth(16f));
                        t.posTo = EditorGUILayout.Vector3Field("", t.posTo, GUILayout.MaxWidth(120f));
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("", GUILayout.MaxWidth(15));
                    t.tweenScale = EditorGUILayout.Toggle(t.tweenScale, GUILayout.MaxWidth(16f));
                    EditorGUILayout.LabelField("播放大小动画");
                    EditorGUILayout.EndHorizontal();
                    if (t.tweenScale) {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("", GUILayout.MaxWidth(30));
                        EditorGUILayout.LabelField("从", GUILayout.MaxWidth(16f));
                        t.scaleFrom = EditorGUILayout.Vector3Field("", t.scaleFrom, GUILayout.MaxWidth(120f));
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("", GUILayout.MaxWidth(30));
                        EditorGUILayout.LabelField("到", GUILayout.MaxWidth(16f));
                        t.scaleTo = EditorGUILayout.Vector3Field("", t.scaleTo, GUILayout.MaxWidth(120f));
                        EditorGUILayout.EndHorizontal();
                    }

                    if (t.tweenAlpha || t.tweenPos || t.tweenScale) {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("", GUILayout.MaxWidth(15));
                        EditorGUILayout.LabelField("动画曲线");
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("", GUILayout.MaxWidth(15));
                        t.curve = EditorGUILayout.CurveField(t.curve, GUILayout.MaxWidth(100), GUILayout.MinWidth(99), GUILayout.MaxHeight(100), GUILayout.MinHeight(99));
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
        }
    }
}
