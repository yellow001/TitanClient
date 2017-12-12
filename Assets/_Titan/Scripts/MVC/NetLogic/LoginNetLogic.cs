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
        LoginCtrl.Ins.model.OnRegisterSRES(m);
    }

    void OnLoginSRES(TransModel m) {
        LoginCtrl.Ins.model.OnLoginSRES(m);
    }

}
