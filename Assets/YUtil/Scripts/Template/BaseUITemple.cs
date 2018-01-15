using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaseUITemple : BaseUI {

//@member

    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init()
    {

//@Init
        base.Init();
    }

    public override void AddEvent() {

//@AddEvent
    }

    public override void UpdateView()
    {
        base.UpdateView();
        
//@UpdateView
    }

    public override void CloseAni()
    {
        base.CloseAni();
//@CloseAni
    }

    public override void OpenAni()
    {
        base.OpenAni();
//@OpenAni
    }
}
