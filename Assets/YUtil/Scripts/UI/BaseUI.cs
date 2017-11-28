using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class BaseUI : MonoBehaviour {

    public AudioClip openClip;
    public AudioClip closeClip;

    [HideInInspector]
    public bool inited = false;
    

    [HideInInspector]
    public CanvasGroup group;

    DOTweenAnimation[] anis;

    [HideInInspector]
    public List<TweenAnimation> openTweens;
    [HideInInspector]
    public List<TweenAnimation> closeTweens;

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
            AudioMgr.Ins.Play(openClip);
        }

        if (anis != null && anis.Length > 0) {
            for (int i = 0; i < anis.Length; i++) {
                if (anis[i].id.Equals("open")) {
                    anis[i].tween.Restart();
                }
            }
        }

        PlayTweenAni(openTweens);
    }

    public virtual void CloseAni() {
        if (closeClip != null)
        {
            AudioMgr.Ins.Play(closeClip);
        }

        if (anis != null && anis.Length > 0) {
            for (int i = 0; i < anis.Length; i++) {
                if (anis[i].id.Equals("close")) {
                    anis[i].tween.Restart();
                }
            }
        }

        float hideTime = 0;
        foreach (var item in closeTweens) {
            if (hideTime < item.duration) {
                hideTime = item.duration;
            }
        }
        PlayTweenAni(closeTweens);

        this.AddTimeEvent(hideTime, () => gameObject.SetActive(false),null);
    }

    public void PlayTweenAni(List<TweenAnimation> tweens) {

        for (int i = 0; i < tweens.Count; i++) {

            TweenAnimation ani = tweens[i];
            
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
            scaleFrom = Vector3.one;
            scaleTo=Vector3.one;
            aFrom = 1;
            aTo=1;
    }
    }
}



