using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : BaseManager<InputMgr> {

    public float Horizontal;

    public float Vertical;

    public bool Space;

    public bool RMB;

    public bool LMB;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Horizontal = Input.GetAxis("Horizontal");

        Vertical = Input.GetAxis("Vertical");

        Space = Input.GetKeyDown(KeyCode.Space);

        RMB = Input.GetMouseButton(1);

        LMB = Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
	}
}
