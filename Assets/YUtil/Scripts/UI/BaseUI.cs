using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

public class BaseUI : MonoBehaviour {

    public AudioClip openClip;
    public AudioClip closeClip;

    [HideInInspector]
    public bool inited = false;


    [HideInInspector]
    public CanvasGroup group;

    DOTweenAnimation[] anis;

    List<UGUITween> tweens = new List<UGUITween>();

    public void OnEnable() {
        if (!inited) {
            Init();
        }
        tweens.Clear();
        tweens.AddRange(GetComponents<UGUITween>());

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

        PlayTweenAni(tweens.FindAll((t) => {
            return t.tweenType == UGUITween.EM_TweenType.Open;
        }));
    }

    public virtual void CloseAni() {
        if (closeClip != null) {
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

        List<UGUITween> closeTweens = tweens.FindAll((t) => {
            return t.tweenType == UGUITween.EM_TweenType.Close;
        });

        foreach (var item in closeTweens) {
            if (hideTime < item.duration) {
                hideTime = item.duration;
            }
        }

        PlayTweenAni(closeTweens);

        this.AddTimeEvent(hideTime, () => gameObject.SetActive(false), null);
    }

    public void PlayTweenAni(List<UGUITween> tweens) {

        for (int i = 0; i < tweens.Count; i++) {

            UGUITween ani = tweens[i];

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
}



