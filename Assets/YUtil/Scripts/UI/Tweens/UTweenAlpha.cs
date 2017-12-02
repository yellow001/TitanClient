using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UTweenAlpha : UGUITween {

    public CanvasGroup group;

    public Graphic uiElement;

    public float aFrom=1, aTo=1;
    public Color cFrom=Color.white, cTo=Color.white;
    public bool autoPlay = false;

    public override void PlayFroward() {
        currentCount = loopCount;

        //delay秒后，以duration为周期播放动画
        this.AddTimeEvent(delay, () => {

            this.AddTimeEvent(duration, () => {
                if (group != null) {
                    group.alpha = aFrom;
                    group.alpha.ChangeValue(aTo, duration, (v) => { group.alpha = v; }, curve, ignoreTime,
                        () => {
                            currentCount--;
                            currentCount = currentCount < 0 ? 0 : currentCount;
                            if (onFinish != null) {
                                onFinish.Invoke();
                            }
                        });
                }

                if (uiElement != null) {
                    uiElement.color = cFrom;
                    uiElement.color.ChangeVaule(cTo, duration, (v) => { uiElement.color = v; }, curve, ignoreTime,
                        () => {
                            if (group == null) {
                                currentCount--;
                                currentCount = currentCount < 0 ? 0 : currentCount;
                                if (onFinish != null) {
                                    onFinish.Invoke();
                                }
                            }
                        });
                }
            }, null, ignoreTime, loopCount,true);

        }, null,ignoreTime);

    }

    public override void PlayReverse() {
        currentCount = loopCount;
        //delay秒后，以duration为周期播放动画
        this.AddTimeEvent(delay, () => {

            this.AddTimeEvent(duration, () => {
                if (group != null) {
                    group.alpha = aTo;
                    group.alpha.ChangeValue(aFrom, duration, (v) => { group.alpha = v; }, curve, ignoreTime,
                        () => {
                            currentCount--;
                            currentCount = currentCount < 0 ? 0 : currentCount;
                            if (onFinish != null) {
                                onFinish.Invoke();
                            }
                        });
                }

                if (uiElement != null) {
                    uiElement.color = cTo;
                    uiElement.color.ChangeVaule(cFrom, duration, (v) => { uiElement.color = v; }, curve, ignoreTime,
                        () => {
                            if (group == null) {
                                currentCount--;
                                currentCount = currentCount < 0 ? 0 : currentCount;
                                if (onFinish != null) {
                                    onFinish.Invoke();
                                }
                            }
                        });
                }
            }, null, ignoreTime, loopCount,true);

        }, null, ignoreTime);
    }

    // Use this for initialization
    public override void Start() {

        base.Start();

        group = GetComponent<CanvasGroup>();
        uiElement = GetComponent<Graphic>();
        if (autoPlay) {
            PlayFroward();
        }
    }

}
