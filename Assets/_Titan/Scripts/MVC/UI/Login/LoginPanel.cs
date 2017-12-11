using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using NetFrame;

public class LoginPanel : BaseUI {

    [HideInInspector]
    public Image Image;
    [HideInInspector]
    public Text Text;
    [HideInInspector]
    public Button registerBtn;
    [HideInInspector]
    public InputField nameInput;
    [HideInInspector]
    public Text Placeholder;
    [HideInInspector]
    public Text defaultTx;
    [HideInInspector]
    public Text tip;
    [HideInInspector]
    public Image root;
    [HideInInspector]
    public Text Text8;
    [HideInInspector]
    public Button loginBtn;
    [HideInInspector]
    public InputField pwdInput;
    [HideInInspector]
    public Text Placeholder11;
    [HideInInspector]
    public Text defaultTx12;
    [HideInInspector]
    public Text tip13;
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
        Image = tra.Find("Image").GetComponent<Image>();
        Text = tra.Find("Right/registerBtn/Text").GetComponent<Text>();
        registerBtn = tra.Find("Right/registerBtn").GetComponent<Button>();
        nameInput = tra.Find("Right/nameInput").GetComponent<InputField>();
        Placeholder = tra.Find("Right/nameInput/Placeholder").GetComponent<Text>();
        defaultTx = tra.Find("Right/nameInput/defaultTx").GetComponent<Text>();
        tip = tra.Find("Right/nameInput/tip").GetComponent<Text>();
        root = GetComponent<Image>();
        Text8 = tra.Find("Right/loginBtn/Text").GetComponent<Text>();
        loginBtn = tra.Find("Right/loginBtn").GetComponent<Button>();
        pwdInput = tra.Find("Right/pwdInput").GetComponent<InputField>();
        Placeholder11 = tra.Find("Right/pwdInput/Placeholder").GetComponent<Text>();
        defaultTx12 = tra.Find("Right/pwdInput/defaultTx").GetComponent<Text>();
        tip13 = tra.Find("Right/pwdInput/tip").GetComponent<Text>();

        waitMask = tra.Find("waitMask").gameObject;

        model = LoginCtrl.Ins.model;
        base.Init();
    }

    public override void AddEvent() {
        registerBtn.onClick.AddListener(()=> {
            CloseAni();
            this.InvokeDeList("openRegisterPanel");
        });
        //nameInput.onEndEdit.AddListener();
        loginBtn.onClick.AddListener(OnLoginBtnClick);
        //pwdInput.onEndEdit.AddListener();

        //监听登录反馈事件
        model.BindEvent("LoginSRES",LoginSRES);
    }

    public override void UpdateView() {
        base.UpdateView();
        //Image.sprite=null;
        //Text.text="";
        //nameInput.text=;
        //Placeholder.text="";
        //defaultTx.text="";
        //tip.text="";
        //root.sprite=null;
        //Text8.text="";
        //pwdInput.text=;
        //Placeholder11.text="";
        //defaultTx12.text="";
        //tip13.text="";

    }

    void OnLoginBtnClick() {
        waitMask.SetActive(true);
        if (string.IsNullOrWhiteSpace(nameInput.text)) {
            this.AddMsg("用户名为空");
        }
        else if (string.IsNullOrWhiteSpace(pwdInput.text)) {
            this.AddMsg("密码为空");
        }
        else {
            model.SetUserData(nameInput.text, pwdInput.text);
            LoginCtrl.Ins.LoginCREQ();
            return;
        }

        waitMask.SetActive(false);
    }

    public override void CloseAni() {
        base.CloseAni();
        //@CloseAni
    }

    /// <summary>
    /// 登录反馈，登录不成功时解除登陆按钮的不可点击状态
    /// </summary>
    /// <param name="model"></param>
    public void LoginSRES(params object[] args) {
        int result = (int)args[0];
        //todo 验证result
        switch (result) {
            case 1:
                //todo 登录成功(加载场景)
                this.AddMsg("登陆成功");
                break;
            case -3:
                //用户不存在
                this.AddMsg("用户不存在");
                break;
            case -4:
                //用户或连接已登录
                this.AddMsg("你已登录");
                break;
            case -6:
                //密码错误
                this.AddMsg("密码错误");
                break;
            default:
                //其他错误
                this.AddMsg("登录错误，请尝试重新打开客户端");
                break;
        }
        waitMask.SetActive(false);
    }

    public override void OpenAni() {
        base.OpenAni();
    }
}
