using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UGUITween : MonoBehaviour {
    public float delay = 0;
    public float duration = 1f;

    /// <summary>
    /// 0和1均是执行一次，负数表示一直执行
    /// </summary>
    public int loopCount = 1;

    public bool ignoreTime = false;

    public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);
    public UnityEvent onFinish;

    protected TimeEvent forwardAni, reverseAni;

    public virtual void PlayFroward() {
        currentCount = loopCount;

        //delay秒后，以duration为周期播放动画
        this.AddTimeEvent(delay, () => {

            this.AddTimeEvent(forwardAni);

        }, null, ignoreTime);
    }

    public virtual void PlayReverse() {
        currentCount = loopCount;

        //delay秒后，以duration为周期播放动画
        this.AddTimeEvent(delay, () => {

            this.AddTimeEvent(reverseAni);

        }, null, ignoreTime);
    }

    /// <summary>
    /// 当前动画剩余次数
    /// </summary>
    protected int currentCount;

    /// <summary>
    /// 停止连续动画用
    /// </summary>
    /// <param name="hideObj">是否隐藏物体</param>
    public virtual void Stop(bool hideObj = false) {
        this.RemoveTimeEvent(forwardAni);
        this.RemoveTimeEvent(reverseAni);
        if (hideObj) {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 再次展示连续动画
    /// </summary>
    /// <param name="reverse">播放反转动画</param>
    public virtual void Play(bool reverse = false) {
        gameObject.SetActive(true);
        if (!reverse) {
            PlayFroward();
        }
        else {
            PlayReverse();
        }
    }

    public virtual void Start() {
        loopCount = loopCount == 0 ? 1 : loopCount;
        Init();
    }

    public virtual void Init() { }

    public enum EM_LoopType {
        PingPong,
        Restart
    }


}
