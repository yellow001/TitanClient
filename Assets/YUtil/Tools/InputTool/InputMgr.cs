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

    public Action InputChange,PositionChange,LMBChange,RMBChange,RotateY;

    bool change = false,move=false;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {

        change = false;
        move = false;

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
            RMBChange?.Invoke();
            change = true;
        }

        if (LMB != (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))) {
            LMB = Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
            LMBChange?.Invoke();
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

        if (RMB && MouseX != 0 && (Vertical == 0 && Horizontal == 0)) {
            RotateY?.Invoke();
        }

        if (Horizontal != 0 || Vertical != 0) {
            move = true;
        }

        if (change) {
            change = false;
            InputChangeFun();
        }

        if (move) {
            move = false;
            PositionChange?.Invoke();
        }
    }


    void InputChangeFun() {
        if (InputChange != null) {
            InputChange();
        }
    }
}
