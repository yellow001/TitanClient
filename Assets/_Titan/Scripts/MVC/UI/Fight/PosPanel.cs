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

    Transform userCam;

    float screenWidth;
    float maxValue;
    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init() {

        tra = transform;
        root = GetComponent<Transform>();
        Bg = tra.Find("Bg").GetComponent<RectTransform>();
        pos = tra.Find("Bg/pos").GetComponent<Image>();


        maxValue = Bg.sizeDelta.x * 0.5f-5;

        screenWidth = Screen.width*0.5f;
        Debug.Log(screenWidth);
        base.Init();
    }

    private void Update() {

        if (userCam == null) {
            userCam = Camera.main.transform;
            return;
        }

        foreach (var item in ObjToImgDic) {
            Vector3 p = item.Key.transform.position - userCam.position;
            p = p.normalized;
            float x = GetPosX(p);
            Vector2 pos = item.Value.rectTransform.anchoredPosition;
            pos.x = x;
            item.Value.rectTransform.anchoredPosition = pos;
        }
    }

    private float GetPosX(Vector3 p) {
        float v = Vector3.Dot(p, userCam.forward);
        if (v < 0) {
            v = Vector3.Dot(p, userCam.right);
            if (v < 0) {
                return -maxValue;
            }
            else {
                return maxValue;
            }
        }
        else {
            v = Vector3.Dot(p, userCam.right);
            float result = v * screenWidth;

            //return result;
            return result<-maxValue?-maxValue:(result>maxValue?maxValue:result);
        }
        //float v = Vector3.Dot(p, userCam.right);
        //v *= screenWidth;
        //return v;
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
                //user = item.Value.transform;
                continue;
            }
            else {
                Image img= Instantiate(pos, Bg).GetComponent<Image>();
                ObjToImgDic.Add(item.Value, img);
                IDToImgDic.Add(item.Key, img);
                img.gameObject.SetActive(true);
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
