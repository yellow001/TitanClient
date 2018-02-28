/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Select a Random Vector2 from a Vector2 array.")]
	public class SelectRandomVector2 : FsmStateAction
	{
		[Tooltip("The array of Vectors and respective weights")]
		[CompoundArray("Vectors", "Vector", "Weight")]
		public FsmVector2[] vector2Array;
		[HasFloatSlider(0, 1)]
		public FsmFloat[] weights;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The picked vector2")]
		public FsmVector2 storeVector2;
		
		public override void Reset()
		{
			vector2Array = new FsmVector2[3];
			weights = new FsmFloat[] {1,1,1};
			storeVector2 = null;
		}

		public override void OnEnter()
		{
			DoSelectRandomColor();
			Finish();
		}
		
		void DoSelectRandomColor()
		{
			if (vector2Array == null) return;
			if (vector2Array.Length == 0) return;
			if (storeVector2 == null) return;

			int randomIndex = ActionHelpers.GetRandomWeightedIndex(weights);
			
			if (randomIndex != -1)
			{
				storeVector2.Value = vector2Array[randomIndex].Value;
			}
		}
	}
}