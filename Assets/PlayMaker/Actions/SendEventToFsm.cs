/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[Obsolete("This action is obsolete; use Send Event with Event Target instead.")]
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sends an Event to another Fsm after an optional delay. Specify an Fsm Name or use the first Fsm on the object.")]
	public class SendEventToFsm : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of Fsm on Game Object")]
		public FsmString fsmName;
		[RequiredField]
		[UIHint(UIHint.FsmEvent)]
		public FsmString sendEvent;
		[HasFloatSlider(0, 10)]
		public FsmFloat delay;
		bool requireReceiver;

		private GameObject go;
		private DelayedEvent delayedEvent;

		public override void Reset()
		{
			gameObject = null;
			fsmName = null;
			sendEvent = null;
			delay = null;
			requireReceiver = false;
		}

		public override void OnEnter()
		{
			go = Fsm.GetOwnerDefaultTarget(gameObject);

			if (go == null)
			{
				Finish();
				return;
			}
			
			var sendToFsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
			
			if (sendToFsm == null)
			{
				if (requireReceiver)
				{
					LogError("GameObject doesn't have FsmComponent: " + go.name + " " + fsmName.Value);
				}

				return;
			}

			if (delay.Value < 0.001)
			{
				sendToFsm.Fsm.Event(sendEvent.Value);
				Finish();
			}
			else
			{
				delayedEvent = sendToFsm.Fsm.DelayedEvent(FsmEvent.GetFsmEvent(sendEvent.Value), delay.Value);
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
