using NetFrame;
using ServerSimple.DTO.Login;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserModel : BaseModel {

    public UserDTO data {
        get;
        protected set;
    }

    public string hairID, hairColor;

    public string clothID, clothColor;

    /// <summary>
    /// 登录消息反馈
    /// </summary>
    public void OnLoginSRES(TransModel model) {
        CallEvent(LoginEvent.LoginSRES, model.area);
    }

    /// <summary>
    /// 注册消息反馈
    /// </summary>
    /// <param name="model"></param>
    public void OnRegisterSRES(TransModel model) {
        CallEvent(LoginEvent.RegisterSRES, model.area);
    }

    public string GetUserName() {
        if (data != null) {
            return data.name;
        }

        return null;
    }

    public void SetUserData(string n,string p) {
        p = YUtil.md5(p);
        if (data == null) {
            data = new UserDTO(n, p,"","","");
        }
        else {
            data.name = n;
            data.password = p;
        }
    }

    public void RefreshUserData(UserDTO dto) {
        data = dto;

        data.headID = string.IsNullOrEmpty(data.headID) ? "0" : data.headID;

        if (string.IsNullOrEmpty(data.hairData) || data.hairData.Split('_').Length < 2) {
            hairID = "0";
            hairColor = "#ffffff";
        }
        else {
            string[] d = data.hairData.Split('_');
            hairID = d[0];
            hairColor = d[1];
        }

        if (string.IsNullOrEmpty(data.clothData) || data.clothData.Split('_').Length < 2) {
            clothID = "0";
            clothColor = "#ffffff";
        }
        else {
            string[] d = data.clothData.Split('_');
            clothID = d[0];
            clothColor = d[1];
        }

        CallEvent(UserEvent.RefreshUserModel, this);
    }
}

public enum LoginEvent {
    /// <summary>
    /// 登陆反馈
    /// </summary>
    LoginSRES,

    /// <summary>
    /// 注册反馈
    /// </summary>
    RegisterSRES,
}

public enum UserEvent {
    /// <summary>
    /// 更新用户数据
    /// </summary>
    RefreshUserModel,
}
