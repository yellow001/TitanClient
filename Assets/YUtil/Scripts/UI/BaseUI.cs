using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BaseUI : MonoBehaviour {

    public AudioClip openClip;
    public AudioClip closeClip;

    [HideInInspector]
    public bool inited = false;

    [HideInInspector]
    public AudioSource au;

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

    }

    public virtual void AddEvent() {
        
    }

    public virtual void OpenAni() {
        if (openClip != null) {
            au.clip = openClip;
            au.Play();
        }
    }

    public virtual void CloseAni() {
        if (closeClip != null)
        {
            au.clip = closeClip;
            au.Play();
        }
    }
}
