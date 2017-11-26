using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class MsgAlert : BaseUI,IAlert<MsgUIModel>{

    [HideInInspector]
    public Text msg;
    [HideInInspector]
    public Image root;
    [HideInInspector]
    public Text title;
    [HideInInspector]
    public Button noBtn;
    [HideInInspector]
    public Button okBtn;
    [HideInInspector]
    public Transform tra;

    MsgUIModel model;

    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init() {
        tra = transform;
        msg = tra.Find("msg").GetComponent<Text>();
        root = GetComponent<Image>();
        title = tra.Find("title").GetComponent<Text>();
        noBtn = tra.Find("noBtn").GetComponent<Button>();
        okBtn = tra.Find("okBtn").GetComponent<Button>();

        group = GetComponent<CanvasGroup>();
        base.Init();
    }

    public override void AddEvent() {
        //noBtn.onClick.AddListener();
        //okBtn.onClick.AddListener();

    }

    public override void UpdateView() {

        if (model == null) { return; }

        base.UpdateView();
        msg.text=model.msg;
        //root.sprite=null;
        title.text=model.title;

        noBtn.onClick.RemoveAllListeners();
        okBtn.onClick.RemoveAllListeners();

        noBtn.onClick.AddListener(()=>model.no_de());
        okBtn.onClick.AddListener(() => model.ok_de());
    }

    public override void CloseAni() {
        base.CloseAni();
        //@CloseAni
        DOTween.To(() => group.alpha, x => group.alpha = x, 0, 0.25f).OnComplete(()=> {
            gameObject.SetActive(false);
        }).Play();
    }

    public override void OpenAni() {
        base.OpenAni();
        //@OpenAni
        DOTween.To(() => group.alpha, x => group.alpha = x, 1, 0.25f).Play();
    }

    public void SetAlertWin(MsgUIModel m) {
        model = m;
        model.no_de +=()=> CloseAni();
        model.ok_de += () => CloseAni();
        gameObject.SetActive(true);
    }
}
