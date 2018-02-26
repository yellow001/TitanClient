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
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Sets the gravity vector, or individual axis.")]
	public class SetGravity2d : FsmStateAction
	{
		[Tooltip("Gravity as Vector2.")]
		public FsmVector2 vector;

		[Tooltip("Override the x value of the gravity")]
		public FsmFloat x;
		[Tooltip("Override the y value of the gravity")]
		public FsmFloat y;

		[Tooltip("Repeat every frame")]
		public bool everyFrame;
		
		public override void Reset()
		{
			vector = null;
			x = new FsmFloat { UseVariable = true };
			y = new FsmFloat { UseVariable = true };
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoSetGravity();
			
			if (!everyFrame)
				Finish();		
		}
		
		public override void OnUpdate()
		{
			DoSetGravity();
		}
		
		void DoSetGravity()
		{
			Vector2 gravity = vector.Value;
			
			if (!x.IsNone)
				gravity.x = x.Value;
			if (!y.IsNone)
				gravity.y = y.Value;

			Physics2D.gravity = gravity;
		}
	}
}
