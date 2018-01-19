﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoEx{

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

    #region 时间事件
    public static void AddTimeEvent(this MonoBehaviour mono, TimeEvent model)
    {
        TimeEventMgr.Ins.AddTimeEvent(model);
    }

    public static void AddTimeEvent(this MonoBehaviour mono, float t,Action overDe,Action<float,float> updateDe,bool ignoreTime=false, int count=1,bool doNow=false)
    {
        TimeEventMgr.Ins.AddTimeEvent(new TimeEvent(t,overDe,ignoreTime, updateDe,count,doNow));
    }

    public static void RemoveTimeEvent(this MonoBehaviour mono, TimeEvent model)
    {
        TimeEventMgr.Ins.RemoveTimeEvent(model);
    }
    #endregion

    #region 注册全局事件名称、监听事件等
    public static void AddEventName(this MonoBehaviour mono, string name)
    {
        StringEventMgr.Ins.AddEventName(name);
    }

    public static void RemoveEventName(this MonoBehaviour mono, string name)
    {
        StringEventMgr.Ins.RemoveEventName(name);
    }


    public static void AddEventFun(this MonoBehaviour mono, string name, InvokeDe fun)
    {
        StringEventMgr.Ins.AddEventFun(name, fun);
    }

    //public static void addEventFunName(this MonoBehaviour mono, string name, InvokeDe fun) {
    //    YManager.Ins.addEventName(name);
    //    YManager.Ins.addEventFun(name, fun);
    //}

    public static void RemoveEventFun(this MonoBehaviour mono, string name, InvokeDe fun)
    {
        StringEventMgr.Ins.RemoveEventFun(name, fun);
    }

    public static void InvokeDeList(this MonoBehaviour mono, string name, params object[] objs)
    {
        StringEventMgr.Ins.InvokeDeList(name, objs);
    }
    #endregion

    #region 注册物体事件名称、监听事件等
    public static void AddObjEventName(this MonoBehaviour mono, GameObject obj, string name) {
        StringEventMgr.Ins.AddObjEventName(obj, name);
    }

    public static void RemoveObjEventName(this MonoBehaviour mono, GameObject obj, string name) {
        StringEventMgr.Ins.RemoveObjEventName(obj, name);
    }


    public static void AddObjEventFun(this MonoBehaviour mono, GameObject obj, string name, InvokeDe fun) {
        StringEventMgr.Ins.AddObjEventFun(obj, name, fun);
    }

    //public static void addObjEventFunName(this MonoBehaviour mono, string name, InvokeDe fun) {
    //    YManager.Ins.addEventName(name);
    //    YManager.Ins.addEventFun(name, fun);
    //}

    public static void RemoveObjEventFun(this MonoBehaviour mono, GameObject obj, string name, InvokeDe fun) {
        StringEventMgr.Ins.RemoveObjEventFun(obj, name, fun);
    }

    public static void InvokeObjDeList(this MonoBehaviour mono, GameObject obj, string name, params object[] objs) {
        StringEventMgr.Ins.InvokeObjDeList(obj, name, objs);
    }
    #endregion
}