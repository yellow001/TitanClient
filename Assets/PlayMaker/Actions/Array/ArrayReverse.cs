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
	[Tooltip("Reverse the order of items in an Array.")]
	public class ArrayReverse : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array to reverse.")]
		public FsmArray array;
			
		public override void Reset()
		{
			array = null;
		}
		
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			var _list = new List<object>(array.Values);
			_list.Reverse();			
			array.Values = _list.ToArray();
			
			Finish();
		}
	}
}
