using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseModel{

    public Dictionary<string, List<ModelEventCallBack>> callBackDic = new Dictionary<string, List<ModelEventCallBack>>();

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
            foreach (var item in callBackDic[eventName]) {
                if(item!=null)
                    item(args);
            }
        }
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
