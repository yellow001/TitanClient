using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ServerSimple.DTO.Match;

public class RoomInfoItem : BaseUI {

    [HideInInspector]
    public Text nameTx;
    [HideInInspector]
    public Button EnterBtn;
    [HideInInspector]
    public Image Image;
    [HideInInspector]
    public Text Text;
    [HideInInspector]
    public Image numImg;
    [HideInInspector]
    public Image Index;
    [HideInInspector]
    public Text indexTX;
    [HideInInspector]
    public Image root;
    [HideInInspector]
    public Text numTx;
    [HideInInspector]
    public Image nameImg;
    [HideInInspector]
    public Transform tra;

    float aniCountValue,countValue;
    public MatchRoomDTO Dto {
        get {
            return dto;
        }
        set {
            dto = value;
            UpdateView();
        }
    }

    private MatchRoomDTO dto;
    
    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init() {
        tra = transform;
        nameTx = tra.Find("nameImg/nameTx").GetComponent<Text>();
        EnterBtn = tra.Find("EnterBtn").GetComponent<Button>();
        Image = tra.Find("EnterBtn/Image").GetComponent<Image>();
        Text = tra.Find("EnterBtn/Text").GetComponent<Text>();
        numImg = tra.Find("Index/numImg").GetComponent<Image>();
        Index = tra.Find("Index").GetComponent<Image>();
        indexTX = tra.Find("Index/indexTX").GetComponent<Text>();
        root = GetComponent<Image>();
        numTx = tra.Find("nameImg/numTx").GetComponent<Text>();
        nameImg = tra.Find("nameImg").GetComponent<Image>();
        
        base.Init();
    }

    public override void AddEvent() {
        EnterBtn.onClick.AddListener(EnterBtnClick);

        closeTweenAction += () => Destroy(gameObject, GetComponent<BaseUITween>().duration);
    }

    void EnterBtnClick() {
        if (string.IsNullOrEmpty(Dto.passwd)) {
            //无密码直接发送请求
            MatchCtrl.Ins.EnterCREQ(Dto.index, "");
        }
        else {
            //有密码先打开输入密码框
            this.CallEventList("openPwdWin",this);
        }
    }

    private void Update() {
        if (Dto != null) {
            aniCountValue = Mathf.Lerp(aniCountValue, countValue, Time.deltaTime);
            numImg.fillAmount = aniCountValue;
        }
    }

    public override void UpdateView() {
        base.UpdateView();
        //nameTx.text="";
        //Image.sprite=null;
        //Text.text="";
        //Right.sprite=null;
        //numImg.sprite=null;
        //Index.sprite=null;
        //indexTX.text="";
        //root.sprite=null;
        //numTx.text="";
        //nameImg.sprite=null;
        if (Dto == null) { return; }

        indexTX.text = Dto.index.ToString();
        nameTx.text = Dto.masterName;
        numTx.text = Dto.playerList.Count + " / " + Dto.maxNum;
        countValue = (float)Dto.playerList.Count / (float)Dto.maxNum;
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
