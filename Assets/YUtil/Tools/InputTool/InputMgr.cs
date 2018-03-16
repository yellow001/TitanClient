using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : BaseManager<InputMgr> {

    public float Horizontal=0;

    public float Vertical=0;

    public bool Space=false;

    public bool RMB=false;

    public bool LMB=false;

    public float MouseX=0;

    public float MouseY=0;

    public Action InputChange;

    bool change = false;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {

        change = false;

        if (Horizontal != Input.GetAxis("Horizontal")) {
            Horizontal = Input.GetAxis("Horizontal");
            change = true;
        }

        if (Vertical != Input.GetAxis("Vertical")) {
            Vertical = Input.GetAxis("Vertical");
            change = true;
        }

        if (Space != Input.GetKeyDown(KeyCode.Space)) {
            Space = Input.GetKeyDown(KeyCode.Space);
            change = true;
        }

        if (RMB != Input.GetMouseButton(1)) {
            RMB = Input.GetMouseButton(1);
            change = true;
        }

        if (LMB != (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))) {
            LMB = Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
            change = true;
        }

        if (MouseX != Input.GetAxis("Mouse X")) {
            MouseX = Input.GetAxis("Mouse X");
            change = true;
        }

        if (MouseY != Input.GetAxis("Mouse Y")) {
            MouseY = Input.GetAxis("Mouse Y");
            change = true;
        }

        if (change) {
            change = false;
            InputChangeFun();
        }
    }


    void InputChangeFun() {
        if (InputChange != null) {
            InputChange();
        }
    }
}
