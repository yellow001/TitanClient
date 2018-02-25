using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KulyHairCtrl : MonoBehaviour {
    public Transform headBone;

    [HideInInspector]
    public string hairID;

    public string hairPath = "hair";

    GameObject hairObj;
    public void RefreshHair(string id) {
        if (!hairID.Equals(id)) {
            hairID = id;

            string path = hairPath + "/" + hairID;

            StartCoroutine(
                ResMgr.Ins.GetResAssetAsync<GameObject>(path, (obj) => {
                    if (hairObj != null) {
                        Destroy(hairObj);
                    }
                    hairObj = Instantiate(obj);
                    hairObj.transform.SetParent(headBone, false);
                })
            );

        }
    }
}
