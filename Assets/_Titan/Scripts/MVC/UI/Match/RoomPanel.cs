﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class RoomPanel : BaseUI {

    [HideInInspector]
    public Image grid;
    [HideInInspector]
    public Image bgEffect;
    [HideInInspector]
    public Button exitBtn;
    [HideInInspector]
    public Image root;
    [HideInInspector]
    public Text Text;
    [HideInInspector]
    public Button startBtn;
    [HideInInspector]
    public Transform tra;

    List<UserItem> itemList=new List<UserItem>();
    public UserItem item;
    MatchModel model;
    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init() {
        tra = transform;
        grid = tra.Find("grid").GetComponent<Image>();
        bgEffect = tra.Find("bgEffect").GetComponent<Image>();
        exitBtn = tra.Find("exitBtn").GetComponent<Button>();
        root = GetComponent<Image>();
        Text = tra.Find("startBtn/Text").GetComponent<Text>();
        startBtn = tra.Find("startBtn").GetComponent<Button>();

        model = MatchCtrl.Ins.model;
        base.Init();
    }

    public override void AddEvent() {
        exitBtn.onClick.AddListener(() => {
            MatchCtrl.Ins.ExitCREQ();
        });
        //startBtn.onClick.AddListener();

        model.BindEvent("ExitSRES", ExitSRES);
    }

    void ExitSRES(object[] args) {
        int result = (int)args[0];
        switch (result) {
            case 1:
                CloseAni();
                this.InvokeDeList("openRoomList");
                break;
            default:
                break;
        }
    }

    public override void UpdateView() {
        base.UpdateView();
        //grid.sprite=null;
        //bgEffect.sprite=null;
        //root.sprite=null;
        //Text.text="";
        if (model.currentRoom == null) {
            //返回房间列表界面
            CloseAni();
            this.InvokeDeList("openRoomList");
            return;
        }

        //todo 刷新列表
        //更新列表项数目（多减少补）
        int count = model.currentRoom.playerList.Count - itemList.Count;
        if (count > 0) {
            while (count > 0) {
                UserItem info = Instantiate(item.gameObject, grid.transform, false).GetComponent<UserItem>();
                itemList.Add(info);
                count--;
            }
        }
        else if (count < 0) {
            while (count < 0) {
                UserItem info = itemList[itemList.Count - 1];
                itemList.Remove(info);
                Destroy(info.gameObject);
                count++;
            }
        }


        //更新信息
        for (int i = 0; i < model.currentRoom.playerList.Count; i++) {
            itemList[i].uname = model.currentRoom.playerList[i];
            itemList[i].UpdateView();
        }

        //是房主的话 开始游戏按钮 可用
        if (model.currentRoom.masterName.Equals(LoginCtrl.Ins.model.GetUserName())) {
            startBtn.gameObject.SetActive(true);
        }
        else {
            startBtn.gameObject.SetActive(false);
        }

    }

    public override void CloseAni() {
        base.CloseAni();
        //@CloseAni

        while (itemList.Count>0) {
            UserItem info = itemList[itemList.Count - 1];
            itemList.Remove(info);
            Destroy(info.gameObject);
        }
    }

    public override void OpenAni() {
        base.OpenAni();
        //@OpenAni
    }
}
