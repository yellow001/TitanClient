using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverPanel : BaseUI {

    [HideInInspector]
    public Image root;
    [HideInInspector]
    public Text OverTx;
    [HideInInspector]
    public Button OKBtn;
    [HideInInspector]
    public Transform tra;


    FightModel fightModel;

    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init() {

        tra = transform;
        root = GetComponent<Image>();
        OverTx = tra.Find("OverTx").GetComponent<Text>();
        OKBtn = tra.Find("OKBtn").GetComponent<Button>();

        fightModel = FightCtrl.Ins.model;
        base.Init();
    }

    public override void AddEvent() {
        OKBtn.onClick.AddListener(()=> {
            //todo 打开匹配场景
            SceneManager.LoadScene("Match");
        });

    }

    public override void UpdateView() {
        base.UpdateView();

        //root.sprite=null;
        //OverTx.text="";

    }

    public void SetResult(bool fail=true) {
        if (fail) {
            OverTx.text = "失 败";
        }
        else {
            OverTx.text = "胜 利";
        }
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
