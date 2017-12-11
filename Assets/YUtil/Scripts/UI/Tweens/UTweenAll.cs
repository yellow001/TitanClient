using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UTweenAll : UGUITween {
    #region 颜色
    public CanvasGroup group;
    public Graphic uiElement;
    public float aFrom = 1, aTo = 1;
    public Color cFrom = Color.white, cTo = Color.white;
    public bool tweenAlpha = false;
    public bool tweenColor = false;
    #endregion

    public Vector3 pFrom, pTo;
    public bool tweenPos = true;

    public Vector3 sFrom = Vector3.one, sTo = Vector3.one;
    public bool tweenScale = false;

    public Vector3 rFrom, rTo;
    public bool tweenRotate = false;

    Transform tra;

    public bool autoPlay = false;

    public override void Awake() {
        base.Awake();
    }

    public override void Init() {
        group = GetComponent<CanvasGroup>();
        uiElement = GetComponent<Graphic>();
        tra = transform;

        forwardAni = new TimeEvent(delay, () => {

            #region 颜色
            if (tweenAlpha && group != null) {
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

            if (tweenColor && uiElement != null) {
                uiElement.color = cFrom;
                uiElement.color.ChangeVaule(cTo, duration, (v) => { uiElement.color = v; }, curve, ignoreTime,
                    () => {
                        if (group == null) {
                            currentCount--;
                            currentCount = currentCount < 0 ? 0 : currentCount;
                            if (!tweenAlpha && onForwardFinish != null) {
                                onForwardFinish.Invoke();
                            }
                        }
                    });
            }
            #endregion

            #region 位置
            if (tweenPos) {
                if (tra is RectTransform) {
                    RectTransform rectTra = tra as RectTransform;
                    rectTra.anchoredPosition3D = pFrom;
                    rectTra.anchoredPosition3D.ChangeVaule(pTo, duration, (v) => rectTra.anchoredPosition3D = v, curve, ignoreTime,
                        () => {
                            currentCount--;
                            currentCount = currentCount < 0 ? 0 : currentCount;
                            if (!tweenColor && onForwardFinish != null) {
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
                        if (!tweenColor && onForwardFinish != null) {
                            onForwardFinish.Invoke();
                        }
                    });

            }
            #endregion

            #region 旋转
            if (tweenRotate) {
                tra.localEulerAngles = rFrom;
                tra.localEulerAngles.ChangeVaule(rTo, duration, (v) => tra.localEulerAngles = v, curve, ignoreTime,
                    () => {
                        currentCount--;
                        currentCount = currentCount < 0 ? 0 : currentCount;
                        if (!tweenPos && onForwardFinish != null) {
                            onForwardFinish.Invoke();
                        }
                    });
            }
            #endregion

            #region 缩放
            if (tweenScale) {
                tra.localScale = sFrom;
                tra.localScale.ChangeVaule(sTo, duration, (v) => tra.localScale = v, curve, ignoreTime,
                    () => {
                        currentCount--;
                        currentCount = currentCount < 0 ? 0 : currentCount;
                        if (!tweenScale && onForwardFinish != null) {
                            onForwardFinish.Invoke();
                        }
                    });
            }
            #endregion

            if (first && loopCount != 1) {
                first = false;
                forwardAni.waitTime = delay + duration;
            }
        }, ignoreTime, null, loopCount, true);

        reverseAni = new TimeEvent(delay, () => {

            #region 颜色
            if (tweenAlpha && group != null) {
                group.alpha = aTo;
                group.alpha.ChangeValue(aFrom, duration, (v) => { group.alpha = v; }, curve, ignoreTime,
                    () => {
                        currentCount--;
                        currentCount = currentCount < 0 ? 0 : currentCount;
                        if (onForwardFinish != null) {
                            onForwardFinish.Invoke();
                        }
                    });
            }

            if (tweenColor && uiElement != null) {
                uiElement.color = cTo;
                uiElement.color.ChangeVaule(cFrom, duration, (v) => { uiElement.color = v; }, curve, ignoreTime,
                    () => {
                        if (group == null) {
                            currentCount--;
                            currentCount = currentCount < 0 ? 0 : currentCount;
                            if (!tweenAlpha && onForwardFinish != null) {
                                onForwardFinish.Invoke();
                            }
                        }
                    });
            }
            #endregion

            #region 位置
            if (tweenPos) {
                if (tra is RectTransform) {
                    RectTransform rectTra = tra as RectTransform;
                    rectTra.anchoredPosition3D = pTo;
                    rectTra.anchoredPosition3D.ChangeVaule(pFrom, duration, (v) => rectTra.anchoredPosition3D = v, curve, ignoreTime,
                        () => {
                            currentCount--;
                            currentCount = currentCount < 0 ? 0 : currentCount;
                            if (!tweenColor && onReverseFinish != null) {
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
                        if (!tweenColor && onReverseFinish != null) {
                            onReverseFinish.Invoke();
                        }
                    });
            }
            #endregion

            #region 旋转
            if (tweenRotate) {
                tra.localEulerAngles = rTo;
                tra.localEulerAngles.ChangeVaule(rFrom, duration, (v) => tra.localEulerAngles = v, curve, ignoreTime,
                    () => {
                        currentCount--;
                        currentCount = currentCount < 0 ? 0 : currentCount;
                        if (!tweenPos && onReverseFinish != null) {
                            onReverseFinish.Invoke();
                        }
                    });
            }
            #endregion

            #region 缩放
            if (tweenScale) {
                tra.localScale = sTo;
                tra.localScale.ChangeVaule(sFrom, duration, (v) => tra.localScale = v, curve, ignoreTime,
                    () => {
                        currentCount--;
                        currentCount = currentCount < 0 ? 0 : currentCount;
                        if (!tweenScale && onReverseFinish != null) {
                            onReverseFinish.Invoke();
                        }
                    });
            }
            #endregion

            if (first&&loopCount!=1) {
                first = false;
                forwardAni.waitTime = delay + duration;
            }
        }, ignoreTime, null, loopCount, true);


        if (autoPlay) {
            PlayFroward();
        }
    }
}
