using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KulyCtrlSync : MonoBehaviour {
    public NetCtrlReceiver receiver;
    public Animator kulyAni;
    Rigidbody rig;

    public GameObject gun;

    SyncPos gunPos;
    public InteractionSystem interactionSystem;
    InteractionObject gunInteraction;

    public Transform gunNormalPos;

    public Transform weaponPos;

    public Transform Bone;

    public float movForce;

    public float rigRoSpeed = 10;

    Vector2 speed;
    Quaternion dstRotation=Quaternion.identity;

    bool rmbState = true;

    bool init = false;

    bool Over = false;
    // Use this for initialization
    IEnumerator Start() {
        rig = GetComponent<Rigidbody>();

        //this.AddObjEventFun(gameObject, "Fire", (args) => { Fire(); });
        yield return null;
        InitGunPos();
        this.AddObjEventFun(gameObject,FightEvent.Kill.ToString(), (args) => {
            Over = true;
            kulyAni.SetBool("Dead", Over);
            SetNormalState();
            this.enabled = false;
        });
    }

    // Update is called once per frame
    void Update() {
        if (!init||Over) { return; }
        //speed = new Vector2(receiver.movData.Horizontal, receiver.movData.Vertical);
        //kulyAni.SetFloat("MoveSpeed", Mathf.Abs(speed.magnitude));


        if (receiver != null && receiver.movData != null) {
            Vector3 v = new Vector3(0, receiver.movData.RotationY, 0);

            dstRotation = Quaternion.Euler(v);

            kulyAni.SetBool("Aim", receiver.RMB);

            kulyAni.SetBool("Shoot", receiver.RMB && receiver.LMB);

            kulyAni.SetFloat("MoveSpeed", rig.position != receiver.movData.Position.ToVec3() ? 1 : 0);

            if (rmbState != receiver.RMB) {
                rmbState = receiver.RMB;
                if (rmbState) {
                    SetAimState();
                }
                else {
                    SetNormalState();
                }
            }
        }
        
    }

    private void FixedUpdate() {

        if (!init || Over) { return; }

        if (receiver.movData != null && receiver.movData.Position != null) {

            Vector3 target = receiver.movData.Position.ToVec3();

            if (rig.position != target) {
                if ((rig.position - target).magnitude < 0.1f) {
                    rig.position = target;
                }
                else {
                    rig.MovePosition(Vector3.Lerp(rig.position, receiver.movData.Position.ToVec3(), Time.fixedDeltaTime * movForce));
                }
            }

            

            //kulyAni.SetFloat("MoveSpeed", 1);
        }
        //else {
        //    kulyAni.SetFloat("MoveSpeed", 0);
        //}

        if (receiver.movData != null && rig.rotation != dstRotation) {
            rig.MoveRotation(dstRotation);
        }

        //Vector3 f = Vector3.zero;
        //if (speed != Vector2.zero) {
        //    if (receiver.RMB) {
        //        Vector3 f1 = Vector3.zero;

        //        if (speed.x > 0) {
        //            f1 = transform.right;
        //        }
        //        else {
        //            f1 = -transform.right;
        //        }

        //        f1 *= Mathf.Abs(speed.x);


        //        Vector3 f2 = Vector3.zero;
        //        if (speed.y > 0) {
        //            f2 = transform.forward;
        //        }
        //        else {
        //            f2 = -transform.forward;
        //        }

        //        f2 *= Mathf.Abs(speed.y);

        //        f = f1 + f2;

        //        //Debug.Log(f1 + "  " + f2);

        //        f = f.normalized * movForce * 0.72f;
        //    }
        //    else {
        //        //f = new Vector3(speed.x, rig.velocity.y, speed.y);
        //        f = transform.forward * movForce;
        //    }

        //    //rig.velocity = f;
        //}
        //f.y = rig.velocity.y;
        //rig.velocity = f;

        //if ((speed != Vector2.zero || receiver.RMB) && dstRotation != null) {
        //    rig.rotation = Quaternion.Lerp(rig.rotation, dstRotation, rigRoSpeed * Time.fixedDeltaTime);
        //}
    }

    private void LateUpdate() {
        if (receiver.RMB && receiver.boneRoX < 999) {
            Vector3 r = Bone.localEulerAngles;
            r.x = receiver.boneRoX;
            Bone.localEulerAngles = r;
        }
    }

    void Fire() {
        this.CallObjDeList(gun.gameObject, "Fire");
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

        gunInteraction.transform.SetParent(weaponPos, false);
        gunInteraction.transform.localPosition = Vector3.zero;
        gunInteraction.transform.localEulerAngles = Vector3.zero;
    }

    void SetNormalState() {
        gunInteraction.transform.parent = null;

        GetComponent<FullBodyBipedIK>().solver.SetIKPositionWeight(1);
        gunPos.SetTarget(gunNormalPos);

        HandPoser[] posers = GetComponentsInChildren<HandPoser>();
        for (int i = 0; i < posers.Length; i++) {
            posers[i].weight = 1;
        }
    }

    void InitGunPos() {

        gunPos = gun.GetComponentInChildren<SyncPos>();

        gunInteraction = gun.GetComponentInChildren<InteractionObject>();

        gunPos.SetTarget(gunNormalPos);
        interactionSystem.StartInteraction(FullBodyBipedEffector.RightHand, gunInteraction, false);
        interactionSystem.StartInteraction(FullBodyBipedEffector.LeftHand, gunInteraction, false);

        HandPoser[] posers = GetComponentsInChildren<HandPoser>();
        for (int i = 0; i < posers.Length; i++) {
            posers[i].weight = 1;
        }

        init = true;
    }
}
