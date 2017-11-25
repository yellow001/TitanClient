using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResPath {
    static ResPath ins;

    Dictionary<int, string> resPathDic = new Dictionary<int, string>();
    Dictionary<int, string> abPathDic = new Dictionary<int, string>();

    public System.Action<Dictionary<int, string>, Dictionary<int, string>> resPathAction;

    public static ResPath Ins{
        get {
            if (ins == null) {
                ins = new ResPath();
            }
            return ins;
        }
    }

    ResPath() {
        #region Res
        resPathDic.Add((int)EM_ResType.StaticCanvas, "UI");
        resPathDic.Add((int)EM_ResType.MsgAlert, "UI");
        resPathDic.Add((int)EM_ResType.TipAlert, "UI");
        #endregion

        #region AB
        abPathDic.Add((int)EM_ResType.StaticCanvas, "UI");
        abPathDic.Add((int)EM_ResType.MsgAlert, "UI");
        abPathDic.Add((int)EM_ResType.TipAlert, "UI");
        #endregion

        if (resPathAction != null) {
            resPathAction(resPathDic, abPathDic);
        }
    }

    public string GetResPath(EM_ResType t) {
        if (resPathDic.ContainsKey((int)t))
            return resPathDic[(int)t]+"/"+t.ToString();

        return string.Empty;
    }

    public string GetABPath(EM_ResType t) {
        if(abPathDic.ContainsKey((int)t))
            return abPathDic[(int)t];

        return string.Empty;
    }
}

public enum EM_ResType:int {

#region UI
    StaticCanvas=1001000,
    MsgAlert=1001001,
    TipAlert=1001002

#endregion


}
