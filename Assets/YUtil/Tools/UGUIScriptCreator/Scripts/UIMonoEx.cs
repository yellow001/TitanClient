using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIMonoEx  {
    #region UI弹窗
    public static void AddMsg(this MonoBehaviour mono, string msg, Action okDe = null, Action cancleDe = null) {
        AlertMgr.Ins.AddMsg(new MsgUIModel(msg, "消息", okDe, cancleDe));
    }

    public static void AddMsg(this MonoBehaviour mono, MsgUIModel m) {
        AlertMgr.Ins.AddMsg(m);
    }

    public static void AddTip(this MonoBehaviour mono, string msg, float time = 2.5f) {
        //YManager.Ins.addTipModel(new TipModel(msg,time));
        AlertMgr.Ins.AddTip(new TipUIModel(msg, time));
    }

    public static void AddTip(this MonoBehaviour mono, TipUIModel m) {
        AlertMgr.Ins.AddTip(m);
    }
    #endregion
}
