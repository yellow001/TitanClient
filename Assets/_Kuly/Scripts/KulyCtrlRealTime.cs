﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KulyCtrlRealTime : MonoBehaviour {
    public Animator kulyAni;

    public InputMgr inputMgr;

    public float movForce;

    Vector2 speed;

    Rigidbody rig;

    public Camera cam;
	// Use this for initialization
	void Start () {
        inputMgr = InputMgr.Ins;
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        speed = new Vector2(inputMgr.Horizontal,inputMgr.Vertical);
        kulyAni.SetFloat("MoveSpeed", Mathf.Abs(speed.magnitude));

        if (speed != Vector2.zero) {
            float angle = Vector2.Angle(speed, Vector2.up);

            Vector3 v = new Vector3(0, cam.transform.rotation.eulerAngles.y, 0);

            v.y += angle;

            rig.rotation = Quaternion.Euler(v);
        }
	}

    private void FixedUpdate() {
        if (speed != Vector2.zero) {
            Vector3 f = new Vector3(speed.x, rig.velocity.y, speed.y);
            rig.velocity = transform.forward* f.magnitude * movForce;
        }
    }
}
