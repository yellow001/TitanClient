using NetFrame;
using ServerSimple.DTO.Fight;
using System;
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

    public void InitCompletedCREQ() {
        TransModel msg = new TransModel(1003003);
        MessageHandler.Send(msg);
    }

    public void MoveCREQ(MoveDataDTO dto) {
        TransModel model = new TransModel(1003005,0);
        model.SetMsg(dto);

        MessageHandler.Send(model);
    }

    public void RMBCREQ(MouseBtnDTO dto) {
        TransModel model = new TransModel(1003005, 1);

        model.SetMsg(dto);

        MessageHandler.Send(model);
    }

    public void LMBCREQ(MouseBtnDTO dto) {
        TransModel model = new TransModel(1003005, 2);
        model.SetMsg(dto);

        MessageHandler.Send(model);
    }

    public void RotateYCREQ(RotateYDTO dto) {
        TransModel model = new TransModel(1003005, 3);
        model.SetMsg(dto);

        MessageHandler.Send(model);
    }

    public void BoneRotateCREQ(RotateYDTO dto) {
        TransModel model = new TransModel(1003005, 4);
        model.SetMsg(dto);

        MessageHandler.Send(model);
    }

    public void ShootCREQ(ShootDTO dto) {

        TransModel model = new TransModel(1003007);
        model.SetMsg(dto);

        MessageHandler.Send(model);
    }

    public void DamageCREQ(DamageDTO dto) {

        TransModel model = new TransModel(1003009);
        model.SetMsg(dto);

        MessageHandler.Send(model);
    }
}
