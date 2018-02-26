/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Clamps the Magnitude of Vector2 Variable.")]
	public class Vector2ClampMagnitude : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Vector2")]
		public FsmVector2 vector2Variable;
		
		[RequiredField]
		[Tooltip("The maximum Magnitude")]
		public FsmFloat maxLength;
		
		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			vector2Variable = null;
			maxLength = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoVector2ClampMagnitude();
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			DoVector2ClampMagnitude();
		}
		
		void DoVector2ClampMagnitude()
		{
			vector2Variable.Value = Vector2.ClampMagnitude(vector2Variable.Value, maxLength.Value);
		}
	}
}

