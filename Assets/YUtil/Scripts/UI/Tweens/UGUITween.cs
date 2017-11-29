using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGUITween : MonoBehaviour {
    public EM_TweenType tweenType = EM_TweenType.Open;
    public float duration=0.25f;
    public Vector3 posFrom, posTo;
    public Vector3 scaleFrom=Vector3.one, scaleTo=Vector3.one;
    public float aFrom=1f, aTo=1f;
    public AnimationCurve curve = new AnimationCurve();

    public bool tweenPos = false;
    public bool tweenScale = false;
    public bool tweenAlpha = false;

    [HideInInspector]
    public bool show = true;

    public enum EM_TweenType {
        Open,
        Close
    }
}
