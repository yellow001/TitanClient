using NetFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class H5Test : MonoBehaviour {
    public Text tx;
    public Button bu;
	// Use this for initialization
	void Start () {
        bu.onClick.AddListener(() => {
            TransModel m = new TransModel(1, 1);
            m.SetMsg("client");
            NetIOH5.Ins.Send(m);
        });
	}
	
	// Update is called once per frame
	void Update () {
        if (NetIOH5.Ins.msg != null && NetIOH5.Ins.msg.Count > 0) {
            OnMsgReceive(NetIOH5.Ins.msg[0]);
            NetIOH5.Ins.msg.RemoveAt(0);
        }
    }

    private void OnMsgReceive(TransModel transModel) {
        tx.text = transModel.GetMsg<string>();
    }
}
