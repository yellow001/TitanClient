using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPos : MonoBehaviour {
    public Transform targetTra;

    bool changing = false;

    public float changeTime = 0.2f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (targetTra != null&&!changing) {
            transform.position = targetTra.position;
            transform.rotation = targetTra.rotation;
        }
	}

    public void SetTarget(Transform tra) {
        if (targetTra != tra) {
            changing = true;
            targetTra = tra;
            if(targetTra!=null)
                this.AddTimeEvent(changeTime, () => changing = false, (t, p) => {
                    transform.position = Vector3.Lerp(transform.position, targetTra.position, p);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetTra.rotation, p);
                });
        }
    }
}
