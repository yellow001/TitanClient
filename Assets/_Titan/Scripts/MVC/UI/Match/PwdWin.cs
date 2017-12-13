using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PwdWin : BaseUI {

    [HideInInspector]
    public Image titleImg;
    [HideInInspector]
    public Text Placeholder;
    [HideInInspector]
    public Button closeBtn;
    [HideInInspector]
    public Button okBtn;
    [HideInInspector]
    public Image root;
    [HideInInspector]
    public Text Text;
    [HideInInspector]
    public Text title;
    [HideInInspector]
    public Image bg;
    [HideInInspector]
    public Text Text8;
    [HideInInspector]
    public InputField pwdInput;
    [HideInInspector]
    public Transform tra;

    MatchModel model;

    public RoomInfoItem info;
    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init() {
        tra = transform;
        titleImg = tra.Find("bg/titleImg").GetComponent<Image>();
        Placeholder = tra.Find("bg/pwdInput/Placeholder").GetComponent<Text>();
        closeBtn = tra.Find("bg/closeBtn").GetComponent<Button>();
        okBtn = tra.Find("bg/okBtn").GetComponent<Button>();
        root = GetComponent<Image>();
        Text = tra.Find("bg/pwdInput/Text").GetComponent<Text>();
        title = tra.Find("bg/title").GetComponent<Text>();
        bg = tra.Find("bg").GetComponent<Image>();
        Text8 = tra.Find("bg/okBtn/Text").GetComponent<Text>();
        pwdInput = tra.Find("bg/pwdInput").GetComponent<InputField>();

        model = MatchCtrl.Ins.model;
        base.Init();
    }

    public override void AddEvent() {
        closeBtn.onClick.AddListener(() => {
            CloseAni();
            pwdInput.text = "";
        });
        okBtn.onClick.AddListener(() => {
            CloseAni();
            if (info == null) {
                MatchCtrl.Ins.CreateCREQ(string.IsNullOrEmpty(pwdInput.text)?"":YUtil.md5(pwdInput.text));
            }
            else {
                MatchCtrl.Ins.EnterCREQ(info.Dto.index, string.IsNullOrEmpty(pwdInput.text) ? "" : YUtil.md5(pwdInput.text));
            }
            pwdInput.text = "";
        });
        //pwdInput.onEndEdit.AddListener();

    }

    public override void UpdateView() {
        base.UpdateView();
        //titleImg.sprite=null;
        //Placeholder.text="";
        //root.sprite=null;
        //Text.text="";
        //title.text="";
        //bg.sprite=null;
        //Text8.text="";
        //pwdInput.text=;

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
