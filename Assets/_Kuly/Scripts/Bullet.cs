using ServerSimple.DTO.Fight;
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

    FightHandler fightHandler;

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
        fightHandler = GameObject.FindObjectOfType<FightHandler>();
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

            RaycastHit[] hits= Physics.RaycastAll(oPos, dis, dis.magnitude);
            if (hits.Length > 0) {
                Vector3 hitPoint = hits[hits.Length - 1].point;
                foreach (var item in hits) {
                    if (item.collider.CompareTag("Player_1")) {
                        //todo 发送伤害请求
                        DamageDTO dto = new DamageDTO();
                        dto.SrcID = fightHandler.currentID;
                        dto.DstID = fightHandler.ObjToIdDic[item.collider.gameObject];
                        dto.DamageType = 1;
                        dto.DamageValue = 20;
                        FightCtrl.Ins.DamageCREQ(dto);
                        //Debug.Log("hit");
                        hitPoint = item.point;
                        break;
                    }
                }
            }
        }
	}
}
