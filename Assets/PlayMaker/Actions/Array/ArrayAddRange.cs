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
	[Tooltip("Add values to an array.")]
	public class ArrayAddRange : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;
		
		[RequiredField]
		[MatchElementType("array")]
		[Tooltip("The variables to add.")]
		public FsmVar[] variables;
		
		public override void Reset()
		{
			array = null;
			variables = new FsmVar[2];
		}

		public override void OnEnter()
		{
			DoAddRange();
			
			Finish();
		}
		
		private void DoAddRange()
		{
			int count = variables.Length;

			if (count>0)
			{
				array.Resize(array.Length+count);

				foreach(FsmVar _var in variables)
				{
                    _var.UpdateValue();
					array.Set(array.Length-count,_var.GetValue());
					count--;
				}
			}

		}
		
		
	}
}
