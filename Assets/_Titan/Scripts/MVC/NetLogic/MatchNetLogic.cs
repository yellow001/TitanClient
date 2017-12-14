using System;
using System.Collections;
using System.Collections.Generic;
using NetFrame;
using UnityEngine;

public class MatchNetLogic : BaseNetLogic {

    public MatchNetLogic() {
        RegisterNetEvent();
    }

    protected override void RegisterNetEvent() {
        //刷新反馈
        MessageHandler.Register(1002002, OnRefreshSRES);
        //创建房间反馈
        MessageHandler.Register(1002004, OnCreateSRES);
        //进入房间反馈（广播）
        MessageHandler.Register(1002006, OnEnterSRES);
        //离开房间反馈（广播）
        MessageHandler.Register(1002008, OnExitSRES);

        //开始游戏反馈（广播）
        MessageHandler.Register(1002011, OnStartSRES);
    }

    private void OnStartSRES(TransModel model) {
        MatchCtrl.Ins.model.OnStartSRES(model);
    }

    private void OnExitSRES(TransModel model) {
        MatchCtrl.Ins.model.OnExitSRES(model);
    }

    private void OnEnterSRES(TransModel model) {
        MatchCtrl.Ins.model.OnEnterSRES(model);
    }

    private void OnCreateSRES(TransModel model) {
        MatchCtrl.Ins.model.OnCreateSRES(model);
    }

    private void OnRefreshSRES(TransModel model) {
        MatchCtrl.Ins.model.OnRefreshSRES(model);
    }
    
}
