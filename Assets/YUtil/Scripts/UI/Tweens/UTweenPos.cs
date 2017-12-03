using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UTweenPos : UGUITween {

    public Vector3 pFrom, pTo;
    Transform tra;
    public bool autoPlay=false;
    
    // Use this for initialization
    public override void Start () {
        base.Start();
	}

    public override void Init() {
        tra = transform;

        forwardAni = new TimeEvent(duration, () => {
            tra.localPosition = pFrom;
            tra.localPosition.ChangeVaule(pTo, duration, (v) => tra.localPosition = v, curve, ignoreTime,
                () => {
                    currentCount--;
                    currentCount = currentCount < 0 ? 0 : currentCount;
                    if (onFinish != null) {
                        onFinish.Invoke();
                    }
                });
        }, ignoreTime,null, loopCount, true);

        reverseAni = new TimeEvent(duration, () => {
            tra.localPosition = pTo;
            tra.localPosition.ChangeVaule(pFrom, duration, (v) => tra.localPosition = v, curve, ignoreTime,
                () => {
                    currentCount--;
                    currentCount = currentCount < 0 ? 0 : currentCount;
                    if (onFinish != null) {
                        onFinish.Invoke();
                    }
                });
        }, ignoreTime, null, loopCount, true);

        if (autoPlay) {
            PlayFroward();
        }
    }
}
