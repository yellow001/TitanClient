using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCtrl : BaseCtrl<FightCtrl> {
    public FightModel model;
    FightNetLogic netLogic;

    public FightCtrl() {
        Init();
    }

    void Init() {
        model = new FightModel();
        netLogic = new FightNetLogic();
    }
}
