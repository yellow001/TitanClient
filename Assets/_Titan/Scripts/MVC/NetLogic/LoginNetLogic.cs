using NetFrame;
using ServerSimple.DTO.Login;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginNetLogic : BaseNetLogic {

    public LoginNetLogic() {
        RegisterNetEvent();
    }

    protected override void RegisterNetEvent() {
        //注册反馈
        MessageHandler.Register(1001002, OnRegisterSRES);
        //登录反馈
        MessageHandler.Register(1001004, OnLoginSRES);
    }

    void OnRegisterSRES(TransModel m) {
        LoginCtrl.Ins.model.OnRegosterSRES(m);
    }

    void OnLoginSRES(TransModel m) {
        LoginCtrl.Ins.model.OnRegosterSRES(m);
    }

    /// <summary>
    /// 注册请求
    /// </summary>
    public void RegisterCREQ() {
        TransModel msg = new TransModel(1001001);
        msg.SetMsg(LoginCtrl.Ins.model.data);
        NetIOH5.Ins.Send(msg);
    }

    /// <summary>
    /// 登录请求
    /// </summary>
    public void LoginCREQ() {
        TransModel msg = new TransModel(1001003);
        msg.SetMsg(LoginCtrl.Ins.model.data);
        NetIOH5.Ins.Send(msg);
    }

    
}
