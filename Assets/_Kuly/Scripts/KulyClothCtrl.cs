using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KulyClothCtrl : MonoBehaviour {
    public Transform rootBone;

    public Transform parentObj;

    [HideInInspector]
    public string clothID;

    public string clothPath = "cloth";

    GameObject clothObj;
    string[] color;
    public void RefreshCloth(string id, string[] c) {
        if (!clothID.Equals(id)) {
            clothID = id;

            string path = clothPath + "/" + clothID;

            StartCoroutine(
                ResMgr.Ins.GetResAssetAsync<GameObject>(path, (obj) => {
                    if (clothObj != null) {
                        Destroy(clothObj);
                    }

                    clothObj = Instantiate(obj);

                    //设置骨骼
                    BoneUtil.ChangeBone(clothObj, rootBone);

                    //设置父节点
                    clothObj.transform.SetParent(parentObj, false);

                    color = c;
                    if (clothObj.GetComponentInChildren<MatColorCtrl>()) {
                        clothObj.GetComponentInChildren<MatColorCtrl>().SetColor(color);
                    }
                })
            );


        }

        if (color == null || !color.Equals(c)) {
            color = c;
            if (clothObj != null && clothObj.GetComponentInChildren<MatColorCtrl>()) {
                clothObj.GetComponentInChildren<MatColorCtrl>().SetColor(color);
            }
        }
    }




}
