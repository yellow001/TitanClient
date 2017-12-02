using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UTweenAll), true)]
[CanEditMultipleObjects]
public class UTweenAllEditor : UGUITweenEditor {

    //#region 颜色
    //public float aFrom, aTo;
    //public Color cFrom, cTo;
    //public bool tweenColor = false;
    //#endregion

    //public Vector3 pFrom, pTo;
    //public bool tweenPos = true;

    //public Vector3 sFrom, sTo;
    //public bool tweenScale = false;

    //public Vector3 rFrom, rTo;
    //public bool tweenRotate = false;

    //Transform tra;

    public bool showTween= true;

    public override void OnInspectorGUI() {
        
        UTweenAll t = target as UTweenAll;

        t.autoPlay = EditorGUILayout.Toggle("自动播放", t.autoPlay);

        UIEditorHelper.DrawTab("展开动画播放设置", ref showTween);
        if (showTween) {
            #region 颜色
            UIEditorHelper.BeginBox();
            t.tweenColor = EditorGUILayout.ToggleLeft("播放颜色(alpha)动画", t.tweenColor);
            if (t.tweenColor) {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("CanvasGroup的alpha值: ", GUILayout.MaxWidth(160));
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
                EditorGUILayout.LabelField("到", GUILayout.MaxWidth(16f));
                t.cTo = EditorGUILayout.ColorField(t.cTo, GUILayout.MaxWidth(60f));
                EditorGUILayout.EndHorizontal();
            }
            UIEditorHelper.EndBox();
            #endregion

            #region 位置
            UIEditorHelper.BeginBox();
            t.tweenPos = EditorGUILayout.ToggleLeft("播放位置动画", t.tweenPos);
            if (t.tweenPos) {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField("位置: ", GUILayout.MaxWidth(60));

                t.pFrom = EditorGUILayout.Vector3Field("从 ", t.pFrom);
                t.pTo = EditorGUILayout.Vector3Field("到 ", t.pTo);
            }
            UIEditorHelper.EndBox();
            #endregion

            #region 旋转
            UIEditorHelper.BeginBox();
            t.tweenRotate = EditorGUILayout.ToggleLeft("播放旋转动画", t.tweenRotate);
            if (t.tweenRotate) {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField("旋转: ", GUILayout.MaxWidth(60));

                t.rFrom = EditorGUILayout.Vector3Field("从 ", t.rFrom);
                t.rTo = EditorGUILayout.Vector3Field("到 ", t.rTo);
            }
            UIEditorHelper.EndBox();
            #endregion

            #region 缩放
            UIEditorHelper.BeginBox();
            t.tweenScale = EditorGUILayout.ToggleLeft("播放缩放动画", t.tweenScale);
            if (t.tweenScale) {
                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField("缩放: ", GUILayout.MaxWidth(60));

                t.sFrom = EditorGUILayout.Vector3Field("从 ", t.sFrom);
                t.sTo = EditorGUILayout.Vector3Field("到 ", t.sTo);
            }
            UIEditorHelper.EndBox();
            #endregion
        }
        base.OnInspectorGUI();
    }
}
