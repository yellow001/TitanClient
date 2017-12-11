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
    
    // Use this for initialization
    public override void Awake() {
        base.Awake();
    }

    public override void Init() {
        group = GetComponent<CanvasGroup>();
        uiElement = GetComponent<Graphic>();

        forwardAni = new TimeEvent(delay, () => {
            if (group != null) {
                group.alpha = aFrom;
                group.alpha.ChangeValue(aTo, duration, (v) => { group.alpha = v; }, curve, ignoreTime,
                    () => {
                        currentCount--;
                        currentCount = currentCount < 0 ? 0 : currentCount;
                        if (onForwardFinish != null) {
                            onForwardFinish.Invoke();
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
                            if (onForwardFinish != null) {
                                onForwardFinish.Invoke();
                            }
                        }
                    });
            }

            if (first) {
                first = false;
                forwardAni.waitTime = delay + duration;
            }

        }, ignoreTime,null, loopCount, true);

        reverseAni = new TimeEvent(delay, () => {
            if (group != null) {
                group.alpha = aTo;
                group.alpha.ChangeValue(aFrom, duration, (v) => { group.alpha = v; }, curve, ignoreTime,
                    () => {
                        currentCount--;
                        currentCount = currentCount < 0 ? 0 : currentCount;
                        if (onReverseFinish != null) {
                            onReverseFinish.Invoke();
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
                            if (onReverseFinish != null) {
                                onForwardFinish.Invoke();
                            }
                        }
                    });
            }

            if (first) {
                first = false;
                forwardAni.waitTime = delay + duration;
            }

        }, ignoreTime, null, loopCount, true);


        if (autoPlay) {
            PlayFroward();
        }
    }
}
