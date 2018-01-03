using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginPanel : BaseUI {

    [HideInInspector]
    public Text loginTx;
    [HideInInspector]
    public Image LBroad;
    [HideInInspector]
    public Image UBroad;
    [HideInInspector]
    public Image LGradient;
    [HideInInspector]
    public Image RGradient;
    [HideInInspector]
    public InputField uInput;
    [HideInInspector]
    public Image rotate;
    [HideInInspector]
    public GameObject waitMask;
    [HideInInspector]
    public Text Text;
    [HideInInspector]
    public Text Placeholder;
    [HideInInspector]
    public Image rotate2;
    [HideInInspector]
    public Image RGradient11;
    [HideInInspector]
    public Text Text12;
    [HideInInspector]
    public Button registerBtn;
    [HideInInspector]
    public Text registerTx;
    [HideInInspector]
    public Image LBroad15;
    [HideInInspector]
    public Image RBroad;
    [HideInInspector]
    public Image UBroad17;
    [HideInInspector]
    public Image root;
    [HideInInspector]
    public Button loginBtn;
    [HideInInspector]
    public Image RBroad20;
    [HideInInspector]
    public Image LGradient21;
    [HideInInspector]
    public InputField pInput;
    [HideInInspector]
    public Text Placeholder23;
    [HideInInspector]
    public Transform tra;

    UserModel model;

    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init() {
        tra = transform;
        loginTx = tra.Find("loginBtn/loginTx").GetComponent<Text>();
        LBroad = tra.Find("loginBtn/LBroad").GetComponent<Image>();
        UBroad = tra.Find("loginBtn/UBroad").GetComponent<Image>();
        LGradient = tra.Find("uInput/LGradient").GetComponent<Image>();
        RGradient = tra.Find("uInput/RGradient").GetComponent<Image>();
        uInput = tra.Find("uInput").GetComponent<InputField>();
        rotate = tra.Find("waitMask/rotate").GetComponent<Image>();
        waitMask = tra.Find("waitMask").gameObject;
        Text = tra.Find("uInput/Text").GetComponent<Text>();
        Placeholder = tra.Find("uInput/Placeholder").GetComponent<Text>();
        rotate2 = tra.Find("waitMask/rotate2").GetComponent<Image>();
        RGradient11 = tra.Find("pInput/RGradient").GetComponent<Image>();
        Text12 = tra.Find("pInput/Text").GetComponent<Text>();
        registerBtn = tra.Find("registerBtn").GetComponent<Button>();
        registerTx = tra.Find("registerBtn/registerTx").GetComponent<Text>();
        LBroad15 = tra.Find("registerBtn/LBroad").GetComponent<Image>();
        RBroad = tra.Find("registerBtn/RBroad").GetComponent<Image>();
        UBroad17 = tra.Find("registerBtn/UBroad").GetComponent<Image>();
        loginBtn = tra.Find("loginBtn").GetComponent<Button>();
        RBroad20 = tra.Find("loginBtn/RBroad").GetComponent<Image>();
        LGradient21 = tra.Find("pInput/LGradient").GetComponent<Image>();
        pInput = tra.Find("pInput").GetComponent<InputField>();
        Placeholder23 = tra.Find("pInput/Placeholder").GetComponent<Text>();

        model = LoginCtrl.Ins.model;

        base.Init();
    }

    public override void AddEvent() {
        //uInput.onEndEdit.AddListener();
        //pInput.onEndEdit.AddListener();
        registerBtn.onClick.AddListener(() => {
            CloseAni();
            this.InvokeDeList("openRegisterPanel");
        });
        loginBtn.onClick.AddListener(OnLoginBtnClick);

        //监听登录反馈事件
        model.BindEvent("LoginSRES", LoginSRES);
    }

    public override void UpdateView() {
        base.UpdateView();
        //loginTx.text="";
        //LBroad.sprite=null;
        //UBroad.sprite=null;
        //LGradient.sprite=null;
        //RGradient.sprite=null;
        //uInput.text=;
        //rotate.sprite=null;
        //waitMask.sprite=null;
        //Text.text="";
        //Placeholder.text="";
        //rotate2.sprite=null;
        //RGradient11.sprite=null;
        //Text12.text="";
        //registerTx.text="";
        //LBroad15.sprite=null;
        //RBroad.sprite=null;
        //UBroad17.sprite=null;
        //root.sprite=null;
        //RBroad20.sprite=null;
        //LGradient21.sprite=null;
        //pInput.text=;
        //Placeholder23.text="";

    }

    void OnLoginBtnClick() {
        waitMask.SetActive(true);
        if (string.IsNullOrWhiteSpace(uInput.text)) {
            this.AddMsg("用户名不能为空");
        }
        else if (string.IsNullOrWhiteSpace(pInput.text)) {
            this.AddMsg("密码不能为空");
        }
        else {
            model.SetUserData(uInput.text, pInput.text);
            LoginCtrl.Ins.LoginCREQ(model.data);
            return;
        }

        waitMask.SetActive(false);
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
                this.AddTip("登陆成功");
                SceneManager.LoadScene("Match");
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

    public override void CloseAni() {
        base.CloseAni();
        //@CloseAni
    }

    public override void OpenAni() {
        base.OpenAni();
        //@OpenAni
    }
}
