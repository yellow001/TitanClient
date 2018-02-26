/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
// Simple custom action by Tobbe Olsson - www.tobbeo.net

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Subtracts a value from a Float Variable.")]
	public class FloatSubtract : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The float variable to subtract from.")]
		public FsmFloat floatVariable;

		[RequiredField]
        [Tooltip("Value to subtract from the float variable.")]
		public FsmFloat subtract;

        [Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

        [Tooltip("Used with Every Frame. Adds the value over one second to make the operation frame rate independent.")]
        public bool perSecond;

		public override void Reset()
		{
			floatVariable = null;
			subtract = null;
			everyFrame = false;
			perSecond = false;
		}

		public override void OnEnter()
		{
			DoFloatSubtract();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoFloatSubtract();
		}

		void DoFloatSubtract()
		{
			if (!perSecond)
			{
				floatVariable.Value -= subtract.Value;
			}
			else
			{
				floatVariable.Value -= subtract.Value * Time.deltaTime;
			}
		}
	}
}
