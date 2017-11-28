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
        //todo 保存数据

        CallEvent("LoginSRES", model.area);
    }

    /// <summary>
    /// 注册消息反馈
    /// </summary>
    /// <param name="model"></param>
    public void OnRegosterSRES(TransModel model) {
        CallEvent("RegisterSRES", model.area);
    }

    public string GetUserName() {
        if (data != null) {
            return data.name;
        }

        return null;
    }
}
