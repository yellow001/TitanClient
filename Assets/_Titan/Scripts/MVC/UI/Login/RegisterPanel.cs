using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RegisterPanel : BaseUI {

    [HideInInspector]
    public Text Placeholder;
    [HideInInspector]
    public Text tip;
    [HideInInspector]
    public Text Placeholder2;
    [HideInInspector]
    public InputField nameInput;
    [HideInInspector]
    public Text Text;
    [HideInInspector]
    public Button cancleBtn;
    [HideInInspector]
    public Text defaultTx;
    [HideInInspector]
    public Text defaultTx7;
    [HideInInspector]
    public InputField pwdInputTwice;
    [HideInInspector]
    public Text Text9;
    [HideInInspector]
    public Button registerBtn;
    [HideInInspector]
    public InputField pwdInput;
    [HideInInspector]
    public Text Placeholder12;
    [HideInInspector]
    public Text defaultTx13;
    [HideInInspector]
    public Text tip14;
    [HideInInspector]
    public Text tip15;
    [HideInInspector]
    public Image root;
    [HideInInspector]
    public Image Image;
    [HideInInspector]
    public Transform tra;
    [HideInInspector]
    public GameObject waitMask;

    UserModel model;
    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init() {
        tra = transform;
        Placeholder = tra.Find("Mid/pwdInputTwice/Placeholder").GetComponent<Text>();
        tip = tra.Find("Mid/nameInput/tip").GetComponent<Text>();
        Placeholder2 = tra.Find("Mid/nameInput/Placeholder").GetComponent<Text>();
        nameInput = tra.Find("Mid/nameInput").GetComponent<InputField>();
        Text = tra.Find("Mid/cancleBtn/Text").GetComponent<Text>();
        cancleBtn = tra.Find("Mid/cancleBtn").GetComponent<Button>();
        defaultTx = tra.Find("Mid/nameInput/defaultTx").GetComponent<Text>();
        defaultTx7 = tra.Find("Mid/pwdInputTwice/defaultTx").GetComponent<Text>();
        pwdInputTwice = tra.Find("Mid/pwdInputTwice").GetComponent<InputField>();
        Text9 = tra.Find("Mid/registerBtn/Text").GetComponent<Text>();
        registerBtn = tra.Find("Mid/registerBtn").GetComponent<Button>();
        pwdInput = tra.Find("Mid/pwdInput").GetComponent<InputField>();
        Placeholder12 = tra.Find("Mid/pwdInput/Placeholder").GetComponent<Text>();
        defaultTx13 = tra.Find("Mid/pwdInput/defaultTx").GetComponent<Text>();
        tip14 = tra.Find("Mid/pwdInput/tip").GetComponent<Text>();
        tip15 = tra.Find("Mid/pwdInputTwice/tip").GetComponent<Text>();
        root = GetComponent<Image>();
        Image = tra.Find("Image").GetComponent<Image>();
        waitMask = tra.Find("waitMask").gameObject;

        model = LoginCtrl.Ins.model;
        base.Init();
    }

    public override void AddEvent() {
        //nameInput.onEndEdit.AddListener();
        cancleBtn.onClick.AddListener(() => {
            CloseAni();
            this.InvokeDeList("openLoginPanel");
        });
        //pwdInputTwice.onEndEdit.AddListener();
        registerBtn.onClick.AddListener(()=>OnRegisterBtnClick());
        //pwdInput.onEndEdit.AddListener();

        model.BindEvent("RegisterSRES",RegisterSRES);
    }

    public override void UpdateView() {
        base.UpdateView();
        //Placeholder.text="";
        //tip.text="";
        //Placeholder2.text="";
        //nameInput.text=;
        //Text.text="";
        //defaultTx.text="";
        //defaultTx7.text="";
        //pwdInputTwice.text=;
        //Text9.text="";
        //pwdInput.text=;
        //Placeholder12.text="";
        //defaultTx13.text="";
        //tip14.text="";
        //tip15.text="";
        //root.sprite=null;
        //Image.sprite=null;

    }

    public void OnRegisterBtnClick() {
        waitMask.SetActive(true);

        if (string.IsNullOrWhiteSpace(nameInput.text)) {
            this.AddMsg("用户名不能为空");
        }
        else if (string.IsNullOrWhiteSpace(pwdInput.text)) {
            this.AddMsg("请输入密码");
        }
        else if (!pwdInputTwice.text.Equals(pwdInput.text)) {
            this.AddMsg("密码不匹配");
        }
        else {
            model.SetUserData(nameInput.text, pwdInput.text);
            LoginCtrl.Ins.RegisterCREQ();
            return;
        }

        waitMask.SetActive(false);

    }

    //1  注册成功;-1 dto错误;-2 用户名以及密码出错;-3 用户已存在
    void RegisterSRES(params object[] args) {
        int result = (int)args[0];
        //todo 验证result
        switch (result) {
            case 1:
                this.AddTip("注册成功");
                cancleBtn.onClick.Invoke();
                break;
            case -3:
                this.AddMsg("用户已存在");
                break;
            default:
                this.AddMsg("注册出错，请检查输入或网络设置");
                break;
        }
        Clear();
        waitMask.SetActive(false);
    }

    void Clear() {
        nameInput.text = string.Empty;
        pwdInput.text = string.Empty;
        pwdInputTwice.text = string.Empty;
    }
    

    public override void CloseAni() {
        base.CloseAni();
        //@CloseAni
    }

    public override void OpenAni() {
        base.OpenAni();
        //@OpenAni
        group.alpha.ChangeValue(1, 0.25f, (v) => group.alpha = v);
    }
}
