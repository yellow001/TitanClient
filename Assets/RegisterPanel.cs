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
        base.Init();
    }

    public override void AddEvent() {
        //nameInput.onEndEdit.AddListener();
        //cancleBtn.onClick.AddListener();
        //pwdInputTwice.onEndEdit.AddListener();
        //registerBtn.onClick.AddListener();
        //pwdInput.onEndEdit.AddListener();

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

    public override void CloseAni() {
        base.CloseAni();
        //@CloseAni
        group.alpha.ChangeValue(0, 0.25f, (v) => group.alpha = v,null,()=>gameObject.SetActive(false));
    }

    public override void OpenAni() {
        base.OpenAni();
        //@OpenAni
        group.alpha.ChangeValue(1, 0.25f, (v) => group.alpha = v);
    }
}
