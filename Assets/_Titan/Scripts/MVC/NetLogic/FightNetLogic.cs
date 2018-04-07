using NetFrame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightNetLogic : BaseNetLogic {

    public FightNetLogic() {
        RegisterNetEvent();
    }

    protected override void RegisterNetEvent() {
        MessageHandler.Register(1003002,OnFightRoomInitDataSRES);//战斗房间初始化数据

        MessageHandler.Register(1003004, OnInitCompletedBRO);//某玩家加载场景数据完毕

        MessageHandler.Register(1003006, MoveBRO);//移动广播

        MessageHandler.Register(1003008, ShootBRO);//射击广播

        MessageHandler.Register(1003010, DamageBRO);//伤害广播

        MessageHandler.Register(1003011, KillBRO);//击杀广播

        MessageHandler.Register(10039998, FailSRES);//失败反馈

        MessageHandler.Register(10039999, SucceedSRES);//成功反馈
    }

    private void FailSRES(TransModel model) {
        FightCtrl.Ins.model.Fail();
    }

    private void SucceedSRES(TransModel model) {
        FightCtrl.Ins.model.Succeed();
    }

    private void KillBRO(TransModel model) {
        FightCtrl.Ins.model.KillBRO(model);
    }

    private void DamageBRO(TransModel model) {
        FightCtrl.Ins.model.DamageBRO(model);
    }

    private void ShootBRO(TransModel model) {
        FightCtrl.Ins.model.ShootBRO(model);
    }

    private void MoveBRO(TransModel model) {
        FightCtrl.Ins.model.MoveBRO(model);
    }

    private void OnInitCompletedBRO(TransModel model) {
        FightCtrl.Ins.model.OnInitCompleted(model);
    }

    void OnFightRoomInitDataSRES(TransModel m) {
        FightCtrl.Ins.model.OnFightRoomInitDataSRES(m);
    }
}
