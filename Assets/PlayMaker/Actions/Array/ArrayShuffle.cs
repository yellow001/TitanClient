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
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Shuffle values in an array. Optionally set a start index and range to shuffle only part of the array.")]
	public class ArrayShuffle : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array to shuffle.")]
		public FsmArray array;

		[Tooltip("Optional start Index for the shuffling. Leave it to none or 0 for no effect")]
		public FsmInt startIndex;
		
		[Tooltip("Optional range for the shuffling, starting at the start index if greater than 0. Leave it to none or 0 for no effect, it will shuffle the whole array")]
		public FsmInt shufflingRange;
	
		public override void Reset()
		{
			array = null;
			startIndex = new FsmInt {UseVariable=true};
			shufflingRange = new FsmInt {UseVariable=true};
		}
			
		// Code that runs on entering the state.
		public override void OnEnter()
		{		
			List<object> _list = new List<object>(array.Values);
			
			int start = 0;
			int end = _list.Count-1;
			
			if (startIndex.Value>0)
			{
				start = Mathf.Min(startIndex.Value,end);
			}
			
			if (shufflingRange.Value>0)
			{
				end = Mathf.Min(_list.Count-1,start + shufflingRange.Value);
				
			}

			// Knuth-Fisher-Yates algo
			
			//	for (int i = proxy.arrayList.Count - 1; i > 0; i--)
			for (int i = end; i > start; i--)
			{
				// Set swapWithPos a random position such that 0 <= swapWithPos <= i
				int swapWithPos = Random.Range(start,i + 1);
				
				// Swap the value at the "current" position (i) with value at swapWithPos
				object tmp = _list[i];
				_list[i] = _list[swapWithPos];
				_list[swapWithPos] = tmp;
			}

			array.Values = _list.ToArray();
			
			Finish();			
		}
	}
}
