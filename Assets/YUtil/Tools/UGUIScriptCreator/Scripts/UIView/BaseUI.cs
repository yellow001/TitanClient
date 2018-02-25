using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System;

public class BaseUI : MonoBehaviour {

    public AudioClip openClip;
    public AudioClip closeClip;

    [HideInInspector]
    public bool inited = false;


    [HideInInspector]
    public CanvasGroup group;

    DOTweenAnimation[] anis;

    public Action openTweenAction, closeTweenAction;

    //List<UGUITween> tweens = new List<UGUITween>();

    bool state;

    public void OnEnable() {
        if (!inited) {
            Init();
        }
        //tweens.Clear();
        //tweens.AddRange(GetComponents<UGUITween>());

        UpdateView();
        if (state != gameObject.activeSelf) {
            state = gameObject.activeSelf;
            OpenAni();
        }
        
        //Debug.Log("enable p");
    }

    public virtual void UpdateView() {
        if (!inited) {
            Init();
        }
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
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

        if (openTweenAction != null) {
            openTweenAction();
        }
    }

    private void OnDisable() {
        state = gameObject.activeSelf;
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
        if (closeTweenAction != null) {
            closeTweenAction();
        }
    }

   
}



