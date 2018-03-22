using ServerSimple.DTO.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetFrame;
using System;
using ServerSimple.DTO.Login;
using System.Linq;

public class FightModel : BaseModel {

    public bool comSelf = false;

    public Dictionary<int, UserDTO> enterUserDTODic = new Dictionary<int, UserDTO>();

    public ServerSimple.DTO.Fight.BaseModel currentModel;
    public FightRoomDTO fightRoom {
        get;
        protected set;
    }

    public void OnFightRoomInitDataSRES(TransModel m) {
        fightRoom = m.GetMsg<FightRoomDTO>();

        int id = fightRoom.nameToModelID[LoginCtrl.Ins.model.GetUserName()];

        currentModel = fightRoom.baseModelDic[id];

        CallEvent(FightEvent.FightRoomInitData);
    }

    internal void OnInitCompleted(TransModel model) {

        enterUserDTODic = model.GetMsg<Dictionary<int, UserDTO>>();

        foreach (var item in enterUserDTODic.Values) {
            if (item.name.Equals(LoginCtrl.Ins.model.GetUserName())) {
                comSelf = true;
                break;
            }
        }

        CallEvent(FightEvent.InitCompleted, model.area,model.GetMsg<Dictionary<int, UserDTO>>());
    }

    internal void Fail() {
        //throw new NotImplementedException();
    }

    internal void KillBRO(TransModel model) {
        //throw new NotImplementedException();
    }

    internal void DamageBRO(TransModel model) {

        DamageDTO dto = model.GetMsg<DamageDTO>();

        fightRoom.baseModelDic[dto.DstID].hp -= dto.DamageValue;

        if (dto.DstID == currentModel.id) {
            CallEvent(FightEvent.DamageSelf);
        }

        CallEvent(FightEvent.Damage, model);
    }

    internal void ShootBRO(TransModel model) {
        CallEvent(FightEvent.Shoot, model);
    }

    /// <summary>
    /// 移动广播
    /// </summary>
    /// <param name="model"></param>
    internal void MoveBRO(TransModel model) {
        CallEvent(FightEvent.Move, model);
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

    /// <summary>
    /// 移动
    /// </summary>
    Move,

    /// <summary>
    /// 射击
    /// </summary>
    Shoot,

    /// <summary>
    /// 伤害
    /// </summary>
    Damage,

    /// <summary>
    /// 击杀
    /// </summary>
    Kill,

    /// <summary>
    /// 结束
    /// </summary>
    Over,


    /// <summary>
    /// 自己受到伤害
    /// </summary>
    DamageSelf,

    /// <summary>
    /// 初始化位置点面板
    /// </summary>
    InitPosPanel,
}