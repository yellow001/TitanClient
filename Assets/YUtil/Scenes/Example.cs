using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void ShowMsg() {
        this.AddMsg("msg",()=> {
            this.AddTip("you click ok");
        },()=> {
            this.AddTip("you click no");
        });
    }

    public void ShowTip() {
        this.AddTip("tip");
    }
}
