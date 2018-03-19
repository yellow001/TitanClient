using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimWeaponCam : MonoBehaviour {
    public float maxX=15;
    public float minX=-10;

    public float roSpeed=10;

    InputMgr inputMgr;

    Transform tra;
    Vector3 ro;

    public Transform bone;
    public float offsetX = 10;

    public KulyGunUser gun;
    // Use this for initialization
    void Start () {
        inputMgr = InputMgr.Ins;
        tra = transform;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (inputMgr.RMB) {
            tra.Rotate(-inputMgr.MouseY * Time.deltaTime * roSpeed, 0, 0);
            ro = tra.localEulerAngles;
            if (ro.x > 180) { ro.x -= 360; }
            ro.x = ro.x > maxX ? maxX : (ro.x < minX ? minX : ro.x);
            tra.localEulerAngles = ro;

            bone.localEulerAngles = ro + new Vector3(offsetX, 0, 0);
            this.CallEventList("BoneRotation", bone.localEulerAngles.x);
            if (gun != null) {
                gun.V_LateUpdate();
            }
        }
    }
}
