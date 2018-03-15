using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KulyGun : MonoBehaviour {
    public string bulletPath;
    public Transform bulletPos;

    public ParticleSystem particle;

    Transform bulletLookPos;

    bool hit = false;

    bool fire = false;
    // Use this for initialization
    void Start () {
        bulletLookPos = new GameObject("bulletLookPos").transform;
        this.AddObjEventFun(gameObject, "Fire", Fire);
	}
	
	// Update is called once per frame
	void Update () {
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

    void Fire(object[] args) {
        fire = true;
    }

    void FireLateUpdate() {
        particle.Simulate(1, true);
        particle.Play(true);

        GameObject obj = PoolMgr.Ins.GetResObj(bulletPath);
        obj.transform.position = bulletPos.position;

        if (hit) {
            obj.transform.LookAt(bulletLookPos);
        }
        else {
            obj.transform.eulerAngles = bulletPos.eulerAngles;
        }

        //todo
        obj.GetComponent<Bullet>().ResetData();

        //Debug.Log("fire"+bulletPos.position);

        obj.SetActive(true);
    }
}
