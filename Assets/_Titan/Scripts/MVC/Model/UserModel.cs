using NetFrame;
using ServerSimple.DTO.Login;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserModel : BaseModel {
    public UserDTO data {
        get;
        set;
    }

    /// <summary>
    /// 登录消息反馈
    /// </summary>
    public void OnLoginSRES(TransModel model) {
        CallEvent("LoginSRES", model.area);
    }

    /// <summary>
    /// 注册消息反馈
    /// </summary>
    /// <param name="model"></param>
    public void OnRegisterSRES(TransModel model) {
        CallEvent("RegisterSRES", model.area);
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
            data = new UserDTO(n, p);
        }
        else {
            data.name = n;
            data.password = p;
        }
    }
}
