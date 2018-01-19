using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChangeBonesWin :EditorWindow {
    GameObject skinMeshObj;
    Transform rootTraObj;

    [MenuItem("YUtil/ChangeBoneWin")]
    public static void OpenChangeBoneWin() {
        ChangeBonesWin win = GetWindow<ChangeBonesWin>();
        win.Show();
    }

    private void OnGUI() {
        skinMeshObj = EditorGUILayout.ObjectField("蒙皮对象", skinMeshObj, typeof(GameObject),true)as GameObject;
        rootTraObj = EditorGUILayout.ObjectField("目标根骨骼", rootTraObj, typeof(Transform), true) as Transform;

        if (GUILayout.Button("开始设置")) {
            if (skinMeshObj == null || rootTraObj == null) {
                EditorUtility.DisplayDialog("提示", "请选择好物体🐷", "确定");
                return;
            }
            ChangeBone(skinMeshObj, rootTraObj.transform);
        }
    }

    public static void ChangeBone(GameObject srcObj,Transform rootBone) {

        if (!srcObj.GetComponentInChildren<SkinnedMeshRenderer>()) {
            EditorUtility.DisplayDialog("提示", "请选择含有蒙皮组件的物体到skinMeshObj🐷", "确定");
            return;
        }
        SkinnedMeshRenderer skin = srcObj.GetComponentInChildren<SkinnedMeshRenderer>();
        List<Transform> newBones = new List<Transform>();

        Transform root = GetTraInChild(skin.rootBone.name, rootBone);
        if (root == null) {
            EditorUtility.DisplayDialog("提示", string.Format("{0}中并没找到根骨骼{1}", rootBone.name, skin.rootBone.name), "(ノへ￣、)");
            return;
        }

        for (int i = 0; i < skin.bones.Length; i++) {
            Transform tra = null;
            tra = GetTraInChild(skin.bones[i].name, rootBone);
            if (tra != null) {
                newBones.Add(tra);
            }
            else {
                EditorUtility.DisplayDialog("提示", string.Format("{0}中并没找到子节点{1}", rootBone.name, skin.bones[i].name), "知道了啦");
                return;
            }
            //Debug.Log(skin.bones[i].name+"  "+GetTraPath(skin.bones[i].name, rootBone));
        }
        Transform oldRoot = skin.rootBone;
        skin.rootBone = root;
        skin.bones = newBones.ToArray();
        Destroy(oldRoot.gameObject);
        EditorUtility.DisplayDialog("提示","更改蒙皮骨骼成功😄", "(/≧▽≦)/");
    }

    #region 弃用
    public static string GetTraPath(string srcName,Transform root) {
        string path = string.Empty;

        Transform tra = GetTraInChild(srcName, root);

        if ( tra!= null) {
            path = tra.name;
            Debug.Log(path);
            while (tra!=root) {
                path = tra.parent.name + "/" + path;
                if (tra.parent != null) {
                    tra = tra.parent;
                }
                else {
                    return string.Empty;
                }
            }
        }
        else {
            EditorUtility.DisplayDialog("提示",string.Format("{0}中并没找到子节点{1}",root.name,srcName),"知道了啦");
        }

        return path;
    }
#endregion


    static Transform GetTraInChild(string srcName,Transform p) {
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

    [MenuItem("YUtil/Check Bone")]
    public static void CheckBone() {
        GameObject ga = Selection.activeGameObject;
        if (ga.GetComponent<SkinnedMeshRenderer>()) {
            SkinnedMeshRenderer skin = ga.GetComponent<SkinnedMeshRenderer>();
            Debug.Log(skin.rootBone.name);
            foreach (var item in skin.bones) {
                Debug.Log(item);
            }
        }
    }
}
