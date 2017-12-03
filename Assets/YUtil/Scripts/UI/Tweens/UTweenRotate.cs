using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UTweenRotate : UGUITween {

    public Vector3 rFrom, rTo;
    Transform tra;
    public bool autoPlay = false;
    
    // Use this for initialization
    public override void Start() {
        base.Start();
    }

    public override void Init() {
        tra = transform;

        forwardAni = new TimeEvent(duration, () => {
            tra.localEulerAngles = rFrom;
            tra.localEulerAngles.ChangeVaule(rTo, duration, (v) => tra.localEulerAngles = v, curve, ignoreTime,
                () => {
                    currentCount--;
                    currentCount = currentCount < 0 ? 0 : currentCount;
                    if (onFinish != null) {
                        onFinish.Invoke();
                    }
                });
        }, ignoreTime, null, loopCount, true);

        reverseAni = new TimeEvent(duration, () => {
            tra.localEulerAngles = rTo;
            tra.localEulerAngles.ChangeVaule(rFrom, duration, (v) => tra.localEulerAngles = v, curve, ignoreTime,
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
