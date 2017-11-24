using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneUtil : MonoBehaviour {

    public static void ChangeBone(GameObject srcObj, Transform rootBone) {

        if (!srcObj.GetComponentInChildren<SkinnedMeshRenderer>()) {
            Debug.Log("请选择含有蒙皮组件的物体到skinMeshObj🐷");
            return;
        }
        SkinnedMeshRenderer skin = srcObj.GetComponentInChildren<SkinnedMeshRenderer>();
        List<Transform> newBones = new List<Transform>();
        
        Transform root = GetTraInChild(skin.rootBone.name, rootBone);
        if (root == null) {
            Debug.Log(string.Format("{0}中并没找到根骨骼{1}", rootBone.name, skin.rootBone.name));
            return;
        }

        for (int i = 0; i < skin.bones.Length; i++) {
            Transform tra = null;
            tra = GetTraInChild(skin.bones[i].name, rootBone);
            if (tra != null) {
                newBones.Add(tra);
            }
            else {
                Debug.Log(string.Format("{0}中并没找到子节点{1}", rootBone.name, skin.bones[i].name));
                return;
            }
            //Debug.Log(skin.bones[i].name+"  "+GetTraPath(skin.bones[i].name, rootBone));
        }
        Transform oldRoot = skin.rootBone;
        skin.rootBone = root;
        skin.bones = newBones.ToArray();
        Destroy(oldRoot.gameObject);
    }

    static Transform GetTraInChild(string srcName, Transform p) {
        if (p.name.Equals(srcName)) {
            return p;
        }
        else {
            for (int i = 0; i < p.childCount; i++) {
                Transform tra = GetTraInChild(srcName, p.GetChild(i));
                if (tra != null) {
                    return tra;
                }
            }
            return null;
        }
    }
}
