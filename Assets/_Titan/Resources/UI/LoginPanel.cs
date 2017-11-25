using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    CanvasGroup group;

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

        group = GetComponent<CanvasGroup>();
        base.Init();
    }

    public override void AddEvent() {
        //registerBtn.onClick.AddListener();
        //nameInput.onEndEdit.AddListener();
        //loginBtn.onClick.AddListener();
        //pwdInput.onEndEdit.AddListener();

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

    public override void CloseAni() {
        base.CloseAni();
        //@CloseAni
        this.AddTimeEvent(0.25f, null, (t,p) => {
            ((RectTransform)tra).sizeDelta = new Vector2(900, Mathf.Lerp(450, 400, p));
            group.alpha = 1 - p;
        });
    }

    public override void OpenAni() {
        base.OpenAni();
        //@OpenAni
        this.AddTimeEvent(0.25f, null, (t, p) => {
            ((RectTransform)tra).sizeDelta = new Vector2(900, Mathf.Lerp(400, 450, p));
            group.alpha = p;
        });
    }
}
