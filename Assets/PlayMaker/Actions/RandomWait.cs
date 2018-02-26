/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
// Thanks derkoi:
// http://hutonggames.com/playmakerforum/index.php?topic=4700.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Time)]
    [Tooltip("Delays a State from finishing by a random time. NOTE: Other actions continue, but FINISHED can't happen before Time.")]
    public class RandomWait : FsmStateAction
    {
        
	    [RequiredField]
        [Tooltip("Minimum amount of time to wait.")]
		public FsmFloat min;

		[RequiredField]
        [Tooltip("Maximum amount of time to wait.")]
		public FsmFloat max;

        [Tooltip("Event to send when timer is finished.")]
        public FsmEvent finishEvent;

        [Tooltip("Ignore time scale.")]
        public bool realTime;
		

        private float startTime;
        private float timer;
		private float time;

        public override void Reset()
        {
            min = 0f;
			max = 1f;
            finishEvent = null;
            realTime = false;
        }

        public override void OnEnter()
        {
			time = Random.Range(min.Value, max.Value);
			
            if (time <= 0)
            {
                Fsm.Event(finishEvent);
                Finish();
                return;
            }

            startTime = FsmTime.RealtimeSinceStartup;
            timer = 0f;
        }

        public override void OnUpdate()
        {
            // update time

            if (realTime)
            {
                timer = FsmTime.RealtimeSinceStartup - startTime;
            }
            else
            {
                timer += Time.deltaTime;
            }

            if (timer >= time)
            {
                Finish();
                if (finishEvent != null)
                {
                    Fsm.Event(finishEvent);
                }
            }
        }

    }
}
