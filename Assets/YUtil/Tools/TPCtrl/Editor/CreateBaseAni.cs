using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class CreateBaseAni:Editor{

    [MenuItem("YUtil/创建基本状态机")]
    static void CreateBaseAnimator() {
        AnimatorController ani = Selection.activeObject as AnimatorController;
        if(ani!=null)
            Debug.Log("create");
    }
}
