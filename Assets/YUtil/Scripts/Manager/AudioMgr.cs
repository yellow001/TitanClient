using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 声音管理器（UI专用）
/// </summary>
public class AudioMgr : BaseManager<AudioMgr> {

    AudioSource au;

    public override void Init() {
        base.Init();
        if (!GetComponent<AudioSource>()) {
            au = gameObject.AddComponent<AudioSource>();
        }
        else {
            au = GetComponent<AudioSource>();
        }

        au.spatialBlend = 0;//2d声音
        au.playOnAwake = false;
        au.loop = false;
    }

    public void Play(AudioClip clip) {
        au.clip = clip;
        au.Play();
    }
}
