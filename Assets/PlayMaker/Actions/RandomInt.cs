/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
// 'inclusiveMax' option added by MaDDoX (@brenoazevedo)

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets an Integer Variable to a random value between Min/Max.")]
	public class RandomInt : FsmStateAction
	{
		[RequiredField]
		public FsmInt min;
		[RequiredField]
		public FsmInt max;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt storeResult;
        [Tooltip("Should the Max value be included in the possible results?")]
        public bool inclusiveMax;

		public override void Reset()
		{
			min = 0;
			max = 100;
			storeResult = null;
			// make default false to not break old behavior.
		    inclusiveMax = false;
		}

		public override void OnEnter()
		{
            storeResult.Value = (inclusiveMax) ? 
                Random.Range(min.Value, max.Value + 1) : 
                Random.Range(min.Value, max.Value);
		    
            Finish();
		}
	}
}
