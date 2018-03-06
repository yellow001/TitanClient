using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ServerSimple.DTO.Login;

public class UserEnterItem : BaseUI {

    [HideInInspector]
    public Transform root;
    [HideInInspector]
    public Text nameTx;
    [HideInInspector]
    public Image headImg;
    [HideInInspector]
    public Transform tra;

    [HideInInspector]
    public UserDTO currentDTO;
    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init() {

        tra = transform;
        root = GetComponent<Transform>();
        nameTx = tra.Find("name/nameTx").GetComponent<Text>();
        headImg = tra.Find("name/head/headImg").GetComponent<Image>();

        base.Init();
    }

    public override void AddEvent() {

        //headImg.onClick.AddListener();

    }

    public override void UpdateView() {
        base.UpdateView();

        UserData data = new UserData(currentDTO);

        nameTx.text=currentDTO.name;

        headImg.sprite = ResMgr.Ins.GetResAsset<Sprite>("head" + "/" + data.headID);

    }

    public override void CloseAni() {
        base.CloseAni();
        //@CloseAni
    }

    public override void OpenAni() {
        base.OpenAni();
        //@OpenAni
    }
}
