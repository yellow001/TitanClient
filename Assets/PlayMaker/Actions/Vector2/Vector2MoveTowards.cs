/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Moves a Vector2 towards a Target. Optionally sends an event when successful.")]
	public class Vector2MoveTowards : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Vector2 to Move")]
		public FsmVector2 source;
		
		[Tooltip("A target Vector2 to move towards.")]
		public FsmVector2 target;
		
		[HasFloatSlider(0, 20)]
		[Tooltip("The maximum movement speed. HINT: You can make this a variable to change it over time.")]
		public FsmFloat maxSpeed;
		
		[HasFloatSlider(0, 5)]
		[Tooltip("Distance at which the move is considered finished, and the Finish Event is sent.")]
		public FsmFloat finishDistance;
		
		[Tooltip("Event to send when the Finish Distance is reached.")]
		public FsmEvent finishEvent;

		public override void Reset()
		{
			source = null;
			target = null;
			maxSpeed = 10f;
			finishDistance = 1f;
			finishEvent = null;
		}

		public override void OnUpdate()
		{
			DoMoveTowards();
		}

		void DoMoveTowards()
		{
			source.Value = Vector2.MoveTowards(source.Value, target.Value, maxSpeed.Value * Time.deltaTime);
			
			var distance = (source.Value - target.Value).magnitude;
			if (distance < finishDistance.Value)
			{
				Fsm.Event(finishEvent);
				Finish();
			}
		}

	}
}
