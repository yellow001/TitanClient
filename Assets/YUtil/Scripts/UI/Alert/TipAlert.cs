using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class TipAlert : BaseUI,IAlert<TipUIModel> {

    [HideInInspector]
    public Text tip;
    [HideInInspector]
    public Image root;
    [HideInInspector]
    public Transform tra;

    TipUIModel model;

    CanvasGroup group;

    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init() {
        tra = transform;
        tip = tra.Find("tip").GetComponent<Text>();
        root = GetComponent<Image>();
        group = GetComponent<CanvasGroup>();
        base.Init();
    }

    public override void AddEvent() {

    }

    public override void UpdateView() {
        if (model == null) { return; }
        base.UpdateView();
        tip.text=model.msg;
        //root.sprite=null;

    }

    public override void CloseAni() {
        base.CloseAni();
        //@CloseAni
        DOTween.To(() => group.alpha, x => group.alpha = x, 0, 0.2f).OnComplete(() => {
            gameObject.SetActive(false);
        }).Play();
    }

    public override void OpenAni() {
        base.OpenAni();
        //@OpenAni
        DOTween.To(() => group.alpha, x => group.alpha = x, 1, 0.2f).Play();
        Debug.Log(model.duration);
        this.AddTimeEvent(model.duration, () => { CloseAni();Debug.Log("close"); }, null);
    }

    public void SetAlertWin(TipUIModel m) {
        model = m;
        gameObject.SetActive(true);
    }
}
