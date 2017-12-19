using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationHelper : MonoBehaviour {
    public string key;
    Text tx;
    void OnEnable() {
        tx = GetComponent<Text>();
        if (tx != null) {
            tx.text = YLocalization.Ins.Get(key);
            YLocalization.Ins.changeAction += ChangeLanguage;
        }
    }

    void ChangeLanguage() {
        if (tx != null) {
            tx.text = YLocalization.Ins.Get(key);
        }
    }
}
