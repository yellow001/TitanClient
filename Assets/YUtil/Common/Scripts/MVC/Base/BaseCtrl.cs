using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseCtrl<T>where T:BaseCtrl<T>{

    protected static T ins;

    public static T Ins {
        get {
            if (ins == null) {
                ins = Activator.CreateInstance<T>();
            }
            return ins;
        }
    }

    protected BaseCtrl() {

    }
}

public delegate void ModelEventCallBack(params object[] args);
