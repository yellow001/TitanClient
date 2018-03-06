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

    public UserDTO tempData {
        get;
        set;
    }


    public bool isTemp=false;
    /// <summary>
    /// 登录消息反馈
    /// </summary>
    public void OnLoginSRES(TransModel model) {
        if (model.area == 1) {
            RefreshUserData(model.GetMsg<UserDTO>());
        }
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

    public void SetTempUserData() {

        CallEvent(UserEvent.RefreshUserModel, this);
    }

    public void RefreshUserData(UserDTO dto) {
        data = dto;
        tempData = dto;
        CallEvent(UserEvent.RefreshUserModel, this);
    }

    public UserData GetUserData() {
        if (isTemp) {
            return new UserData(tempData);
        }
        else {
            return new UserData(data);
        }
    }
}

public class UserData{

    public string headID;

    public string hairID, hairColor;

    public string clothID;
    public string[] clothColor;

    public UserData(UserDTO dto) {

        headID = string.IsNullOrEmpty(dto.headID) ? "head0" : dto.headID;

        if (string.IsNullOrEmpty(dto.hairData) || dto.hairData.Split('_').Length < 2) {
            hairID = "hair0";
            hairColor = "#ffffff";
        }
        else {
            string[] d = dto.hairData.Split('_');
            hairID = d[0];
            hairColor = d[1];
        }

        if (string.IsNullOrEmpty(dto.clothData) || dto.clothData.Split('_').Length < 2) {
            clothID = "cloth0";
            clothColor = new string[] { "#ffffff" };
        }
        else {
            string[] d = dto.clothData.Split('_');
            clothID = d[0];
            Array.Copy(d, 1, clothColor, 0, d.Length - 1);
        }
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
