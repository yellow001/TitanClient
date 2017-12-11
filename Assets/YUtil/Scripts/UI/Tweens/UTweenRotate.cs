using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UTweenRotate : UGUITween {

    public Vector3 rFrom, rTo;
    Transform tra;
    public bool autoPlay = false;
    
    // Use this for initialization
    public override void Awake() {
        base.Awake();
    }

    public override void Init() {
        tra = transform;

        forwardAni = new TimeEvent(delay, () => {
            tra.localEulerAngles = rFrom;
            tra.localEulerAngles.ChangeVaule(rTo, duration, (v) => tra.localEulerAngles = v, curve, ignoreTime,
                () => {
                    currentCount--;
                    currentCount = currentCount < 0 ? 0 : currentCount;
                    if (onForwardFinish != null) {
                        onForwardFinish.Invoke();
                    }
                });

            if (first && loopCount != 1) {
                first = false;
                forwardAni.waitTime = delay + duration;
            }
        }, ignoreTime, null, loopCount, true);

        reverseAni = new TimeEvent(delay, () => {
            tra.localEulerAngles = rTo;
            tra.localEulerAngles.ChangeVaule(rFrom, duration, (v) => tra.localEulerAngles = v, curve, ignoreTime,
                () => {
                    currentCount--;
                    currentCount = currentCount < 0 ? 0 : currentCount;
                    if (onReverseFinish != null) {
                        onReverseFinish.Invoke();
                    }
                });

            if (first && loopCount != 1) {
                first = false;
                forwardAni.waitTime = delay + duration;
            }
        }, ignoreTime,null, loopCount, true);

        if (autoPlay) {
            PlayFroward();
        }
    }
}
