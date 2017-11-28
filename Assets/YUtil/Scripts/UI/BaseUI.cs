using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class BaseUI : MonoBehaviour {

    public AudioClip openClip;
    public AudioClip closeClip;

    [HideInInspector]
    public bool inited = false;

    [HideInInspector]
    public AudioSource au;

    [HideInInspector]
    public CanvasGroup group;

    DOTweenAnimation[] anis;

    public TweenAnimation[] openTweens;
    public TweenAnimation[] closeTweens;

    public void OnEnable() {
        if (!inited) {
            Init();
        }
        UpdateView();
        OpenAni();
        //Debug.Log("enable p");
    }
    
    public virtual void UpdateView() {
        if (!inited) {
            Init();
        }
        au = GetComponent<AudioSource>();
        au.loop = false;
        au.playOnAwake = false;
    }

    public virtual void Init() {
        AddEvent();
        inited = true;
        anis = GetComponentsInChildren<DOTweenAnimation>();
        group = GetComponent<CanvasGroup>();
    }

    public virtual void AddEvent() {
        
    }

    public virtual void OpenAni() {
        if (openClip != null) {
            au.clip = openClip;
            au.Play();
        }

        if (anis != null && anis.Length > 0) {
            for (int i = 0; i < anis.Length; i++) {
                if (anis[i].id.Equals("open")) {
                    anis[i].tween.Restart();
                }
            }
        }

        for (int i = 0; i < openTweens.Length; i++) {
            TweenAnimation ani = closeTweens[i];

            if (group != null && ani.tweenAlpha) {
                group.alpha = ani.aFrom;
                group.alpha.ChangeValue(ani.aTo, ani.duration, (v) => group.alpha = v, ani.curve);
            }

            if (ani.tweenPos) {
                transform.localPosition = ani.posFrom;
                transform.localPosition.ChangeVaule(ani.posTo, ani.duration, (v) => transform.localPosition = v, ani.curve);
            }

            if (ani.tweenScale) {
                transform.localScale = ani.scaleFrom;
                transform.localScale.ChangeVaule(ani.scaleTo, ani.duration, (v) => transform.localScale = v, ani.curve);
            }
        }
    }

    public virtual void CloseAni() {
        if (closeClip != null)
        {
            au.clip = closeClip;
            au.Play();
        }

        if (anis != null && anis.Length > 0) {
            for (int i = 0; i < anis.Length; i++) {
                if (anis[i].id.Equals("close")) {
                    anis[i].tween.Restart();
                }
            }
        }

        float hideTime = 0;

        for (int i = 0; i < closeTweens.Length; i++) {
            
            TweenAnimation ani = closeTweens[i];

            if (hideTime < ani.duration) {
                hideTime = ani.duration;
            }

            if (group != null&&ani.tweenAlpha) {
                group.alpha = ani.aFrom;
                group.alpha.ChangeValue(ani.aTo, ani.duration, (v) => group.alpha = v,ani.curve);
            }

            if (ani.tweenPos) {
                transform.localPosition = ani.posFrom;
                transform.localPosition.ChangeVaule(ani.posTo, ani.duration, (v) => transform.localPosition = v, ani.curve);
            }

            if (ani.tweenScale) {
                transform.localScale = ani.scaleFrom;
                transform.localScale.ChangeVaule(ani.scaleTo, ani.duration, (v) => transform.localScale = v, ani.curve);
            }
            
        }

        this.AddTimeEvent(hideTime, () => gameObject.SetActive(false),null);
    }

    [System.Serializable]
    public class TweenAnimation {
        public float duration;
        public Vector3 posFrom, posTo;
        public Vector3 scaleFrom, scaleTo;
        public float aFrom, aTo;
        public AnimationCurve curve=new AnimationCurve();

        public bool tweenPos = false;
        public bool tweenScale = false;
        public bool tweenAlpha = false;

        public TweenAnimation() {
            duration = 0.25f;
            posFrom = Vector3.zero;
            posTo=Vector3.zero;
            scaleFrom = Vector3.zero;
            scaleTo=Vector3.zero;
            aFrom = 1;
            aTo=1;
    }
    }
}



