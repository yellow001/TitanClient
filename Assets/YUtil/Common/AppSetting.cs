using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AppSetting{
    static Dictionary<string, string> settings;

    public static string GetSetting(string k) {
        if (settings == null) {
            settings = new Dictionary<string, string>();
            StreamReader sr = File.OpenText(Application.streamingAssetsPath+"/AppSetting.txt");
            string content = sr.ReadToEnd();
            sr.Close();
            Init(content);
        }

        if (settings.ContainsKey(k)) {
            return settings[k];
        }
        else {
            return string.Empty;
        }
    }

    static void Init(string content) {
        string[] lines = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in lines) {
            string[] set = item.Split(':');
            if (set.Length >= 2) {
                settings[set[0]] = set[1];
            }
        }
    }
}
