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
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets the value of a Bool Variable.")]
	public class SetBoolValue : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmBool boolVariable;
		[RequiredField]
		public FsmBool boolValue;
		public bool everyFrame;

		public override void Reset()
		{
			boolVariable = null;
			boolValue = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			boolVariable.Value = boolValue.Value;
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			boolVariable.Value = boolValue.Value;
		}
	}
}
