using NetFrame;
using ServerSimple.DTO.Login;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginCtrl : BaseCtrl<LoginCtrl> {
    public UserModel model;

    LoginNetLogic netLogic;

    public LoginCtrl() {
        Init();
    }

    public void Init() {
        model = new UserModel();
        netLogic = new LoginNetLogic();
    }

    public void LoginCREQ(UserDTO dto) {
        TransModel msg = new TransModel(1001001);
        msg.SetMsg(dto);
        MessageHandler.Send(msg);
    }

    public void RegisterCREQ(UserDTO dto) {
        TransModel msg = new TransModel(1001003);
        msg.SetMsg(dto);
        MessageHandler.Send(msg);
    }
}
