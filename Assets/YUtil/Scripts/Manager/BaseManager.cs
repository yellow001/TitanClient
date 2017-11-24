using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager<T> : MonoBehaviour where T:BaseManager<T>{
    protected static T ins;
    protected bool inited = false;
    public static T Ins {
        get {
            if (ins == null) {
                ins = FindObjectOfType<T>();
            }

            if (ins == null) {
                GameObject obj;
                if ((obj=GameObject.Find("AllManager"))==null) {
                    obj = new GameObject("AllManager");
                }
                obj.AddComponent<T>();
                ins = obj.GetComponent<T>();
            }

            if (!ins.inited) {
                ins.Init();
                DontDestroyOnLoad(ins.gameObject);
            }

            return ins;
        }
    }

    public virtual void Init() {
        inited = true;
    }
}
