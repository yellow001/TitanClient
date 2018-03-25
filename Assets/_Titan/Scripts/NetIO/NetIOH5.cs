using NetFrame;
using NetFrame.EnDecode;
using NetFrame.EnDecode.Extend;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetIOH5:MonoBehaviour {
    WebSocket socket;

    bool init = false;
    bool error = false;

    //处理数据的缓冲区
    List<byte> cache = new List<byte>();

    //发送数据的消息队列
    public Queue<TransModel> write_queue = new Queue<TransModel>();

    bool isWrite=false, isRead=false;

    public List<TransModel> msg = new List<TransModel>();

    static NetIOH5 ins;

    public static NetIOH5 Ins {
        get {
            if (ins == null) {
                ins = FindObjectOfType<NetIOH5>();
            }

            if (ins == null) {
                GameObject obj;
                if ((obj = GameObject.Find("AllManager")) == null) {
                    obj = new GameObject("AllManager");
                }
                obj.AddComponent<NetIOH5>();
                ins = obj.GetComponent<NetIOH5>();
                AbsCoding.Ins = new PbCoding();
            }

            return ins;
        }
    }

    // Use this for initialization
    IEnumerator Start() {
        DontDestroyOnLoad(this.gameObject);

        socket = new WebSocket(new Uri("ws://"+ AppSetting.GetSetting("ServerIP")+":"+AppSetting.GetSetting("ServerPort")));
        yield return StartCoroutine(socket.Connect());
        init = true;
    }

    public void Update() {
        if (!init || error) {
            return;
        }

        byte[] data= socket.Recv();

        if (data == null || data.Length == 0) {
            return;
        }

        cache.AddRange(data);

        if (!isRead) {
            isRead = true;
            Read();
        }

        if (socket.error != null) {
            Debug.Log(socket.error);
            error = true;
        }
    }

    // <summary>
    /// 对缓冲区数据进行处理
    /// </summary>
    void Read() {
        TransModel model = AbsCoding.Ins.ModelDecoding<TransModel>(ref cache);
        if (model == null) {
            isRead = false;
            return;
        }
        msg.Add(model);
        Read();
    }

    public void Write() {
        if (write_queue.Count == 0) {
            isWrite = false;
            return;
        }

        TransModel model = write_queue.Dequeue();

        byte[] value = AbsCoding.Ins.ModelEncoding(model);
        socket.Send(value);

        Write();
    }

    public void Send(TransModel model) {
        write_queue.Enqueue(model);
        if (!isWrite) {
            Write();
        }
    }

}
