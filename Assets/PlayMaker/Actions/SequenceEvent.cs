/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sends the next event on the state each time the state is entered.")]
	public class SequenceEvent : FsmStateAction
	{
		[HasFloatSlider(0, 10)]
		public FsmFloat delay;

		DelayedEvent delayedEvent;
		int eventIndex;

		public override void Reset()
		{
			delay = null;
		}

		public override void OnEnter()
		{
			var eventCount = State.Transitions.Length;

			if (eventCount > 0)
			{
				var fsmEvent = State.Transitions[eventIndex].FsmEvent;
				
				if (delay.Value < 0.001f)
				{
					Fsm.Event(fsmEvent);
					Finish();
				}
				else
				{
					delayedEvent = Fsm.DelayedEvent(fsmEvent, delay.Value);
				}
				
				eventIndex++;
				if (eventIndex == eventCount)
				{
					eventIndex = 0;
				}
			}
		}

		public override void OnUpdate()
		{
			if (DelayedEvent.WasSent(delayedEvent))
			{
				Finish();
			}
		}
	}
}
