﻿using ServerSimple.DTO.Login;
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
	// Use this for initialization
	void Start () {
        fightModel = FightCtrl.Ins.model;

        fightModel.BindEvent(FightEvent.InitCompleted, (args) => {

            InitCompleted((int)args[0]);

        });
	}

    // Update is called once per frame
    void Update () {
		
	}


    void InitCompleted(int all) {
        if (all == 1) {
            //所有人加载完毕，可以开始游戏
            //todo 关闭ui
            this.CallEventList(FightEvent.AllInitCompleted.ToString());
            waitMaskPanel.SetActive(false);
            waitUserPanel.SetActive(false);
            Debug.Log("over");
        }

        if (fightModel.comSelf) {
            tipText.text = "等待其他玩家";
        }
    }
}
