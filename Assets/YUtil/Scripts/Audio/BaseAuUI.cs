using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseAuUI : MonoBehaviour,
        IPointerClickHandler,
        IPointerEnterHandler
{
    [Tooltip("\'n\' means no audio")]
    public string enAuname, ckAuname;

    public AudioClip enterAu, clickAu;

    void Start()
    {
        //this.addTip("base start");
        OnStart();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickAu != null) {
            AudioMgr.Ins.Play(clickAu);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (enterAu != null)
        {
            AudioMgr.Ins.Play(enterAu);
            //this.addTip("enter",0.2f);
        }
    }

    
    public void OnStart()
    {
        //this.addTip("base onstart");

        //ResHandler res = FindObjectOfType<ResHandler>();

        //if (res == null) {
        //    Debug.Log("cannot load the res");
        //    return;
        //}

        //if (enterAu == null&& !enAuname.Equals("n")) {

        //    if (enAuname.Trim().Equals(string.Empty)) {
        //        enAuname = "enter";
        //    }

        //    enterAu = res.getAsset<AudioClip>("BtnAu", enAuname);
        //    //this.addTip("load enter au");
        //}

        //if (clickAu == null&& !ckAuname.Equals("n"))
        //{
        //    if (ckAuname.Trim().Equals(string.Empty))
        //    {
        //        ckAuname = "click";
        //    }
        //    clickAu = res.getAsset<AudioClip>("BtnAu", ckAuname);
        //    //this.addTip("load btn au");
        //}
    }
}
