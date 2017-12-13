using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEventMgr : BaseManager<TimeEventMgr> {

    static List<TimeEvent> tEvents=new List<TimeEvent>();
    static List<TimeEvent> rmTEvents = new List<TimeEvent>();
    
	
	// Update is called once per frame
	void Update () {

        if (rmTEvents != null && rmTEvents.Count > 0) {
            foreach (TimeEvent item in rmTEvents) {
                if (tEvents.Contains(item)) {
                    tEvents.Remove(item);
                }
            }
            rmTEvents.Clear();
        }

        if (tEvents != null && tEvents.Count > 0) {

            for (int i = 0; i < tEvents.Count; i++) {
                TimeEvent item = tEvents[i];

                //如果是立即执行
                if (item.now) {
                    if (item.overDe != null) {
                        item.overDe();
                    }
                    item.now = false;
                    item.count = item.count > 0 ? --item.count : item.count;
                    if (tEvents[i].count == 0) {
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
                    float deltaTime = item.ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
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
                            RemoveTimeEvent(item);
                        }
                        
                    }
                }
            }
        }
    }

    public void AddTimeEvent(TimeEvent model) {
        if (model == null) {
            Debug.Log("the model you add is null");
            return;
        }
        model.deltaTime = 0;
        if (!tEvents.Contains(model)) {
            tEvents.Add(model);
        }
    }

    public void RemoveTimeEvent(TimeEvent model) {
        if (!rmTEvents.Contains(model)) {
            rmTEvents.Add(model);
        }
    }

    public void Clear() {
        tEvents.Clear();
        rmTEvents.Clear();
    }

    private void OnDestroy() {
        Clear();
    }
}
