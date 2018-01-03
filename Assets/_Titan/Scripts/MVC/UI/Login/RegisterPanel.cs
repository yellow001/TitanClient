using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RegisterPanel : BaseUI {

    [HideInInspector]
    public Text Text;
    [HideInInspector]
    public InputField pInput;
    [HideInInspector]
    public Text Text2;
    [HideInInspector]
    public Button closeBtn;
    [HideInInspector]
    public Image Img;
    [HideInInspector]
    public Text registerTx;
    [HideInInspector]
    public InputField uInput;
    [HideInInspector]
    public Image rotate;
    [HideInInspector]
    public Image rotate2;
    [HideInInspector]
    public GameObject waitMask;
    [HideInInspector]
    public Text Text16;
    [HideInInspector]
    public Button registerBtn;
    [HideInInspector]
    public Text Placeholder;
    [HideInInspector]
    public Text Placeholder19;
    [HideInInspector]
    public InputField p2Input;
    [HideInInspector]
    public Text Placeholder24;
    [HideInInspector]
    public Transform tra;

    UserModel model;

    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init() {
        tra = transform;
        Text = tra.Find("uInput/Text").GetComponent<Text>();
        pInput = tra.Find("pInput").GetComponent<InputField>();
        Text2 = tra.Find("pInput/Text").GetComponent<Text>();
        closeBtn = tra.Find("closeBtn").GetComponent<Button>();
        Img = tra.Find("closeBtn/Img").GetComponent<Image>();
        registerTx = tra.Find("registerBtn/registerTx").GetComponent<Text>();
        uInput = tra.Find("uInput").GetComponent<InputField>();
        rotate = tra.Find("waitMask/rotate").GetComponent<Image>();
        rotate2 = tra.Find("waitMask/rotate2").GetComponent<Image>();
        waitMask = tra.Find("waitMask").gameObject;
        Text16 = tra.Find("p2Input/Text").GetComponent<Text>();
        registerBtn = tra.Find("registerBtn").GetComponent<Button>();
        Placeholder = tra.Find("pInput/Placeholder").GetComponent<Text>();
        Placeholder19 = tra.Find("uInput/Placeholder").GetComponent<Text>();
        p2Input = tra.Find("p2Input").GetComponent<InputField>();
        Placeholder24 = tra.Find("p2Input/Placeholder").GetComponent<Text>();

        model = LoginCtrl.Ins.model;
        base.Init();
    }

    public override void AddEvent() {
        //pInput.onEndEdit.AddListener();
        //uInput.onEndEdit.AddListener();
        //registerBtn.onClick.AddListener();
        //p2Input.onEndEdit.AddListener();

        closeBtn.onClick.AddListener(() => {
            CloseAni();
            this.InvokeDeList("openLoginPanel");
        });
        registerBtn.onClick.AddListener(() => OnRegisterBtnClick());

        model.BindEvent("RegisterSRES", RegisterSRES);
    }

    public override void UpdateView() {
        base.UpdateView();
        //Text.text="";
        //pInput.text=;
        //Text2.text="";
        //Img.sprite=null;
        //registerTx.text="";
        //UBroad.sprite=null;
        //RGradient.sprite=null;
        //uInput.text=;
        //RBroad.sprite=null;
        //LGradient.sprite=null;
        //rotate.sprite=null;
        //LBroad.sprite=null;
        //LGradient13.sprite=null;
        //rotate2.sprite=null;
        //waitMask.sprite=null;
        //Text16.text="";
        //Placeholder.text="";
        //Placeholder19.text="";
        //LGradient20.sprite=null;
        //p2Input.text=;
        //RGradient22.sprite=null;
        //RGradient23.sprite=null;
        //Placeholder24.text="";

    }

    public void OnRegisterBtnClick() {
        waitMask.SetActive(true);

        if (string.IsNullOrWhiteSpace(uInput.text)) {
            this.AddMsg("用户名不能为空");
        }
        else if (string.IsNullOrWhiteSpace(pInput.text)) {
            this.AddMsg("请输入密码");
        }
        else if (!p2Input.text.Equals(pInput.text)) {
            this.AddMsg("密码不匹配");
        }
        else {
            model.SetUserData(uInput.text, pInput.text);
            LoginCtrl.Ins.RegisterCREQ(model.data);
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
                closeBtn.onClick.Invoke();
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
        uInput.text = string.Empty;
        pInput.text = string.Empty;
        p2Input.text = string.Empty;
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
