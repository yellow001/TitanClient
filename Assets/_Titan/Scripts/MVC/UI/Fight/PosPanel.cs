using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class PosPanel : BaseUI {

    [HideInInspector]
    public Transform root;
    [HideInInspector]
    public Image pos;
    [HideInInspector]
    public Transform tra;

    RectTransform Bg;

    Dictionary<int, Image> IDToImgDic = new Dictionary<int, Image>();

    Dictionary<GameObject, Image> ObjToImgDic = new Dictionary<GameObject, Image>();

    Transform user;
    float maxValue;
    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init() {

        tra = transform;
        root = GetComponent<Transform>();
        Bg = tra.Find("Bg").GetComponent<RectTransform>();
        pos = tra.Find("Bg/pos").GetComponent<Image>();


        maxValue = Bg.sizeDelta.x * 0.5f;
        base.Init();
    }

    private void Update() {

        if (user == null) { return; }

        foreach (var item in ObjToImgDic) {
            Vector3 p = item.Key.transform.position - user.position;
            p = p.normalized;
            float x = GetPosX(p);
            Vector2 pos = item.Value.rectTransform.anchoredPosition;
            pos.x = x;
            item.Value.rectTransform.anchoredPosition = pos;
        }
    }

    private float GetPosX(Vector3 p) {
        float v = Vector3.Dot(p, user.forward);
        if (v < 0) {
            v = Vector3.Dot(p, user.right);
            if (v < 0) {
                return -1 * maxValue;
            }
            else {
                return maxValue;
            }
        }
        else {
            v = Vector3.Dot(p, user.right);
            return v * maxValue;
        }
    }

    public override void AddEvent() {

        this.AddEventFun(FightEvent.InitPosPanel.ToString(), (args) => {
            InitImg((int)args[0], (Dictionary<int, GameObject>)args[1]);
        });
    }

    public override void UpdateView() {
        base.UpdateView();

        //pos.sprite=null;

    }

    public void InitImg(int currentID,Dictionary<int,GameObject> objDic) {
        foreach (var item in objDic) {
            if (item.Key == currentID) {
                user = item.Value.transform;
            }
            else {
                Image img= Instantiate(pos, Bg).GetComponent<Image>();
                ObjToImgDic.Add(item.Value, img);
                IDToImgDic.Add(item.Key, img);
            }
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
