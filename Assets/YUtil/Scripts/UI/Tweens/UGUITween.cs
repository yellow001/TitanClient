using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UGUITween : MonoBehaviour {
    public float delay = 0;
    public float duration=1f;

    /// <summary>
    /// 0和1均是执行一次，负数表示一直执行
    /// </summary>
    public int loopCount = 1;

    public bool ignoreTime = false;

    public AnimationCurve curve =AnimationCurve.Linear(0,0,1,1);
    public UnityEvent onFinish;

    public virtual void PlayFroward() { }

    public virtual void PlayReverse() { }

    /// <summary>
    /// 当前动画剩余次数
    /// </summary>
    protected int currentCount;

    /// <summary>
    /// 停止连续动画用
    /// </summary>
    public virtual void Hide() {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 再次展示连续动画用
    /// </summary>
    public virtual void Show() {
        gameObject.SetActive(true);
        if (currentCount == 0) {
            PlayFroward();
        }
    }

    public virtual void Start() {
        loopCount = loopCount == 0 ? 1 : loopCount;
    }

    public enum EM_LoopType {
        PingPong,
        Restart
    }


}
