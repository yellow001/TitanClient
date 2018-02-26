/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Get a Random item from an Array.")]
	public class ArrayGetRandom : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array to use.")]
		public FsmArray array;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the value in a variable.")]
        [MatchElementType("array")]
        public FsmVar storeValue;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		public override void Reset()
		{
			array = null;
			storeValue =null;
            everyFrame = false;
		}
		
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			DoGetRandomValue();
			
			if (!everyFrame)
			{
				Finish();
			}
			
		}
		
		public override void OnUpdate()
		{
			DoGetRandomValue();
			
		}
		
		private void DoGetRandomValue()
		{
			if (storeValue.IsNone)
			{
				return;
			}

			storeValue.SetValue(array.Get(Random.Range(0,array.Length)));	
		}

		
	}
}

