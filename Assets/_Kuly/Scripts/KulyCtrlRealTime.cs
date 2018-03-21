using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.Demos;
using RootMotion.FinalIK;
using Cinemachine;
using System;

public class KulyCtrlRealTime : MonoBehaviour {
    public Animator kulyAni;

    public InputMgr inputMgr;

    public float movForce;

    Vector2 speed;

    Rigidbody rig;

    Camera cam;

    public float rigRoSpeed = 10;
    Quaternion dstRotation;

    public GameObject gun;

    SyncPos gunSyncPos;
    public InteractionSystem interactionSystem;
    InteractionObject gunInteraction;

    public Transform gunNormalPos;

    public Transform weaponPos;

    bool rmbState = true;

    public CinemachineVirtualCamera aimCam;

    public float rotateSpeed = 10;

    //public AimIK aimIk;
    bool init = false;
    // Use this for initialization
    IEnumerator Start() {
        inputMgr = InputMgr.Ins;
        rig = GetComponent<Rigidbody>();

        cam = Camera.main;

        //this.AddObjEventFun(gameObject, "Fire", (args) => { Fire(); });
        yield return null;
        InitGunPos();
    }

    void Fire() {
        this.CallObjDeList(gun.gameObject, "Fire");
    }

    // Update is called once per frame
    void Update() {

        if (!init) { return; }

        speed = new Vector2(inputMgr.Horizontal, inputMgr.Vertical);
        kulyAni.SetFloat("MoveSpeed", Mathf.Abs(speed.magnitude));

        Vector3 v = Vector2.zero;

        if (speed != Vector2.zero) {
            float angle = Vector2.Angle(speed, Vector2.up);
            angle *= speed.x < 0 ? -1 : 1;

            if (inputMgr.RMB) {
                v = new Vector3(0, transform.eulerAngles.y, 0);
                v.y += inputMgr.MouseX * rotateSpeed;
            }
            else {
                v = new Vector3(0, cam.transform.eulerAngles.y, 0);
                v.y += angle;
            }
        }
        else {
            if (inputMgr.RMB) {
                v = new Vector3(0, transform.eulerAngles.y, 0);
                v.y += inputMgr.MouseX * rotateSpeed;
            }
        }

        dstRotation = Quaternion.Euler(v);

        kulyAni.SetBool("Aim", inputMgr.RMB);

        kulyAni.SetBool("Shoot", inputMgr.RMB && inputMgr.LMB);

        if (rmbState != inputMgr.RMB) {
            rmbState = inputMgr.RMB;
            if (rmbState) {
                SetAimState();
            }
            else {
                SetNormalState();
            }
        }
    }

    private void FixedUpdate() {
        Vector3 f = Vector3.zero;
        if (speed != Vector2.zero) {
            if (inputMgr.RMB) {
                Vector3 f1 = Vector3.zero;

                if (speed.x > 0) {
                    f1 = transform.right;
                }
                else {
                    f1 = -transform.right;
                }

                f1 *= Mathf.Abs(speed.x);


                Vector3 f2 = Vector3.zero;
                if (speed.y > 0) {
                    f2 = transform.forward;
                }
                else {
                    f2 = -transform.forward;
                }

                f2 *= Mathf.Abs(speed.y);

                f = f1 + f2;

                //Debug.Log(f1 + "  " + f2);

                f = f.normalized * movForce * 0.72f;
            }
            else {
                //f = new Vector3(speed.x, rig.velocity.y, speed.y);
                f = transform.forward * movForce;
            }

            //rig.velocity = f;
        }
        f.y = rig.velocity.y;
        rig.velocity = f;

        if ((speed != Vector2.zero || inputMgr.RMB) && dstRotation != null) {
            rig.rotation = Quaternion.Lerp(rig.rotation, dstRotation, rigRoSpeed * Time.fixedDeltaTime);
        }
    }

    void InitGunPos() {

        gunSyncPos = gun.GetComponentInChildren<SyncPos>();

        gunInteraction = gun.GetComponentInChildren<InteractionObject>();

        gunSyncPos.SetTarget(gunNormalPos);
        interactionSystem.StartInteraction(FullBodyBipedEffector.RightHand, gunInteraction, false);
        interactionSystem.StartInteraction(FullBodyBipedEffector.LeftHand, gunInteraction, false);

        HandPoser[] posers = GetComponentsInChildren<HandPoser>();
        for (int i = 0; i < posers.Length; i++) {
            posers[i].weight = 1;
        }

        init = true;
    }

    void SetNormalState() {
        //this.AddTimeEvent(0.2f, null, (t, p) => { aimIk.solver.SetIKPositionWeight(1-p); });

        aimCam.enabled = false;

        if (gun.transform.parent != null) {
            gun.transform.SetParent(null);
        }

        GetComponent<FullBodyBipedIK>().solver.SetIKPositionWeight(1);
        gunSyncPos.SetTarget(gunNormalPos);

        HandPoser[] posers = GetComponentsInChildren<HandPoser>();
        for (int i = 0; i < posers.Length; i++) {
            posers[i].weight = 1;
        }
    }

    void SetAimState() {
        aimCam.enabled = true;

        gunSyncPos.SetTarget(null);

        //this.AddTimeEvent(0.2f, null, (t, p) => { aimIk.solver.SetIKPositionWeight(p); });

        this.AddTimeEvent(0.2f, null, (t, p) => {
            GetComponent<FullBodyBipedIK>().solver.leftHandEffector.positionWeight = 1 - p;
        });
        //GetComponent<FullBodyBipedIK>().solver.leftHandEffector.positionWeight = 0 ;

        HandPoser[] posers = GetComponentsInChildren<HandPoser>();
        for (int i = 0; i < posers.Length; i++) {
            posers[i].weight = 1;
        }

        gun.transform.SetParent(weaponPos, false);
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localEulerAngles = Vector3.zero;

        rig.rotation = Quaternion.Euler(0, cam.transform.localEulerAngles.y, 0);
    }
}
