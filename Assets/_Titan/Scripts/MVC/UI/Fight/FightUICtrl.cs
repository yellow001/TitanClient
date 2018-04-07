using ServerSimple.DTO.Login;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightUICtrl : MonoBehaviour {

    FightModel fightModel;


    public Text tipText;

    public GameObject waitMaskPanel;
    public GameObject waitUserPanel;

    public OverPanel overPanel;
	// Use this for initialization
	void Start () {
        fightModel = FightCtrl.Ins.model;

        fightModel.BindEvent(FightEvent.InitCompleted,InitCompleted);

        fightModel.BindEvent(FightEvent.Over,Over);
    }

    private void OnDestroy() {
        fightModel.UnBindEvent(FightEvent.InitCompleted, InitCompleted);
        fightModel.UnBindEvent(FightEvent.Over, Over);
    }

    // Update is called once per frame
    void Update () {
		
	}


    void InitCompleted(object[] args) {
        int all = (int)args[0];
        if (all == 1) {
            //所有人加载完毕，可以开始游戏（等待2秒再开始）
            //todo 关闭ui
            this.AddTimeEvent(2, () => {
                this.CallEventList(FightEvent.AllInitCompleted.ToString());
                waitMaskPanel.SetActive(false);
                waitUserPanel.SetActive(false);
                Debug.Log("over");
            }, null);
        }

        if (fightModel.comSelf) {
            tipText.text = "等待其他玩家";
        }
    }

    void Over(object[] args) {
        overPanel.gameObject.SetActive(true);
        overPanel.SetResult((bool)args[0]);

        this.CallEventList(FightEvent.Over.ToString());
    }
}
