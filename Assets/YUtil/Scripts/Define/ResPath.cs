using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResPath {
    static ResPath ins;

    Dictionary<string, string> resPathDic = new Dictionary<string, string>();
    Dictionary<string, string> abPathDic = new Dictionary<string, string>();

    public System.Action<Dictionary<string, string>, Dictionary<string, string>> resPathAction;

    public static ResPath Ins{
        get {
            if (ins == null) {
                ins = new ResPath();
            }
            return ins;
        }
    }

    ResPath() {

        //todo 这里应该读表初始化

        #region Res
        resPathDic.Add("StaticCanvas", "UI");
        resPathDic.Add("MsgAlert", "UI");
        resPathDic.Add("TipAlert", "UI");
        resPathDic.Add("localization", "Localization");
        #endregion

        #region AB
        abPathDic.Add("StaticCanvas", "UI");
        abPathDic.Add("MsgAlert", "UI");
        abPathDic.Add("TipAlert", "UI");
        abPathDic.Add("localization", "Localization");
        #endregion

        if (resPathAction != null) {
            resPathAction(resPathDic, abPathDic);
        }
    }

    public string GetResPath(string t) {
        if (resPathDic.ContainsKey(t))
            return resPathDic[t]+"/"+t.ToString();

        return string.Empty;
    }

    public string GetABPath(string t) {
        if(abPathDic.ContainsKey(t))
            return abPathDic[t];

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
