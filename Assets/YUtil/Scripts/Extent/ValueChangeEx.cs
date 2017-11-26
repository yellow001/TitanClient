using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class ValueChangeEx {
    public static void ChangeValue(this float src, float dst, float duration, Action<float> changeFun, Action callback = null) {
        float temp = src;
        TimeEventMgr.Ins.AddTimeEvent(duration, callback, (t, p) => {
            src = Mathf.Lerp(temp, dst, p);
            changeFun(src);
        });
    }

    public static void ChangeValue(this Vector2 src, Vector2 dst, float duration, Action<Vector2> changeFun, Action callback = null) {
        Vector2 temp = src;
        TimeEventMgr.Ins.AddTimeEvent(duration, callback, (t, p) => {
            src = Vector2.Lerp(temp, dst, p);
            changeFun(src);
        });
    }

    public static void ChangeVector(this Vector3 src, Vector3 dst, float duration, Action<Vector3> changeFun, Action callback = null) {
        Vector3 temp = src;
        TimeEventMgr.Ins.AddTimeEvent(duration, callback, (t, p) => {
            src = Vector3.Lerp(temp, dst, p);
            changeFun(src);
        });
    }
}
