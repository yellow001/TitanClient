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
            TweenAnimation ani = openTweens[i];
            if (group != null) {
                group.alpha = ani.aFrom;
                group.alpha.ChangeValue(ani.aTo, ani.duration, (v) => group.alpha = v,ani.curve);
            }
            transform.localPosition = ani.vFrom;
            transform.localPosition.ChangeVaule(ani.vTo, ani.duration, (v) => transform.localPosition = v,ani.curve);
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

        for (int i = 0; i < closeTweens.Length; i++) {
            TweenAnimation ani = closeTweens[i];
            if (group != null) {
                group.alpha = ani.aFrom;
                group.alpha.ChangeValue(ani.aTo, ani.duration, (v) => group.alpha = v,ani.curve);
            }
            transform.localPosition = ani.vFrom;
            transform.localPosition.ChangeVaule(ani.vTo, ani.duration, (v) => transform.localPosition = v,ani.curve);
        }
    }

    [System.Serializable]
    public class TweenAnimation {
        public float duration;
        public Vector3 vFrom, vTo;
        public float aFrom, aTo;
        public AnimationCurve curve=new AnimationCurve();
    }
}



