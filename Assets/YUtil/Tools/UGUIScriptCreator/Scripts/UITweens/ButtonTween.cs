using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTween : MonoBehaviour {

    UIEvent uiEvent;

    Button btn;

    public UTweenAll hoverTween;
    public UTweenAll clickTween;

    public void Start() {
        uiEvent = GetComponent<UIEvent>();
        if (uiEvent == null) {
            uiEvent = gameObject.AddComponent<UIEvent>();
        }

        btn = GetComponent<Button>();

        AddEvent();
    }

    void AddEvent() {
        uiEvent.enterEvent.AddListener(PlayHoverAni);

        uiEvent.exitEvent.AddListener((data) => {
            if (hoverTween != null) {
                hoverTween.Stop();
            }
        });

        uiEvent.clickEvent.AddListener(PlayClickAni);
    }

    private void PlayHoverAni(PointerEventData data) {
        if (hoverTween!=null) {
            if (btn != null && !btn.interactable) {
                return;
            }
            hoverTween.Play();
        }
    }

    void PlayClickAni(PointerEventData data) {
        if (clickTween!=null) {
            if (btn != null && !btn.interactable) {
                return;
            }
            clickTween.Play();
        }
    }
}
