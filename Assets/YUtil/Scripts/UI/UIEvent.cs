using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIEvent : MonoBehaviour,
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerUpHandler,
    IDragHandler,
    IBeginDragHandler,
    IEndDragHandler,
    IInitializePotentialDragHandler,
    IDropHandler,
    ISelectHandler,
    IDeselectHandler
    {

    public PointerUIEvent clickEvent = new PointerUIEvent(),
                        downEvent = new PointerUIEvent(),
                        enterEvent = new PointerUIEvent(),
                        exitEvent = new PointerUIEvent(),
                        upEvent = new PointerUIEvent(),
                        dragEvent = new PointerUIEvent(),
                        beginDragEvent=new PointerUIEvent(),
                        endDragEvent=new PointerUIEvent(),
                        dropEvent=new PointerUIEvent(),
                        initDragEvent=new PointerUIEvent();
    public BaseUIEvent selectEvent = new BaseUIEvent(),
                        deselectEvent = new BaseUIEvent();

    public UnityEvent clickEvent_base = new UnityEvent(),
                        downEvent_base = new UnityEvent(),
                        enterEvent_base = new UnityEvent(),
                        exitEvent_base = new UnityEvent(),
                        upEvent_base = new UnityEvent(),
                        dragEvent_base = new UnityEvent(),
                        beginDragEvent_base = new UnityEvent(),
                        endDragEvent_base = new UnityEvent(),
                        dropEvent_base = new UnityEvent(),
                        initDragEvent_base = new UnityEvent(),
                        selectEvent_base = new UnityEvent(),
                        deselectEvent_base = new UnityEvent();

    public void OnBeginDrag(PointerEventData eventData) {
        if (beginDragEvent != null) {
            beginDragEvent.Invoke(eventData);
        }

        if (beginDragEvent_base != null) {
            beginDragEvent_base.Invoke();
        }
    }

    public void OnDeselect(BaseEventData eventData) {
        if (deselectEvent != null) {
            deselectEvent.Invoke(eventData);
        }

        if (deselectEvent_base != null) {
            deselectEvent_base.Invoke();
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (dragEvent != null) {
            dragEvent.Invoke(eventData);
        }

        if (dragEvent_base != null) {
            dragEvent_base.Invoke();
        }
    }

    public void OnDrop(PointerEventData eventData) {
        if (dropEvent != null) {
            dropEvent.Invoke(eventData);
        }

        if (dropEvent_base != null) {
            dropEvent_base.Invoke();
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (endDragEvent != null) {
            endDragEvent.Invoke(eventData);
        }

        if (endDragEvent_base != null) {
            endDragEvent_base.Invoke();
        }
    }

    public void OnInitializePotentialDrag(PointerEventData eventData) {
        if (initDragEvent != null) {
            initDragEvent.Invoke(eventData);
        }

        if (initDragEvent_base != null) {
            initDragEvent_base.Invoke();
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (clickEvent != null) {
            clickEvent.Invoke(eventData);
        }

        if (clickEvent_base != null) {
            clickEvent_base.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (downEvent != null) {
            downEvent.Invoke(eventData);
        }

        if (downEvent_base != null) {
            downEvent_base.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (enterEvent != null) {
            enterEvent.Invoke(eventData);
        }

        if (enterEvent_base != null) {
            enterEvent_base.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (exitEvent != null) {
            exitEvent.Invoke(eventData);
        }

        if (exitEvent_base != null) {
            exitEvent_base.Invoke();
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (upEvent != null) {
            upEvent.Invoke(eventData);
        }

        if (upEvent_base != null) {
            upEvent_base.Invoke();
        }
    }

    public void OnSelect(BaseEventData eventData) {
        if (selectEvent != null) {
            selectEvent.Invoke(eventData);
        }

        if (selectEvent_base != null) {
            selectEvent_base.Invoke();
        }
    }
}

public class PointerUIEvent : UnityEvent<PointerEventData> {
}
public class BaseUIEvent : UnityEvent<BaseEventData> {
}