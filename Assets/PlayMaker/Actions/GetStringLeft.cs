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
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Gets the Left n characters from a String Variable.")]
	public class GetStringLeft : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;
		
        [Tooltip("Number of characters to get.")]
        public FsmInt charCount;
		
        [RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString storeResult;
		
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

		public override void Reset()
		{
			stringVariable = null;
			charCount = 0;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetStringLeft();

		    if (!everyFrame)
		    {
		        Finish();
		    }
		}

		public override void OnUpdate()
		{
			DoGetStringLeft();
		}
		
		void DoGetStringLeft()
		{
			if (stringVariable.IsNone) return;
			if (storeResult.IsNone) return;

			storeResult.Value = stringVariable.Value.Substring(0, Mathf.Clamp(charCount.Value, 0, stringVariable.Value.Length));
		}
		
	}
}