using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

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
    [HideInInspector]
    public Text roomInfo;

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
        roomInfo = tra.Find("roomInfo").GetComponent<Text>();

        model = MatchCtrl.Ins.model;
        base.Init();
    }

    public override void AddEvent() {
        exitBtn.onClick.AddListener(() =>MatchCtrl.Ins.ExitCREQ());

        startBtn.onClick.AddListener(()=>MatchCtrl.Ins.StartCREQ());

        model.BindEvent(MatchEvent.EnterSRES, EnterSRES);
        model.BindEvent(MatchEvent.ExitSRES, ExitSRES);
    }
    
    private void EnterSRES(object[] args) {
        int result = (int)args[0];
        switch (result) {
            case 1:
                UpdateView();
                break;
            default:
                break;
        }
    }

    void ExitSRES(object[] args) {
        //"1  离开成功;- 1 不在房间中;- 2 连接未登录;- 3 连接不在该房间中;0有人离开房间"
        int result = (int)args[0];
        switch (result) {
            case 1:
                CloseAni();
                this.CallEventList("openRoomList");
                break;
            case 0:
                UpdateView();
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
            this.CallEventList("openRoomList");
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
                info.CloseAni();
                count++;
            }
        }


        //更新信息
        for (int i = 0; i < model.currentRoom.playerList.Count; i++) {
            itemList[i].currentDTO = model.currentRoom.playerList[i];
            itemList[i].UpdateView();
        }

        //是房主的话 开始游戏按钮 可用
        if (model.currentRoom.masterName.Equals(LoginCtrl.Ins.model.GetUserName())) {
            startBtn.gameObject.SetActive(true);
        }
        else {
            startBtn.gameObject.SetActive(false);
        }

        //只有一个人时不可以开始游戏
        if (model.currentRoom.playerList.Count <= 1) {
            startBtn.interactable = false;
        }
        else {
            startBtn.interactable = true;
        }

        roomInfo.text = string.Format("房间号：{0}    房主：{1}", model.currentRoom.id, model.currentRoom.masterName);
    }

    public override void CloseAni() {
        base.CloseAni();
        //@CloseAni

        while (itemList.Count>0) {
            UserItem info = itemList[itemList.Count - 1];
            itemList.Remove(info);
            info.CloseAni();
        }
    }

    public override void OpenAni() {
        base.OpenAni();
        //@OpenAni
    }
}
