using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ServerSimple.DTO.Login;

public class UserItem : BaseUI {
    
    [HideInInspector]
    public Text nameTx;
    [HideInInspector]
    public Image root;
    [HideInInspector]
    public Button closeBtn;
    [HideInInspector]
    public Transform tra;

    [HideInInspector]
    public Image headImg;

    [HideInInspector]
    public Image MasterImg;

    public UserDTO currentDTO;
    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init() {
        tra = transform;
        nameTx = tra.Find("name/nameTx").GetComponent<Text>();
        root = GetComponent<Image>();
        closeBtn = tra.Find("closeBtn").GetComponent<Button>();

        headImg = tra.Find("name/head/headImg").GetComponent<Image>();
        MasterImg = tra.Find("name/MasterImg").GetComponent<Image>();

        base.Init();
    }

    public override void AddEvent() {
        closeBtn.onClick.AddListener(()=> {
            //踢人请求
            MatchCtrl.Ins.RemoveCREQ(currentDTO.name);
        });

        closeTweenAction += () => Destroy(gameObject,GetComponent<BaseUITween>().duration);
    }

    public override void UpdateView() {
        base.UpdateView();
        //effect.sprite=null;
        //name.text="";
        //root.sprite=null;
        if (string.IsNullOrEmpty(currentDTO.name)) { return; }

        //不是房主或者item是自己
        string userName = LoginCtrl.Ins.model.GetUserName();
        if (!MatchCtrl.Ins.model.currentRoom.masterName.Equals(userName) || currentDTO.name.Equals(userName)) {
            closeBtn.gameObject.SetActive(false);
        }
        else {
            closeBtn.gameObject.SetActive(true);
        }

        nameTx.text = currentDTO.name;

        MasterImg.gameObject.SetActive(MatchCtrl.Ins.model.currentRoom.masterName.Equals(currentDTO.name));
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
