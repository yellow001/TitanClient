using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UTweenPos : UGUITween {

    public Vector3 pFrom, pTo;
    Transform tra;
    public bool autoPlay=false;
    
    // Use this for initialization
    public override void Awake() {
        base.Awake();
	}

    public override void Init() {
        tra = transform;

        forwardAni = new TimeEvent(delay, () => {
            tra.localPosition = pFrom;
            tra.localPosition.ChangeVaule(pTo, duration, (v) => tra.localPosition = v, curve, ignoreTime,
                () => {
                    currentCount--;
                    currentCount = currentCount < 0 ? 0 : currentCount;
                    if (onForwardFinish != null) {
                        onForwardFinish.Invoke();
                    }
                });
        }, ignoreTime,null, loopCount);

        reverseAni = new TimeEvent(delay, () => {
            tra.localPosition = pTo;
            tra.localPosition.ChangeVaule(pFrom, duration, (v) => tra.localPosition = v, curve, ignoreTime,
                () => {
                    currentCount--;
                    currentCount = currentCount < 0 ? 0 : currentCount;
                    if (onReverseFinish != null) {
                        onReverseFinish.Invoke();
                    }
                });
        }, ignoreTime, null, loopCount);

        if (autoPlay) {
            PlayFroward();
        }
    }
}
