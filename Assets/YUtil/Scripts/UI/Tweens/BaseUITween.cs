using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUITween : UTweenAll {

    public EM_PlayTime playTime = EM_PlayTime.Open;

    public bool doubleTween = true;

    BaseUI baseUI;

    public override void Start() {
        autoPlay = false;
        loopCount = 1;
        baseUI = GetComponentInParent<BaseUI>();
        if (baseUI != null) {
            switch (playTime) {
                case EM_PlayTime.Open:
                    baseUI.openTweenAction += PlayFroward;
                    if (doubleTween) {
                        baseUI.closeTweenAction += PlayReverse;
                    }
                    break;
                case EM_PlayTime.Close:
                    baseUI.closeTweenAction += PlayFroward;
                    if (doubleTween) {
                        baseUI.openTweenAction += PlayReverse;
                    }
                    break;
                default:
                    break;
            }
        }
        base.Start();
    }
    
    public enum EM_PlayTime {
        Open,
        Close
    }
}
