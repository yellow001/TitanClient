using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UTweenScale : UGUITween {
    public Vector3 sFrom, sTo;
    Transform tra;
    public bool autoPlay = false;
    public override void PlayFroward() {
        currentCount = loopCount;

        //delay秒后，以duration为周期播放动画
        this.AddTimeEvent(delay, () => {

            this.AddTimeEvent(duration, () => {
                tra.localScale = sFrom;
                tra.localScale.ChangeVaule(sTo, duration, (v) => tra.localScale = v, curve, ignoreTime,
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
                tra.localScale = sTo;
                tra.localScale.ChangeVaule(sFrom, duration, (v) => tra.localScale = v, curve, ignoreTime,
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
