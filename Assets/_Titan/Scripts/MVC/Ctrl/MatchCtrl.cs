﻿using NetFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchCtrl : BaseCtrl<MatchCtrl> {
    public MatchModel model;
    MatchNetLogic netLogic;

    public MatchCtrl() {
        Init();
    }

    void Init() {
        model = new MatchModel();
        netLogic = new MatchNetLogic();
    }

    public void RefreshCREQ() {
        TransModel model = new TransModel(1002001);
        MessageHandler.Send(model);
    }

    public void CreateCREQ(string passwd) {
        TransModel model = new TransModel(1002003);
        if (!string.IsNullOrEmpty(passwd)) {
            model.SetMsg(passwd);
        }
        MessageHandler.Send(model);
    }

    public void EnterCREQ(long index,string pwd) {
        TransModel model = new TransModel(1002005,(int)index);
        model.SetMsg(pwd);
        MessageHandler.Send(model);
    }

    public void ExitCREQ() {
        TransModel model = new TransModel(1002007);
        MessageHandler.Send(model);
    }

    public void RemoveCREQ(string n) {
        TransModel model = new TransModel(1002009);
        model.SetMsg(n);
        MessageHandler.Send(model);
    }

    public void StartCREQ() {
        TransModel model = new TransModel(1002010);
        MessageHandler.Send(model);
    }
}
