using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserItem : BaseUI {

    [HideInInspector]
    public Image effect;
    [HideInInspector]
    public Text nameTx;
    [HideInInspector]
    public Image root;
    [HideInInspector]
    public Button closeBtn;
    [HideInInspector]
    public Transform tra;

    public string uname;
    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init() {
        tra = transform;
        effect = tra.Find("effect").GetComponent<Image>();
        nameTx = tra.Find("nameBg/name").GetComponent<Text>();
        root = GetComponent<Image>();
        closeBtn = tra.Find("closeBtn").GetComponent<Button>();

        base.Init();
    }

    public override void AddEvent() {
        closeBtn.onClick.AddListener(()=> {
            //todo 踢人请求
        });

        closeTweenAction += () => Destroy(gameObject,GetComponent<BaseUITween>().duration);
    }

    public override void UpdateView() {
        base.UpdateView();
        //effect.sprite=null;
        //name.text="";
        //root.sprite=null;
        if (string.IsNullOrEmpty(uname)) { return; }

        //不是房主或者item是自己
        string userName = LoginCtrl.Ins.model.GetUserName();
        if (!MatchCtrl.Ins.model.currentRoom.masterName.Equals(userName) || uname.Equals(userName)) {
            closeBtn.gameObject.SetActive(false);
        }
        else {
            closeBtn.gameObject.SetActive(true);
        }

        nameTx.text = uname;
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
