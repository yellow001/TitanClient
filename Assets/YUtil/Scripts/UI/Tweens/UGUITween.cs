using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UGUITween : MonoBehaviour {
    public EM_TweenTime tweenType = EM_TweenTime.Open;
    public float duration=0.25f;
    //public Vector3 posFrom, posTo;
    //public Vector3 scaleFrom=Vector3.one, scaleTo=Vector3.one;
    //public float aFrom=1f, aTo=1f;
    public AnimationCurve curve = new AnimationCurve();

    //public bool tweenPos = false;
    //public bool tweenScale = false;
    //public bool tweenAlpha = false;
    public bool autoPlay = true;

    #if UNITY_EDITOR
    [HideInInspector]
    public bool show = true;
#endif

    public virtual void Play() {

    }

    public enum EM_TweenTime {
        Open,
        Close
    }

    public enum EM_TweenType {
        Alpha,
        Position,
        Scale,
        Rotation
    }
}
