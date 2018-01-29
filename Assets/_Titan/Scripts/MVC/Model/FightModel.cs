using ServerSimple.DTO.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetFrame;
using System;

public class FightModel : BaseModel {
    public FightRoomDTO fightRoom {
        get;
        protected set;
    }

    public void OnRegisterSRES(TransModel m) {
        fightRoom = m.GetMsg<FightRoomDTO>();
        CallEvent("OnFightRoomInitDataSRES");
    }
}
