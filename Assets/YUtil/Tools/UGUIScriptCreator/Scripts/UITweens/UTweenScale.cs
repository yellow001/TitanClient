﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UTweenScale : UGUITween {
    public Vector3 sFrom=Vector3.one, sTo=Vector3.one;
    Transform tra;
    public bool autoPlay = false;

    // Use this for initialization
    public override void Awake() {
        base.Awake();
    }
    

    public override void Init() {
        tra = transform;

        forwardAni = new TimeEvent(delay, () => {
            tra.localScale = sFrom;
            tra.localScale.ChangeVaule(sTo, duration, (v) => tra.localScale = v, curve, ignoreTime,
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
        }, ignoreTime,null, loopCount, true);

        reverseAni = new TimeEvent(delay, () => {
            tra.localScale = sTo;
            tra.localScale.ChangeVaule(sFrom, duration, (v) => tra.localScale = v, curve, ignoreTime,
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
