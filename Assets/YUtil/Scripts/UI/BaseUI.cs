using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class BaseUI : MonoBehaviour {

    public AudioClip openClip;
    public AudioClip closeClip;

    [HideInInspector]
    public bool inited = false;

    [HideInInspector]
    public AudioSource au;

    DOTweenAnimation[] anis;

    public void OnEnable() {
        if (!inited) {
            Init();
        }
        UpdateView();
        OpenAni();
        //Debug.Log("enable p");
    }
    
    public virtual void UpdateView() {
        if (!inited) {
            Init();
        }
        au = GetComponent<AudioSource>();
        au.loop = false;
        au.playOnAwake = false;
    }

    public virtual void Init() {
        AddEvent();
        inited = true;
        anis = GetComponentsInChildren<DOTweenAnimation>();
    }

    public virtual void AddEvent() {
        
    }

    public virtual void OpenAni() {
        if (openClip != null) {
            au.clip = openClip;
            au.Play();
        }

        if (anis != null && anis.Length > 0) {
            for (int i = 0; i < anis.Length; i++) {
                if (anis[i].id.Equals("open")) {
                    anis[i].tween.Restart();
                    //anis[i].DORestart(true);
                }
            }
        }
    }

    public virtual void CloseAni() {
        if (closeClip != null)
        {
            au.clip = closeClip;
            au.Play();
        }

        if (anis != null && anis.Length > 0) {
            for (int i = 0; i < anis.Length; i++) {
                if (anis[i].id.Equals("close")) {
                    //anis[i].DORestart(true);
                    anis[i].tween.Restart();
                }
            }
        }
    }
}
