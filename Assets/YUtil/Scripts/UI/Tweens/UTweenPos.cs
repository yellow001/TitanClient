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
            if (tra is RectTransform) {
                RectTransform rectTra = tra as RectTransform;
                rectTra.anchoredPosition3D = pFrom;
                rectTra.anchoredPosition3D.ChangeVaule(pTo, duration, (v) => rectTra.anchoredPosition3D = v, curve, ignoreTime,
                    () => {
                        currentCount--;
                        currentCount = currentCount < 0 ? 0 : currentCount;
                        if (onForwardFinish != null) {
                            onForwardFinish.Invoke();
                        }
                    });

                return;
            }
            tra.localPosition = pFrom;
            tra.localPosition.ChangeVaule(pTo, duration, (v) => tra.localPosition = v, curve, ignoreTime,
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
            if (tra is RectTransform) {
                RectTransform rectTra = tra as RectTransform;
                rectTra.anchoredPosition3D = pTo;
                rectTra.anchoredPosition3D.ChangeVaule(pFrom, duration, (v) => rectTra.anchoredPosition3D = v, curve, ignoreTime,
                    () => {
                        currentCount--;
                        currentCount = currentCount < 0 ? 0 : currentCount;
                        if (onReverseFinish != null) {
                            onReverseFinish.Invoke();
                        }
                    });

                return;
            }

            tra.localPosition = pTo;
            tra.localPosition.ChangeVaule(pFrom, duration, (v) => tra.localPosition = v, curve, ignoreTime,
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
        }, ignoreTime, null, loopCount, true);

        if (autoPlay) {
            PlayFroward();
        }
    }
}
