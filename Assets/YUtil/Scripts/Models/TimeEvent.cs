using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEvent{
    /// <summary>
    /// 等待执行时间
    /// </summary>
    public float waitTime;

    /// <summary>
    /// 累计过去的时间
    /// </summary>
    public float deltaTime;

    /// <summary>
    /// 完成倒计时后的调用的事件
    /// </summary>
    public Action overDe;

    /// <summary>
    /// 事件倒计时的帧事件，可获取倒计时
    /// </summary>
    public Action<float,float> updateDe;

    /// <summary>
    /// 执行次数 （负数 循环执行）
    /// </summary>
    public int count;

    /// <summary>
    /// 是否立即执行
    /// </summary>
    public bool now;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="t">触发时间间隔</param>
    /// <param name="over_de">触发事件</param>
    /// <param name="c">触发次数</param>
    /// <param name="n">是否立即执行</param>
    /// <param name="up_De">每帧调用事件</param>
    public TimeEvent(float t, Action over_de, Action<float,float> up_De = null,int c=1,bool n=false) {
        waitTime = t;
        overDe = over_de;
        now = n;
        if (c == 0) {
            c = 1;
        }
        count = c;
        updateDe = up_De;
    }
}
