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
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts a Material variable to an Object variable. Useful if you want to use Set Property (which only works on Object variables).")]
	public class ConvertMaterialToObject : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Material variable to convert to an Object.")]
		public FsmMaterial materialVariable;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in an Object variable.")]
		public FsmObject objectVariable;
	
		[Tooltip("Repeat every frame. Useful if the Material variable is changing.")]
		public bool everyFrame;

		public override void Reset()
		{
			materialVariable = null;
			objectVariable = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoConvertMaterialToObject();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoConvertMaterialToObject();
		}

		void DoConvertMaterialToObject()
		{
			objectVariable.Value = materialVariable.Value;
		}
	}
}
