using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EventAuClips : MonoBehaviour {
    public AuItem[] items;
    public string auAssetName;
    new public AudioSource audio;
	// Use this for initialization
	void Start () {

        audio = GetComponentInParent<AudioSource>();
        if (audio == null) { return; }

        audio.playOnAwake = false;
        audio.loop = false;
        

        foreach (var item in items) {
            if (string.IsNullOrEmpty(item.eName)) {
                continue;
            }

            if (item.clip == null) {
                item.clip = ResMgr.Ins.getAsset<AudioClip>(auAssetName, item.auPath);
            }

            if (item.eObj == null) {
                item.eObj = gameObject;
            }

            this.AddObjEventFun(item.eObj, item.eName, (args) => {
                this.AddTimeEvent(item.delayTime, () => {
                    if (isActiveAndEnabled) {
                        audio.clip = item.clip;
                        audio.Play();
                    }
                }, null);
            });
        }
	}

    [System.Serializable]
    public class AuItem {
        public string eName;
        public string auPath;
        public AudioClip clip;
        public GameObject eObj;
        public float delayTime = 0;
    }
}
