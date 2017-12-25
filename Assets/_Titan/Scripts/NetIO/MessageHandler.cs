using NetFrame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageHandler : MonoBehaviour {

    static Dictionary<int, List<MsgReceive_De>> msgPool = new Dictionary<int, List<MsgReceive_De>>();
    

    public void Update() {
#if !WEBGL
        if (NetIO.Ins.msg != null && NetIO.Ins.msg.Count > 0) {
            OnMsgReceive(NetIO.Ins.msg[0]);
            NetIO.Ins.msg.RemoveAt(0);
        }
#else

        if (NetIOH5.Ins.msg != null && NetIOH5.Ins.msg.Count > 0) {
            OnMsgReceive(NetIOH5.Ins.msg[0]);
            NetIOH5.Ins.msg.RemoveAt(0);
        }
#endif
    }

    public void OnMsgReceive(TransModel model) {

        if (msgPool.ContainsKey(model.pID)) {
            lock (msgPool[model.pID]) {
                foreach (var item in msgPool[model.pID]) {
                    if (item != null) {
                        item(model);
                    }
                    else {
                        RemoveFunByPid(model.pID, item);
                    }
                        
                }
            }
        }
    }

    /// <summary>
    /// 注册协议对应函数
    /// </summary>
    /// <param name="pid"></param>
    /// <param name="fun"></param>
    public static void Register(int pid, MsgReceive_De fun) {
        if (!msgPool.ContainsKey(pid)) {
            List<MsgReceive_De> list = new List<MsgReceive_De>();
            list.Add(fun);
            msgPool.Add(pid, list);
        }
        else {
            if (msgPool[pid].Contains(fun)) {
                Debug.LogWarning(fun.ToString() + " is contain where pid is " + pid);
            }
            else {
                msgPool[pid].Add(fun);
            }
        }
    }

    /// <summary>
    /// 移除某协议上的所有监听
    /// </summary>
    /// <param name="pid"></param>
    public static void ClearPid(int pid) {
        if (!msgPool.ContainsKey(pid)) {
            Debug.LogWarning("the pid " + pid + " you want to clear is not contain");
            return;
        }
        msgPool[pid].Clear();
    }

    public static void RemoveFunByPid(int pid, MsgReceive_De fun) {
        if (!msgPool.ContainsKey(pid)) {
            Debug.LogWarning("the pid " + pid + " you want to remove is not contain");
            return;
        }

        if (!msgPool[pid].Contains(fun)) {
            Debug.LogWarning("the fun in pid " + pid + " you want to remove is not contain");
            return;
        }

        msgPool[pid].Remove(fun);
    }

    /// <summary>
    /// 移除所有协议监听
    /// </summary>
    public static void RemoveAll() {
        msgPool.Clear();
        GC.Collect();
    }

    public static void Send(TransModel model) {
#if !WEBGL
        NetIO.Ins.Send(model);
#else
        NetIOH5.Ins.Send(model);
#endif
    }
}

public delegate void MsgReceive_De(TransModel model);
