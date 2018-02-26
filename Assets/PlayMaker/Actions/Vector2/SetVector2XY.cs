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
	[Tooltip("Sets the XY channels of a Vector2 Variable. To leave any channel unchanged, set variable to 'None'.")]
	public class SetVector2XY : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector2 target")]
		public FsmVector2 vector2Variable;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector2 source")]
		public FsmVector2 vector2Value;
		
		[Tooltip("The x component. Override vector2Value if set")]
		public FsmFloat x;
		
		[Tooltip("The y component.Override vector2Value if set")]
		public FsmFloat y;
		
		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			vector2Variable = null;
			vector2Value = null;
			x = new FsmFloat{ UseVariable = true};
			y = new FsmFloat{ UseVariable = true};
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetVector2XYZ();
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			DoSetVector2XYZ();
		}

		void DoSetVector2XYZ()
		{
			if (vector2Variable == null) return;
			
			Vector2 newVector2 = vector2Variable.Value;
			
			if (!vector2Value.IsNone) newVector2 = vector2Value.Value;
			if (!x.IsNone) newVector2.x = x.Value;
			if (!y.IsNone) newVector2.y = y.Value;
			
			vector2Variable.Value = newVector2;
		}
	}
}
