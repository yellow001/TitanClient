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
	[ActionCategory(ActionCategory.Color)]
	[Tooltip("Get the RGBA channels of a Color Variable and store them in Float Variables.")]
	public class GetColorRGBA : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The Color variable.")]
		public FsmColor color;

		[UIHint(UIHint.Variable)]
        [Tooltip("Store the red channel in a float variable.")]
		public FsmFloat storeRed;	
	
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the green channel in a float variable.")]
		public FsmFloat storeGreen;	
	
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the blue channel in a float variable.")]
		public FsmFloat storeBlue;
		
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the alpha channel in a float variable.")]
		public FsmFloat storeAlpha;

        [Tooltip("Repeat every frame. Useful if the color variable is changing.")]
		public bool everyFrame;
		
		public override void Reset()
		{
			color = null;
			storeRed = null;
			storeGreen = null;
			storeBlue = null;
			storeAlpha = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetColorRGBA();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate ()
		{
			DoGetColorRGBA();
		}
		
		void DoGetColorRGBA()
		{
			if (color.IsNone)
			{
				return;
			}
			
			storeRed.Value = color.Value.r;
			storeGreen.Value = color.Value.g;
			storeBlue.Value = color.Value.b;
			storeAlpha.Value = color.Value.a;
		}
	}
}
