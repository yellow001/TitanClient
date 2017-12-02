using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UTweenRotate : UGUITween {

    public Vector3 rFrom, rTo;
    Transform tra;
    public bool autoPlay = false;

    public override void PlayFroward() {
        currentCount = loopCount;

        //delay秒后，以duration为周期播放动画
        this.AddTimeEvent(delay, () => {

            this.AddTimeEvent(duration, () => {
                tra.localEulerAngles = rFrom;
                tra.localEulerAngles.ChangeVaule(rTo, duration, (v) => tra.localEulerAngles = v, curve, ignoreTime,
                    () => {
                        currentCount--;
                        currentCount = currentCount < 0 ? 0 : currentCount;
                        if (onFinish != null) {
                            onFinish.Invoke();
                        }
                    });
            }, null, ignoreTime, loopCount, true);

        }, null, ignoreTime);
    }

    public override void PlayReverse() {
        currentCount = loopCount;

        //delay秒后，以duration为周期播放动画
        this.AddTimeEvent(delay, () => {

            this.AddTimeEvent(duration, () => {
                tra.localEulerAngles = rTo;
                tra.localEulerAngles.ChangeVaule(rFrom, duration, (v) => tra.localEulerAngles = v, curve, ignoreTime,
                    () => {
                        currentCount--;
                        currentCount = currentCount < 0 ? 0 : currentCount;
                        if (onFinish != null) {
                            onFinish.Invoke();
                        }
                    });
            }, null, ignoreTime, loopCount, true);

        }, null, ignoreTime);
    }

    // Use this for initialization
    public override void Start() {
        base.Start();
        tra = transform;
        if (autoPlay) {
            PlayFroward();
        }
    }
}
