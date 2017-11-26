using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class ValueChangeEx {
    public static void ChangeValue(this float src, float dst, float duration, Action<float> changeFun, AnimationCurve curve = null, Action callback = null) {
        float temp = src;
        float offset = dst - src;
        TimeEventMgr.Ins.AddTimeEvent(duration, callback, (t, p) => {
            if (curve != null&&curve.length>0) {
                p = curve.Evaluate(p);
            }
            src =temp+ offset*p;
            changeFun(src);
        });
    }

    public static void ChangeValue(this Vector2 src, Vector2 dst, float duration, Action<Vector2> changeFun, AnimationCurve curve = null, Action callback = null) {
        Vector2 temp = src;
        Vector2 offset = dst - src;
        TimeEventMgr.Ins.AddTimeEvent(duration, callback, (t, p) => {
            if (curve != null&&curve.length>0) {
                p = curve.Evaluate(p);
            }
            src = temp + offset * p;
            changeFun(src);
        });
    }

    public static void ChangeVaule(this Vector3 src, Vector3 dst, float duration, Action<Vector3> changeFun, AnimationCurve curve=null, Action callback = null) {
        Vector3 temp = src;
        Vector3 offset = dst - src;
        TimeEventMgr.Ins.AddTimeEvent(duration, callback, (t, p) => {
            if (curve != null && curve.length > 0) {
                p = curve.Evaluate(p);
            }
            src =temp+ offset* p;
            changeFun(src);
        });
    }
}
