using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchUICtrl : MonoBehaviour {
    public RoomListPanel roomList;
    public PwdWin pwdWin;
    public RoomPanel roomPanel;
    public GameObject waitMask;

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
    }

    void CreateSRES(object[] args) {
        //1 创建成功;-1 连接已在房间中;-2 连接未登录;-3 获取房间出错
        int result = (int)args[0];
        switch (result) {
            case 1:
                this.InvokeDeList("openRoomPanel");
                break;
            default:
                break;
        }
    }
}
