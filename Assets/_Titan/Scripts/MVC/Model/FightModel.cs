using ServerSimple.DTO.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetFrame;
using System;
using ServerSimple.DTO.Login;

public class FightModel : BaseModel {

    public bool comSelf = false;

    public FightRoomDTO fightRoom {
        get;
        protected set;
    }

    public void OnFightRoomInitDataSRES(TransModel m) {
        fightRoom = m.GetMsg<FightRoomDTO>();
        CallEvent(FightEvent.FightRoomInitData);
    }

    internal void OnInitCompleted(TransModel model) {
        List<UserDTO> dtos = model.GetMsg<List<UserDTO>>();

        foreach (var item in dtos) {
            if (item.name.Equals(LoginCtrl.Ins.model.GetUserName())) {
                comSelf = true;
                break;
            }
        }

        CallEvent(FightEvent.InitCompleted, model.area,model.GetMsg<List<UserDTO>>());
    }
}


public enum FightEvent {

    /// <summary>
    /// 战斗房间数据初始化
    /// </summary>
    FightRoomInitData,

    /// <summary>
    /// 加载完毕
    /// </summary>
    InitCompleted,

    /// <summary>
    /// 所有人加载完毕，开始战斗
    /// </summary>
    AllInitCompleted,

}