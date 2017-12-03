using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UTweenScale : UGUITween {
    public Vector3 sFrom=Vector3.one, sTo=Vector3.one;
    Transform tra;
    public bool autoPlay = false;

    // Use this for initialization
    public override void Start() {
        base.Start();
    }

    public override void Init() {
        tra = transform;

        forwardAni = new TimeEvent(duration, () => {
            tra.localScale = sFrom;
            tra.localScale.ChangeVaule(sTo, duration, (v) => tra.localScale = v, curve, ignoreTime,
                () => {
                    currentCount--;
                    currentCount = currentCount < 0 ? 0 : currentCount;
                    if (onFinish != null) {
                        onFinish.Invoke();
                    }
                });
        }, ignoreTime,null, loopCount, true);

        reverseAni = new TimeEvent(duration, () => {
            tra.localScale = sTo;
            tra.localScale.ChangeVaule(sFrom, duration, (v) => tra.localScale = v, curve, ignoreTime,
                () => {
                    currentCount--;
                    currentCount = currentCount < 0 ? 0 : currentCount;
                    if (onFinish != null) {
                        onFinish.Invoke();
                    }
                });
        }, ignoreTime,null, loopCount, true);

        if (autoPlay) {
            PlayFroward();
        }
    }
}
