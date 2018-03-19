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

    private void FixedUpdate() {
        Vector3 f = Vector3.zero;
        if (speed != Vector2.zero) {
            if (receiver.movData.RMB) {
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

            rig.velocity = f;
        }
        else {
            f.y = rig.velocity.y;
            rig.velocity = f;
        }

        if ((speed != Vector2.zero || receiver.movData.RMB) && dstRotation != null) {
            rig.rotation = Quaternion.Lerp(rig.rotation, dstRotation, rigRoSpeed * Time.fixedDeltaTime);
        }
    }

    private void LateUpdate() {
        if (receiver.movData.RMB && receiver.movData.boneRoX < 999) {
            Vector3 r = Bone.localEulerAngles;
            r.x = receiver.movData.boneRoX;
            Bone.eulerAngles =r;
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
    }
}
