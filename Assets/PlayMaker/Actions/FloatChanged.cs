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
	[Tooltip("Tests if the value of a Float variable changed. Use this to send an event on change, or store a bool that can be used in other operations.")]
	public class FloatChanged : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The Float variable to watch for a change.")]
		public FsmFloat floatVariable;

        [Tooltip("Event to send if the float variable changes.")]
		public FsmEvent changedEvent;
		
        [UIHint(UIHint.Variable)]
        [Tooltip("Set to True if the float variable changes.")]
		public FsmBool storeResult;
		
		float previousValue;

		public override void Reset()
		{
			floatVariable = null;
			changedEvent = null;
			storeResult = null;
		}

		public override void OnEnter()
		{
			if (floatVariable.IsNone)
			{
				Finish();
				return;
			}
			
			previousValue = floatVariable.Value;
		}
		
		public override void OnUpdate()
		{
			storeResult.Value = false;
			
			if (floatVariable.Value != previousValue)
			{
				previousValue = floatVariable.Value;
				storeResult.Value = true;
				Fsm.Event(changedEvent);
			}
		}
	}
}

