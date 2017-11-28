using System;
using System.Collections;
using System.Collections.Generic;
using NetFrame;
using UnityEngine;

public class LoginUICtrl : MonoBehaviour {
    public BaseUI loginPanel;
    public BaseUI registerPanel;
	// Use this for initialization
	void Start () {
        loginPanel.gameObject.SetActive(true);
        AddEvent();
    }

    void AddEvent() {
        this.AddEventFun("openRegisterPanel", (args) => {
            registerPanel.gameObject.SetActive(true);
        });

        this.AddEventFun("openLoginPanel", (args) => {
            loginPanel.gameObject.SetActive(true);
        });
    }

    private void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 100, 40), "close")) {
            loginPanel.CloseAni();
        }

        if (GUI.Button(new Rect(10, 60, 100, 40), "open")) {
            loginPanel.gameObject.SetActive(true);
        }

        if (GUI.Button(new Rect(10, 110, 100, 40), "closeRegister")) {
            registerPanel.CloseAni();
        }

        if (GUI.Button(new Rect(10, 160, 100, 40), "openRegister")) {
            registerPanel.gameObject.SetActive(true);
        }
    }
}
