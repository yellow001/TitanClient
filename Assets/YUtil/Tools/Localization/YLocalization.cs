using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class YLocalization {
    static YLocalization ins;
    static List<string> languageType = new List<string>();
    static List<string> allKeys = new List<string>();
    static string currentLanguage;
    static Dictionary<string, Dictionary<string, string>> languageDic = new Dictionary<string, Dictionary<string, string>>();

    public System.Action changeAction;

    public static YLocalization Ins {
        get {
            if (ins == null) {
                ins = new YLocalization();
            }
            return ins;
        }
    }

    YLocalization() {
        Init();
    }

    void Init() {
        string localizationTx="";
        if (!Application.isPlaying) {
            localizationTx = Resources.Load<TextAsset>("Localization/localization").text;
        }
        else {
            switch (ResMgr.Ins.loadmode) {
                case ResMgr.LoadMode.Resources:
                    localizationTx = ResMgr.Ins.GetAsset<TextAsset>("localization").text;
                    break;
                case ResMgr.LoadMode.AssetBundle:
                    string path = AppConst.ResPath + "/" + ResPath.Ins.GetResPath("localization") + ".txt";
                    using (StreamReader sr = new StreamReader(path)) {
                        localizationTx = sr.ReadToEnd();
                    }
                    break;
            }
            
        }
        ReadTx(localizationTx);
    }

    void ReadTx(string tx) {
        languageType.Clear();
        languageDic.Clear();

        string[] allLines = tx.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        string[] langKeys = allLines[0].Split(';');
        for (int i = 1; i < langKeys.Length; i++) {
            languageDic.Add(langKeys[i], new Dictionary<string, string>());
        }


        languageType.AddRange(languageDic.Keys.ToList());
        currentLanguage = PlayerPrefs.GetString("Language", languageType[0]);

        for (int j = 1; j < allLines.Length; j++) {
            string[] allpart = allLines[j].Split(';');
            for (int k = 0; k < languageType.Count; k++) {
                allKeys.Add(allpart[0]);
                languageDic[languageType[k]].Add(allpart[0], allpart[k+1]);
            }
        }
    }

    public string Get(string key) {
        if (languageDic[currentLanguage].ContainsKey(key)) {
            return languageDic[currentLanguage][key];
        }
        return key;
    }

    public void ChangeLanguage(string key) {
        if (!languageType.Contains(key)) {
            Debug.Log(string.Format("the language {0} is not contain", key));
            return;
        }
        PlayerPrefs.SetString("Language", key);
        currentLanguage = key;
        if (changeAction != null) {
            changeAction();
        }
    }

    public string[] GetLanguageList() {
        return languageType.ToArray();
    }

    public void AddKey() {

    }
}
