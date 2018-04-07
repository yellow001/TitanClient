using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeEventMgr : BaseManager<TimeEventMgr> {

    static List<TimeEvent> update_Events=new List<TimeEvent>();
    static List<TimeEvent> rm_Update_Events = new List<TimeEvent>();

    static List<TimeEvent> fixed_Events = new List<TimeEvent>();
    static List<TimeEvent> rm_Fixed_Events = new List<TimeEvent>();

    static List<TimeEvent> late_Events = new List<TimeEvent>();
    static List<TimeEvent> rm_Late_Events = new List<TimeEvent>();


    public override void Init() {
        SceneManager.sceneUnloaded += SceneChange;
        base.Init();
    }

    private void SceneChange(Scene arg0) {
        Clear();
    }

    // Update is called once per frame
    void Update () {

        if (rm_Update_Events != null && rm_Update_Events.Count > 0) {
            foreach (TimeEvent item in rm_Update_Events) {
                if (update_Events.Contains(item)) {
                    update_Events.Remove(item);
                }
            }
            rm_Update_Events.Clear();
        }

        #region Old
        //if (tEvents != null && tEvents.Count > 0) {

        //    for (int i = 0; i < tEvents.Count; i++) {
        //        TimeEvent item = tEvents[i];

        //        //如果是立即执行
        //        if (item.now) {
        //            if (item.overDe != null) {
        //                item.overDe();
        //            }
        //            item.now = false;
        //            item.count = item.count > 0 ? --item.count : item.count;
        //            if (tEvents[i].count == 0) {
        //                RemoveTimeEvent(item);
        //            }
        //        }

        //        //已经到达时间
        //        if (item.deltaTime >= item.waitTime) {
        //            //执行方法，并移除model
        //            try {
        //                item.overDe?.Invoke();
        //            }
        //            catch (System.Exception ex) {
        //                RemoveTimeEvent(item);
        //                return;
        //            }

        //            item.deltaTime = 0;
        //            item.count = item.count > 0 ? --item.count : item.count;
        //            if (item.count == 0) {
        //                RemoveTimeEvent(item);
        //            }


        //        }
        //        else {
        //            float deltaTime = item.ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
        //            item.deltaTime += deltaTime;

        //            if (item.updateDe != null) {

        //                float leaveTime = item.waitTime - item.deltaTime;

        //                leaveTime = (int)(leaveTime * 100) / 100f;

        //                float percent = (item.deltaTime) / item.waitTime;

        //                percent = (int)(percent * 100) / 100f;//0.1~1

        //                try {
        //                    item.updateDe(Mathf.Max(0, leaveTime), Mathf.Min(1, percent));
        //                }
        //                catch (System.Exception ex) {
        //                    Debug.Log(ex);
        //                    RemoveTimeEvent(item);
        //                }

        //            }
        //        }
        //    }
        //}
        #endregion

        ExcuteTimeEvent(update_Events, Time.deltaTime,Time.unscaledDeltaTime);
    }

    private void FixedUpdate() {
        if (rm_Fixed_Events != null && rm_Fixed_Events.Count > 0) {
            foreach (TimeEvent item in rm_Fixed_Events) {
                if (fixed_Events.Contains(item)) {
                    fixed_Events.Remove(item);
                }
            }
            rm_Fixed_Events.Clear();
        }

        ExcuteTimeEvent(fixed_Events, Time.fixedDeltaTime, Time.fixedUnscaledDeltaTime);
    }

    private void LateUpdate() {
        if (rm_Late_Events != null && rm_Late_Events.Count > 0) {
            foreach (TimeEvent item in rm_Late_Events) {
                if (late_Events.Contains(item)) {
                    late_Events.Remove(item);
                }
            }
            rm_Late_Events.Clear();
        }

        ExcuteTimeEvent(late_Events, Time.deltaTime, Time.unscaledDeltaTime);
    }

    void ExcuteTimeEvent(List<TimeEvent> events, float time,float unscaleTime) {
        if (events != null && events.Count > 0) {

            for (int i = 0; i < events.Count; i++) {
                TimeEvent item = events[i];

                //如果是立即执行
                if (item.now) {
                    if (item.overDe != null) {
                        item.overDe();
                    }
                    item.now = false;
                    item.count = item.count > 0 ? --item.count : item.count;
                    if (update_Events[i].count == 0) {
                        RemoveTimeEvent(item);
                    }
                }

                //已经到达时间
                if (item.deltaTime >= item.waitTime) {
                    //执行方法，并移除model
                    try {
                        item.overDe?.Invoke();
                    }
                    catch (System.Exception ex) {
                        RemoveTimeEvent(item);
                        return;
                    }

                    item.deltaTime = 0;
                    item.count = item.count > 0 ? --item.count : item.count;
                    if (item.count == 0) {
                        RemoveTimeEvent(item);
                    }


                }
                else {
                    float deltaTime = item.ignoreTimeScale ? unscaleTime : time;
                    item.deltaTime += deltaTime;

                    if (item.updateDe != null) {

                        float leaveTime = item.waitTime - item.deltaTime;

                        leaveTime = (int)(leaveTime * 100) / 100f;

                        float percent = (item.deltaTime) / item.waitTime;

                        percent = (int)(percent * 100) / 100f;//0.1~1

                        try {
                            item.updateDe(Mathf.Max(0, leaveTime), Mathf.Min(1, percent));
                        }
                        catch (System.Exception ex) {
                            Debug.Log(ex);
                            RemoveTimeEvent(item);
                        }

                    }
                }
            }
        }
    }


    public void AddTimeEvent(TimeEvent model,ExcuteMode mode=ExcuteMode.Update) {
        if (model == null) {
            Debug.Log("the model you add is null");
            return;
        }
        model.deltaTime = 0;

        switch (mode) {
            case ExcuteMode.Update:
                if (!update_Events.Contains(model)) {
                    update_Events.Add(model);
                }
                break;
            case ExcuteMode.FixedUpdate:
                if (!fixed_Events.Contains(model)) {
                    fixed_Events.Add(model);
                }
                break;
            case ExcuteMode.LateUpdate:
                if (!late_Events.Contains(model)) {
                    late_Events.Add(model);
                }
                break;
            default:
                break;
        }

        
    }

    public void RemoveTimeEvent(TimeEvent model) {
        if (update_Events.Contains(model) && !rm_Update_Events.Contains(model)) {
            rm_Update_Events.Add(model);
        }
        else if (fixed_Events.Contains(model) && !rm_Fixed_Events.Contains(model)) {
            rm_Fixed_Events.Add(model);
        }
        else if (late_Events.Contains(model) && !rm_Late_Events.Contains(model)) {
            rm_Late_Events.Add(model);
        }
    }

    public void Clear() {
        update_Events.Clear();
        rm_Update_Events.Clear();

        fixed_Events.Clear();
        rm_Fixed_Events.Clear();

        late_Events.Clear();
        rm_Late_Events.Clear();
    }

    private void OnDestroy() {
        Clear();
    }

    
}

public enum ExcuteMode {
    Update,
    FixedUpdate,
    LateUpdate
}
