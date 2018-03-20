using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserStatePanel : BaseUI {

    [HideInInspector]
    public Transform root;
    [HideInInspector]
    public Image HpImg;
    [HideInInspector]
    public Text HpTx;
    [HideInInspector]
    public Transform tra;


    FightModel fightModel;

    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init() {

        tra = transform;
        root = GetComponent<Transform>();
        HpImg = tra.Find("Hp/HpImg").GetComponent<Image>();
        HpTx = tra.Find("Hp/Text").GetComponent<Text>();

        fightModel = FightCtrl.Ins.model;

        base.Init();
    }

    public override void AddEvent() {

        fightModel.BindEvent(FightEvent.DamageSelf, (args) => { UpdateView(); });
    }

    public override void UpdateView() {
        base.UpdateView();

        float value = ((float)fightModel.currentModel.hp / (float)fightModel.currentModel.data.hpGrow.currentValue);
        HpImg.fillAmount = value;

        Color color = Color.Lerp(Color.red,Color.green, value);

        color.a = 0.8f;

        HpImg.color =color;

        HpTx.text = fightModel.currentModel.hp.ToString();

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
