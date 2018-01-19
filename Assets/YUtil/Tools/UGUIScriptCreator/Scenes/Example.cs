using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void ShowMsg() {
        AlertMgr.Ins.AddMsg("msg",()=> {
            AlertMgr.Ins.AddTip("you click ok");
        },()=> {
            AlertMgr.Ins.AddTip("you click no");
        });
    }

    public void ShowTip() {
        AlertMgr.Ins.AddTip("tip");
    }
}
