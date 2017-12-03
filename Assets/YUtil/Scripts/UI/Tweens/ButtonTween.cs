using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTween : MonoBehaviour {

    UIEvent uiEvent;

    public UTweenAll hoverTween;
    public UTweenAll clickTween;

    public void Start() {
        uiEvent = GetComponent<UIEvent>();
        if (uiEvent == null) {
            uiEvent = gameObject.AddComponent<UIEvent>();
        }

        AddEvent();
    }

    void AddEvent() {
        uiEvent.enterEvent.AddListener(PlayHoverAni);

        uiEvent.exitEvent.AddListener((data) => {
            hoverTween.Stop();
        });

        uiEvent.clickEvent.AddListener(PlayClickAni);
    }

    private void PlayHoverAni(PointerEventData data) {
        if (hoverTween!=null) {
            hoverTween.Play();
        }
    }

    void PlayClickAni(PointerEventData data) {
        if (clickTween!=null) {
            clickTween.Play();
        }
    }
}
