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

    FightModel fightModel;

    AsyncOperation op;
    // Use this for initialization
    void Start () {
        model = MatchCtrl.Ins.model;
        fightModel = FightCtrl.Ins.model;//初始化
        AddEvent();
	}

    void AddEvent() {
        this.AddEventFun("openPwdWin",OpenPwdWin);

        this.AddEventFun("openRoomList",OpenRoomList);

        this.AddEventFun("openRoomPanel",OpenRoomPanel);

        model.BindEvent(MatchEvent.CreateSRES, CreateSRES);
        model.BindEvent(MatchEvent.EnterSRES, EnterSRES);
        model.BindEvent(MatchEvent.StartSRES, StartSRES);

        fightModel.BindEvent(FightEvent.FightRoomInitData, InitFightRoomData);
    }

    private void OnDestroy() {
        this.RemoveEventFun("openPwdWin", OpenPwdWin);

        this.RemoveEventFun("openRoomList", OpenRoomList);

        this.RemoveEventFun("openRoomPanel", OpenRoomPanel);

        model.UnBindEvent(MatchEvent.CreateSRES, CreateSRES);
        model.UnBindEvent(MatchEvent.EnterSRES, EnterSRES);
        model.UnBindEvent(MatchEvent.StartSRES, StartSRES);

        fightModel.UnBindEvent(FightEvent.FightRoomInitData, InitFightRoomData);
    }

    void OpenPwdWin(object[] args) {
        pwdWin.info = args == null ? null : (RoomInfoItem)args[0];
        pwdWin.gameObject.SetActive(true);
    }

    void OpenRoomList(object[] args) {
        roomList.gameObject.SetActive(true);
    }

    void OpenRoomPanel(object[] args) {
        roomList.CloseAni();
        roomPanel.gameObject.SetActive(true);
    }

    void EnterSRES(object[] args) {
        //1 进入房间成功;-1 房间不存在或已过期;-2 连接还在某房间中;-3 连接未登录;-4 密码错误;-5 人数已满;-6 进入房间出错
        int result = (int)args[0];
        switch (result) {
            case 1:
                if (!roomPanel.gameObject.activeSelf) {
                    this.CallEventList("openRoomPanel");
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
                this.CallEventList("openRoomPanel");
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
                //显示等待面板但不加载场景(收到战斗房间初始化信息时才加载场景)
                waitMask.SetActive(true);
                
                break;
            default:
                Debug.Log(result);
                break;
        }
    }

    private void Update() {
        if (op != null) {
            progressTx.text = (int)(op.progress * 100+9) + "%";
        }
    }

    private void InitFightRoomData(object[] args) {
        op = SceneManager.LoadSceneAsync("Fight");
        progressTx.text = (int)(op.progress * 100) + "%";
    }
}
