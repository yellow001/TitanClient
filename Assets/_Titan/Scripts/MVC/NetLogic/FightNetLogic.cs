using NetFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightNetLogic : BaseNetLogic {

    public FightNetLogic() {
        RegisterNetEvent();
    }

    protected override void RegisterNetEvent() {
        MessageHandler.Register(1003002,OnFightRoomInitDataSRES);//战斗房间初始化数据
    }

    void OnFightRoomInitDataSRES(TransModel m) {
        FightCtrl.Ins.model.OnRegisterSRES(m);
    }
}
