using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlertMgr : BaseManager<AlertMgr> {

    public GameObject msgAlert;
    public GameObject tipAlert;

    public Queue<MsgUIModel> msgModels;
    public Queue<TipUIModel> tipModels;

    public override void Init() {
        base.Init();
        SceneManager.sceneLoaded += OnLoadScene;
        if (msgAlert != null) { msgAlert.SetActive(false); }
        if (tipAlert != null) { tipAlert.SetActive(false); }
    }

    public void Update() {
        if (msgModels != null && msgModels.Count > 0 && !msgAlert.activeInHierarchy) {
            msgAlert.GetComponent<MsgAlert>().SetAlertWin(msgModels.Dequeue());
        }

        if (tipModels != null && tipModels.Count > 0 && !tipAlert.activeInHierarchy) {
            tipAlert.GetComponent<TipAlert>().SetAlertWin(tipModels.Dequeue());
        }
    }

    private void OnLoadScene(Scene arg0, LoadSceneMode arg1) {
        msgModels.Clear();
        tipModels.Clear();
        msgAlert.SetActive(false);
        tipAlert.SetActive(false);
    }

    public void AddMsg(MsgUIModel m) {
        if (msgModels == null) { msgModels = new Queue<MsgUIModel>(); }
        msgModels.Enqueue(m);
    }
    public void AddTip(TipUIModel m) {
        if (tipModels == null) { tipModels = new Queue<TipUIModel>(); }
        tipModels.Enqueue(m);
    }
    
}
