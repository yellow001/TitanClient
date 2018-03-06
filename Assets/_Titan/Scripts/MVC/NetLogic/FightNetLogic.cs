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
    }

    private void OnInitCompletedBRO(TransModel model) {
        FightCtrl.Ins.model.OnInitCompleted(model);
    }

    void OnFightRoomInitDataSRES(TransModel m) {
        FightCtrl.Ins.model.OnFightRoomInitDataSRES(m);
    }
}
