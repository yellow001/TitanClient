using NetFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetIOTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (NetIO.Ins.msg != null && NetIO.Ins.msg.Count > 0) {
            OnMsgReceive(NetIO.Ins.msg[0]);
            NetIO.Ins.msg.RemoveAt(0);
        }
    }

    public void OnMsgReceive(TransModel model) {
        Debug.Log(model.GetMsg<string>());
    }

    public void Send() {
        TransModel m = new TransModel(1001001, 1);
        m.SetMsg("i am client asdasdasdasdasdasdasdsadasdsadasdsadsadasdasdasdasdasdadasdasd");
        MessageHandler.Send(m);
    }
}
