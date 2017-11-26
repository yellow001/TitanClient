using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUICtrl : MonoBehaviour {
    public BaseUI loginPanel;
	// Use this for initialization
	void Start () {
		
	}

    private void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 100, 40), "close")) {
            loginPanel.CloseAni();
        }

        if (GUI.Button(new Rect(10, 60, 100, 40), "open")) {
            loginPanel.gameObject.SetActive(true);
        }
    }
}
