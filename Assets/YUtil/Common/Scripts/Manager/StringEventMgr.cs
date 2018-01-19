using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有监听事件的通用委托(无返回值)
/// </summary>
/// <param name="objs"></param>
public delegate void InvokeDe(params object[] objs);

public class StringEventMgr : BaseManager<StringEventMgr> {

    public Dictionary<string, List<InvokeDe>> DeList=new Dictionary<string, List<InvokeDe>>();

    public Dictionary<GameObject, Dictionary<string, List<InvokeDe>>> ObjDelist=new Dictionary<GameObject, Dictionary<string, List<InvokeDe>>>();

    #region 注册、移除、调用事件
    public void AddEventName(string name) {
        lock (Ins) {
            if (DeList.ContainsKey(name)) {
                return;
            }

            DeList.Add(name, new List<InvokeDe>());
        }
    }

    public void RemoveEventName(string name) {
        lock (Ins) {
            if (!DeList.ContainsKey(name)) {
#if UNITY_EDITOR
                Debug.Log(string.Format("StringEventManager: the event name \'{0}\' is not contain", name));
#endif
                return;
            }

            DeList.Remove(name);
        }

    }


    public void AddEventFun(string name, InvokeDe fun) {

        lock (Ins) {
            AddEventName(name);
            if (DeList[name].Contains(fun)) {
#if UNITY_EDITOR
                Debug.Log(string.Format("StringEventManager: you can not add the function because the event \'{0}\' already had the function", name));
#endif
                return;
            }

            DeList[name].Add(fun);
        }
    }

    public void RemoveEventFun(string name, InvokeDe fun) {

        lock (Ins) {
            if (!DeList.ContainsKey(name)) {
#if UNITY_EDITOR
                Debug.Log(string.Format("StringEventManager: you can not remove the function because the event name \'{0}\' is not contain", name));
#endif
                return;
            }

            if (!DeList[name].Contains(fun)) {
#if UNITY_EDITOR
                Debug.Log(string.Format("StringEventManager: you can not remove the function because the event \'{0}\' do not have the function", name));
#endif
                return;
            }

            DeList[name].Remove(fun);
        }
    }

    public void InvokeDeList(string name, params object[] objs) {
        lock (Ins) {
            if (!DeList.ContainsKey(name)) {
#if UNITY_EDITOR
                Debug.Log(string.Format("StringEventManager: you can not invoke the function because the event name \'{0}\' is not contain", name));
#endif
                return;
            }

            foreach (var item in DeList[name]) {
                item(objs);
            }
        }
    }
    #endregion

    #region 注册物体事件名称、监听事件等
    public void AddObjEventName(GameObject obj, string name) {
        lock (Ins) {
            //存在该物体键值
            if (ObjDelist.ContainsKey(obj)) {
                //检查该是否该函数名
                if (ObjDelist[obj].ContainsKey(name)) {
                    return;
                }
                else {
                    ObjDelist[obj].Add(name, new List<InvokeDe>());
                }
            }
            else {
                ObjDelist.Add(obj, new Dictionary<string, List<InvokeDe>>());
                ObjDelist[obj].Add(name, new List<InvokeDe>());
            }
        }
    }

    public void RemoveObjEvent(GameObject obj) {
        lock (Ins) {
            if (!ObjDelist.ContainsKey(obj)) {
                return;
            }
            ObjDelist.Remove(obj);
        }

    }

    public void RemoveObjEventName(GameObject obj, string name) {
        lock (Ins) {
            if (!ObjDelist.ContainsKey(obj)) {
                return;
            }
            if (!ObjDelist[obj].ContainsKey(name)) {
                return;
            }
            ObjDelist[obj].Remove(name);
        }

    }


    public void AddObjEventFun(GameObject obj, string name, InvokeDe fun) {
        lock (Ins) {
            AddObjEventName(obj, name);
            if (ObjDelist[obj][name].Contains(fun)) {
                return;
            }
            ObjDelist[obj][name].Add(fun);
        }
    }

    public void RemoveObjEventFun(GameObject obj, string name, InvokeDe fun) {

        lock (Ins) {
            if (!ObjDelist.ContainsKey(obj)) { return; }
            if (!ObjDelist[obj].ContainsKey(name)) { return; }
            if (!ObjDelist[obj][name].Contains(fun)) { return; }

            ObjDelist[obj][name].Remove(fun);
        }
    }

    public void InvokeObjDeList(GameObject obj, string name, params object[] objs) {
        lock (Ins) {
            if (!ObjDelist.ContainsKey(obj)) {
#if UNITY_EDITOR
                Debug.Log(string.Format("StringEventManager:{0} has no funList name {1}", obj, name));
#endif
                return;
            }
            if (!ObjDelist[obj].ContainsKey(name)) {
#if UNITY_EDITOR
                Debug.Log(string.Format("StringEventManager:  event name \'{0}\' is not contain in {1}", name, obj));
#endif
                return;
            }

            foreach (var item in ObjDelist[obj][name]) {
                item(objs);
            }
        }
    }
    #endregion

}
