using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgUIModel {
    //消息内容
    public string msg;

    //弹窗标题
    public string title;

    public Action ok_de, no_de;

    public MsgUIModel(string msg,string t="消息", Action okDe = null, Action cancleDe = null) {
        this.msg = msg;
        title = t;
        ok_de = okDe;
        no_de = cancleDe;
    }
}

public class TipUIModel {
    //消息内容
    public string msg;

    public float duration;

    public TipUIModel(string m, float t = 1) {
        this.msg = m;
        this.duration = t;
    }
}