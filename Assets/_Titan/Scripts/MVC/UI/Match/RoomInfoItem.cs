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
    public Image Right;
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
        nameTx = tra.Find("Right/nameImg/nameTx").GetComponent<Text>();
        EnterBtn = tra.Find("Right/EnterBtn").GetComponent<Button>();
        Image = tra.Find("Right/EnterBtn/Image").GetComponent<Image>();
        Text = tra.Find("Right/EnterBtn/Text").GetComponent<Text>();
        Right = tra.Find("Right").GetComponent<Image>();
        numImg = tra.Find("Index/numImg").GetComponent<Image>();
        Index = tra.Find("Index").GetComponent<Image>();
        indexTX = tra.Find("Index/indexTX").GetComponent<Text>();
        root = GetComponent<Image>();
        numTx = tra.Find("Right/nameImg/numTx").GetComponent<Text>();
        nameImg = tra.Find("Right/nameImg").GetComponent<Image>();

        base.Init();
    }

    public override void AddEvent() {
        //EnterBtn.onClick.AddListener();

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
