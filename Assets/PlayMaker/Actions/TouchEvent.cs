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

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Sends events based on Touch Phases. Optionally filter by a fingerID.")]
	public class TouchEvent : FsmStateAction
	{
		public FsmInt fingerId;
		public TouchPhase touchPhase;
		public FsmEvent sendEvent;
		[UIHint(UIHint.Variable)]
		public FsmInt storeFingerId;
		
		public override void Reset()
		{
			fingerId = new FsmInt { UseVariable = true } ;
			storeFingerId = null;
		}

		public override void OnUpdate()
		{
			if (Input.touchCount > 0)
			{
				foreach (var touch in Input.touches)
				{

					if (fingerId.IsNone || touch.fingerId == fingerId.Value)
					{
						if (touch.phase == touchPhase)
						{
							storeFingerId.Value = touch.fingerId;
							Fsm.Event(sendEvent);
						}
					}
				}
			}
		}
	}
}
