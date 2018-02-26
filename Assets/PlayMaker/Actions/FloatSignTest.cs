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
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends Events based on the sign of a Float.")]
	public class FloatSignTest : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The float variable to test.")]
		public FsmFloat floatValue;

        [Tooltip("Event to send if the float variable is positive.")]
		public FsmEvent isPositive;

        [Tooltip("Event to send if the float variable is negative.")]
		public FsmEvent isNegative;

        [Tooltip("Repeat every frame. Useful if the variable is changing and you're waiting for a particular result.")]
		public bool everyFrame;

		public override void Reset()
		{
			floatValue = 0f;
			isPositive = null;
			isNegative = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSignTest();
			
			if (!everyFrame)
			{
			    Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSignTest();
		}

		void DoSignTest()
		{
			if (floatValue == null)
			{
			    return;
			}
			
			Fsm.Event(floatValue.Value < 0 ? isNegative : isPositive);
		}

		public override string ErrorCheck()
		{
			if (FsmEvent.IsNullOrEmpty(isPositive) &&
				FsmEvent.IsNullOrEmpty(isNegative))
				return "Action sends no events!";
			return "";
		}
	}
}
