using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseModel{

    public Dictionary<string, List<ModelEventCallBack>> callBackDic = new Dictionary<string, List<ModelEventCallBack>>();

    public void BindEvent(Enum eventName, ModelEventCallBack func) {
        BindEvent(eventName.ToString(), func);
    }

    /// <summary>
    /// 绑定事件名与回调函数（外部尽量不要调用）
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="func"></param>
    public void BindEvent(string eventName,ModelEventCallBack func) {
        if (callBackDic.ContainsKey(eventName)) {
            if (callBackDic[eventName].Contains(func)) {
                return;
            }
            callBackDic[eventName].Add(func);
        }
        else {
            callBackDic.Add(eventName, new List<ModelEventCallBack>() { func });
        }
    }

    public void CallEvent(string eventName, params object[] args) {

        if (callBackDic.ContainsKey(eventName) && callBackDic[eventName].Count > 0) {
            List<ModelEventCallBack> fun = callBackDic[eventName].ToList();
            foreach (var item in fun) {
                if(item!=null)
                    item(args);
            }
        }
    }

    public void CallEvent(Enum eventName, params object[] args) {
        CallEvent(eventName.ToString(), args);
    }

    public void UnBindEvent(string eventName,ModelEventCallBack func) {
        if (!callBackDic.ContainsKey(eventName)) {
            return;
        }
        if (!callBackDic[eventName].Contains(func)) {
            return;
        }
        callBackDic[eventName].Remove(func);

    }

    public void ClearEvent(string eventName) {
        if (!callBackDic.ContainsKey(eventName)) {
            return;
        }

        callBackDic[eventName].Clear();
    }

    public void ClearAllEvent() {
        callBackDic.Clear();
    }
}
