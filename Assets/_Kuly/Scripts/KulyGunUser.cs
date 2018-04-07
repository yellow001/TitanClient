using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KulyGunUser : KulyGunPlayer {
    public string bulletPath;
    public Transform bulletPos;

    Transform bulletLookPos;

    bool hit = false;
    
    // Use this for initialization
    public override void Start () {
        bulletLookPos = new GameObject("bulletLookPos").transform;
        base.Start();
	}

    public override void LateUpdate() {
        
    }

    public void V_LateUpdate() {

        Ray ray;
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f));
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        hit = false;
        foreach (var item in hits) {
            if (!item.transform.CompareTag("User")) {
                hit = true;
                bulletLookPos.position = item.point;
                break;
            }
        }

        if (fire) {
            fire = false;
            FireLateUpdate();
        }

        //Debug.Log("late" + bulletPos.position);
    }

    public override void FireLateUpdate() {

        GameObject obj = PoolMgr.Ins.GetResObj(bulletPath);
        obj.transform.position = bulletPos.position;

        if (hit) {
            obj.transform.LookAt(bulletLookPos);
        }
        else {
            obj.transform.eulerAngles = bulletPos.eulerAngles;
        }

        this.CallEventList("GunFire", obj.transform.position, obj.transform.eulerAngles);

        //todo
        obj.GetComponent<Bullet>().ResetData(true);

        //Debug.Log("fire"+bulletPos.position);

        obj.SetActive(true);

        base.FireLateUpdate();
    }
}
