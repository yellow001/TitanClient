﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchUICtrl : MonoBehaviour {
    public RoomListPanel roomList;
    public PwdWin pwdWin;
    public RoomPanel roomPanel;
    public GameObject waitMask;

    public Text progressTx;
    MatchModel model;
	// Use this for initialization
	void Start () {
        model = MatchCtrl.Ins.model;
        AddEvent();
	}

    void AddEvent() {
        this.AddEventFun("openPwdWin", (args) => {
            pwdWin.info = args==null?null:(RoomInfoItem)args[0];
            pwdWin.gameObject.SetActive(true);
        });

        this.AddEventFun("openRoomList", (args) => {
            roomList.gameObject.SetActive(true);
        });

        this.AddEventFun("openRoomPanel", (args) => {
            roomList.CloseAni();
            roomPanel.gameObject.SetActive(true);
        });

        model.BindEvent("CreateSRES", CreateSRES);
        model.BindEvent("EnterSRES", EnterSRES);
        model.BindEvent("StartSRES", StartSRES);

    }

    void EnterSRES(object[] args) {
        //1 进入房间成功;-1 房间不存在或已过期;-2 连接还在某房间中;-3 连接未登录;-4 密码错误;-5 人数已满;-6 进入房间出错
        int result = (int)args[0];
        switch (result) {
            case 1:
                if (!roomPanel.gameObject.activeSelf) {
                    this.InvokeDeList("openRoomPanel");
                }
                break;
            case -1:
                this.AddMsg("房间已过期");
                break;
            case -4:
                this.AddMsg("密码错误");
                break;
            case -5:
                this.AddMsg("人数已满");
                break;
            default:
                break;
        }
    }

    void CreateSRES(object[] args) {
        //1 创建成功;-1 连接已在房间中;-2 连接未登录;-3 获取房间出错
        int result = (int)args[0];
        switch (result) {
            case 1:
                this.InvokeDeList("openRoomPanel");
                this.AddTimeEvent(0.2f, () => roomPanel.UpdateView(),null);
                break;
            default:
                break;
        }
    }

    private void StartSRES(object[] args) {
        int result = (int)args[0];
        switch (result) {
            case 1:
                //todo 开始游戏(加载场景)
                waitMask.SetActive(true);

                AsyncOperation async= SceneManager.LoadSceneAsync("Fight");
                progressTx.text = (int)(async.progress * 100) + "%";
                break;
            default:
                Debug.Log(result);
                break;
        }
    }
}