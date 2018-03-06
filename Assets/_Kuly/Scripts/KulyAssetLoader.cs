using ServerSimple.DTO.Login;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KulyAssetLoader : MonoBehaviour {
    public KulyClothCtrl clothCtrl;

    public KulyHairCtrl hairCtrl;

    public int loadCount = 2;

    public Action refreshCallback;

    UserModel userModel;
    // Use this for initialization
    void Awake() {

        clothCtrl.callBack += LoadCallBack;

        hairCtrl.callBack += LoadCallBack;

        userModel = LoginCtrl.Ins.model;

        userModel.BindEvent(UserEvent.RefreshUserModel, (args) => { RefreshKulyData(); });
    }

    void LoadCallBack(GameObject obj) {
        loadCount--;
        if (loadCount == 0) {
            if (refreshCallback != null) {
                refreshCallback();
            }
        }
    }

    public void SetCallBack(Action callBack = null) {
        if (callBack != null) {
            refreshCallback += callBack;
        }
    }

    public void RefreshKulyData() {
        loadCount = 2;

        //todo refresh
        UserData data = userModel.GetUserData();

        hairCtrl.RefreshHair(data.hairID);

        clothCtrl.RefreshCloth(data.clothID, data.clothColor);
        
    }
}
