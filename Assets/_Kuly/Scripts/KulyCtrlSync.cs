using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KulyCtrlSync : MonoBehaviour {
    public NetCtrlReceiver receiver;
    public Animator kulyAni;
    Rigidbody rig;

    public SyncPos gunPos;
    public InteractionSystem interactionSystem;
    public InteractionObject gun;

    public Transform gunNormalPos;

    public Transform weaponPos;

    Vector2 speed;
    Quaternion dstRotation;

    bool rmbState = true;
    // Use this for initialization
    IEnumerator Start() {
        rig = GetComponent<Rigidbody>();

        //this.AddObjEventFun(gameObject, "Fire", (args) => { Fire(); });
        yield return null;
        InitGunPos();
    }

    // Update is called once per frame
    void Update () {
        speed = new Vector2(receiver.movData.Horizontal, receiver.movData.Vertical);
        kulyAni.SetFloat("MoveSpeed", Mathf.Abs(speed.magnitude));

        Vector3 v = new Vector3(receiver.movData.Rotation.x,receiver.movData.Rotation.y,receiver.movData.Rotation.z);

        dstRotation = Quaternion.Euler(v);

        kulyAni.SetBool("Aim", receiver.movData.RMB);

        kulyAni.SetBool("Shoot", receiver.movData.RMB && receiver.movData.LMB);

        if (rmbState != receiver.movData.RMB) {
            rmbState = receiver.movData.RMB;
            if (rmbState) {
                SetAimState();
            }
            else {
                SetNormalState();
            }
        }
    }

    void SetAimState() {
        gunPos.SetTarget(null);

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
    }

    void SetNormalState() {
        gun.transform.parent = null;

        GetComponent<FullBodyBipedIK>().solver.SetIKPositionWeight(1);
        gunPos.SetTarget(gunNormalPos);

        HandPoser[] posers = GetComponentsInChildren<HandPoser>();
        for (int i = 0; i < posers.Length; i++) {
            posers[i].weight = 1;
        }
    }

    void InitGunPos() {
        gunPos.SetTarget(gunNormalPos);
        interactionSystem.StartInteraction(FullBodyBipedEffector.RightHand, gun, false);
        interactionSystem.StartInteraction(FullBodyBipedEffector.LeftHand, gun, false);

        HandPoser[] posers = GetComponentsInChildren<HandPoser>();
        for (int i = 0; i < posers.Length; i++) {
            posers[i].weight = 1;
        }
    }
}
