using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class RoomListPanel : BaseUI {

    [HideInInspector]
    public Button refreshBtn;
    [HideInInspector]
    public Button createBtn;
    [HideInInspector]
    public Text Text;
    [HideInInspector]
    public Image root;
    [HideInInspector]
    public Transform tra;
    [HideInInspector]
    public GridLayoutGroup content;

    TimeEvent refreshEvent;

    MatchModel model;

    List<RoomInfoItem> roomList=new List<RoomInfoItem>();

    public RoomInfoItem item;
    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init() {
        tra = transform;
        refreshBtn = tra.Find("refreshBtn").GetComponent<Button>();
        createBtn = tra.Find("createBtn").GetComponent<Button>();
        Text = tra.Find("refreshBtn/Text").GetComponent<Text>();
        root = GetComponent<Image>();
        content = tra.Find("Scroll View/Viewport/Content").GetComponent<GridLayoutGroup>();

        model = MatchCtrl.Ins.model;

        refreshEvent = new TimeEvent(2.5f, () => {
            MatchCtrl.Ins.RefreshCREQ();
        }, null, -1, true);

        base.Init();
    }

    public override void AddEvent() {
        refreshBtn.onClick.AddListener(()=> {
            refreshBtn.interactable = false;
            MatchCtrl.Ins.RefreshCREQ();
            this.AddTimeEvent(2.5f, ()=>refreshBtn.interactable = true, null);
        });

        createBtn.onClick.AddListener(()=> {
            this.CallEventList("openPwdWin",null);
        });

        model.BindEvent(MatchEvent.RefreshSRES, RefreshRoomList);
        
    }

    void RefreshRoomList(object[] args) {
        int result = (int)args[0];
        //1 可以刷新;-1 连接在某房间中，不用刷新;-2 连接未登录
        switch (result) {
            case 1:
                UpdateView();
                break;
            default:
                break;
        }
    }

    public override void UpdateView() {
        base.UpdateView();
        //Text.text="";
        //root.sprite=null;
        //ScrollView.sprite=null;
        //Viewport.sprite=null;
        //Handle.sprite=null;

        //更新列表项数目（多减少补）
        int count = model.roomList.Count - roomList.Count;
        if (count > 0) {
            while (count>0) {
                RoomInfoItem info = Instantiate(item.gameObject, content.transform, false).GetComponent<RoomInfoItem>();
                roomList.Add(info);
                count--;
            }
        }
        else if (count < 0) {
            while (count<0) {
                RoomInfoItem info = roomList[roomList.Count - 1];
                roomList.Remove(info);
                info.CloseAni();
                count++;
            }
        }
        

        //更新信息
        for (int i = 0; i < model.roomList.Count; i++) {
            roomList[i].Dto = model.roomList[i];
        }
    }

    public override void CloseAni() {
        this.RemoveTimeEvent(refreshEvent);
        base.CloseAni();
        //@CloseAni
    }

    public override void OpenAni() {
        base.OpenAni();
        //@OpenAni
        this.AddTimeEvent(refreshEvent);
    }
}
