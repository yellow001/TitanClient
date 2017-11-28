using ServerSimple.DTO.Login;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginCtrl : BaseCtrl<LoginCtrl> {
    public UserModel model;

    public LoginNetLogic netLogic;

    public LoginCtrl() {
        Init();
    }

    public void Init() {
        model = new UserModel();
        netLogic = new LoginNetLogic();
    }

    public void LoginCREQ(UserDTO d) {
        model.data = d;
        netLogic.LoginCREQ();
    }

    public void RegisterCREQ(UserDTO d) {
        model.data = d;

        netLogic.LoginCREQ();
    }
}
