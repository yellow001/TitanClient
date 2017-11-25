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

    RectTransform staticCanvas;

    public override void Init() {
        base.Init();
        LoadAlert();
        SceneManager.sceneLoaded += OnLoadScene;
        if (msgAlert != null) { msgAlert.SetActive(false); }
        if (tipAlert != null) { tipAlert.SetActive(false); }
    }

    void LoadAlert() {
        if (GameObject.Find("StaticCanvas") == null) {
            GameObject canvas = ResMgr.Ins.GetAsset<GameObject>(EM_ResType.StaticCanvas);
            staticCanvas = Instantiate(canvas).transform as RectTransform;
            staticCanvas.SetParent(transform, false);
        }
        else {
            staticCanvas = GameObject.Find("StaticCanvas").transform as RectTransform;
        }

        if (msgAlert == null) {
            GameObject obj = ResMgr.Ins.GetAsset<GameObject>(EM_ResType.MsgAlert);
            RectTransform tra = Instantiate(obj).transform as RectTransform;
            msgAlert = tra.gameObject;
            tra.SetParent(staticCanvas, false);
        }

        if (tipAlert == null) {
            GameObject obj = ResMgr.Ins.GetAsset<GameObject>(EM_ResType.TipAlert);
            RectTransform tra = Instantiate(obj).transform as RectTransform;
            tipAlert = tra.gameObject;
            tra.SetParent(staticCanvas, false);
        }
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
