using NetFrame;
using ServerSimple.DTO.Match;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MatchModel : BaseModel {

    public List<MatchRoomDTO> roomList {
        get;
        protected set;
    } = new List<MatchRoomDTO>();

    public MatchRoomDTO currentRoom {
        get;
        protected set;
    }

    //1 可以刷新;-1 连接在某房间中，不用刷新;-2 连接未登录
    public void OnRefreshSRES(TransModel model) {
        if (model.area == 1) {
            roomList.Clear();
            if (model.GetMsg<MatchRoomDTO[]>() != null) {
                roomList.AddRange(model.GetMsg<MatchRoomDTO[]>());
            }
        }

        CallEvent("RefreshSRES", model.area);
    }

    internal void OnStartSRES(TransModel model) {
        CallEvent("StartSRES", model.area);
    }

    //1 创建成功;-1 连接已在房间中;-2 连接未登录;-3 获取房间出错
    public void OnCreateSRES(TransModel model) {
        if (model.area == 1) {
            currentRoom = model.GetMsg<MatchRoomDTO>();
        }
        CallEvent("CreateSRES", model.area);
    }


    public void OnEnterSRES(TransModel model) {
        //1 进入房间成功;-1 房间不存在或已过期;-2 连接还在某房间中;-3 连接未登录;-4 密码错误;-5 人数已满;-6 进入房间出错
        if (model.area == 1) {
            currentRoom = model.GetMsg<MatchRoomDTO>();
        }
        CallEvent("EnterSRES", model.area);
    }

    public void OnExitSRES(TransModel model) {
        //"1  离开成功;- 1 不在房间中;- 2 连接未登录;- 3 连接不在该房间中;0有人离开房间"
        if (model.area == 0) {
            currentRoom = model.GetMsg<MatchRoomDTO>();
        }
        CallEvent("ExitSRES", model.area);
    }
}
