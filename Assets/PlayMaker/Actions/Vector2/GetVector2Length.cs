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
	[Tooltip("Get Vector2 Length.")]
	public class GetVector2Length : FsmStateAction
	{
		[Tooltip("The Vector2 to get the length from")]
		public FsmVector2 vector2;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Vector2 the length")]
		public FsmFloat storeLength;
		
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
		
		
		public override void Reset()
		{
			vector2 = null;
			storeLength = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoVectorLength();
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoVectorLength();
		}
		
		void DoVectorLength()
		{
			if (vector2 == null) return;
			if (storeLength == null) return;
			storeLength.Value = vector2.Value.magnitude;
		}
	}
}
