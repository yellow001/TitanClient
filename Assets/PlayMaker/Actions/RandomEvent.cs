/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sends a Random State Event after an optional delay. Use this to transition to a random state from the current state.")]
	public class RandomEvent : FsmStateAction
	{
		[HasFloatSlider(0, 10)]
        [Tooltip("Delay before sending the event.")]
		public FsmFloat delay;

        [Tooltip("Don't repeat the same event twice in a row.")]
	    public FsmBool noRepeat;

		private DelayedEvent delayedEvent;
	    private int randomEventIndex;
        private int lastEventIndex = -1;
        
		public override void Reset()
		{
			delay = null;
		}

		public override void OnEnter()
		{
			if (State.Transitions.Length == 0)
			{
				return;
			}
			
            if (lastEventIndex == -1)
            {
                lastEventIndex = Random.Range(0, State.Transitions.Length);
            }

			if (delay.Value < 0.001f)
			{
				Fsm.Event(GetRandomEvent());
				Finish();
			}
			else
			{
				delayedEvent = Fsm.DelayedEvent(GetRandomEvent(), delay.Value);
			}
		}

		public override void OnUpdate()
		{
			if (DelayedEvent.WasSent(delayedEvent))
			{
				Finish();
			}
		}

		FsmEvent GetRandomEvent()
		{
            do
            {
                randomEventIndex = Random.Range(0, State.Transitions.Length);
            } while (noRepeat.Value && State.Transitions.Length > 1 && randomEventIndex == lastEventIndex);

            lastEventIndex = randomEventIndex;

            return State.Transitions[randomEventIndex].FsmEvent;
		}

	}
}
