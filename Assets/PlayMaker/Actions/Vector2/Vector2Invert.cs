/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Reverses the direction of a Vector2 Variable. Same as multiplying by -1.")]
	public class Vector2Invert : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector to invert")]
		public FsmVector2 vector2Variable;

		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			vector2Variable = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			vector2Variable.Value = vector2Variable.Value * -1;
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			vector2Variable.Value = vector2Variable.Value *  -1;
		}
	}
}
