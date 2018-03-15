using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour ,IPoolReset{
    public string poolName = "bullet";

    Transform tra;

    public float speed = 50;

    public float desTime = 1;

    float deltaTime = 0;

    bool isMine = false;

    int srcID;

    Vector3 oPos,dis;

    RaycastHit hit;
    public string GetPoolName() {
        return poolName;
    }

    public void Reset() {
        deltaTime = 0;
        tra = transform;
        oPos = tra.position;
    }

    public void SetPoolName(string n) {
        poolName = n;
    }

    // Use this for initialization
    void Start () {
        tra = transform;
	}

    public void ResetData(bool isMine=false) {
        this.isMine = isMine;
    }

	// Update is called once per frame
	void Update () {
        if (deltaTime < desTime) {
            deltaTime += Time.deltaTime;
        }
        else {
            deltaTime = 0;
            PoolMgr.Ins.Push(this);
        }
        tra.position += tra.forward * speed;

        if (isMine) {
            dis = tra.position - oPos;
            if (Physics.Raycast(oPos, dis, out hit, dis.magnitude)) {
                if (hit.collider.CompareTag("Player")) {
                    //todo 发送伤害请求
                    Debug.Log("hit");
                }
            }
        }
	}
}
