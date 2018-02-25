using NetFrame;
using ServerSimple.DTO.Login;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserModel : BaseModel {

    public UserDTO data {
        get;
        protected set;
    }

    public string hairID, hairColor;

    public string clothID;
    public string[] clothColor;

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

        data.headID = string.IsNullOrEmpty(data.headID) ? "head0" : data.headID;

        if (string.IsNullOrEmpty(data.hairData) || data.hairData.Split('_').Length < 2) {
            hairID = "hair0";
            hairColor = "#ffffff";
        }
        else {
            string[] d = data.hairData.Split('_');
            hairID = d[0];
            hairColor = d[1];
        }

        if (string.IsNullOrEmpty(data.clothData) || data.clothData.Split('_').Length < 2) {
            clothID = "cloth0";
            clothColor = new string[]{ "#ffffff" };
        }
        else {
            string[] d = data.clothData.Split('_');
            clothID = d[0];
            Array.Copy(d, 1, clothColor, 0, d.Length - 1);
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
