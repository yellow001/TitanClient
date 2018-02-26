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
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Multiplies a Float by Time.deltaTime to use in frame-rate independent operations. E.g., 10 becomes 10 units per second.")]
	public class PerSecond : FsmStateAction
	{
		[RequiredField]
		public FsmFloat floatValue;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeResult;
		public bool everyFrame;

		public override void Reset()
		{
			floatValue = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoPerSecond();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoPerSecond();
		}
		
		void DoPerSecond()
		{
			if (storeResult == null) return;
			
			storeResult.Value = floatValue.Value * Time.deltaTime;
		}
	}
}
