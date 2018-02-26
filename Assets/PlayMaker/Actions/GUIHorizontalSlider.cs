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
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("GUI Horizontal Slider connected to a Float Variable.")]
	public class GUIHorizontalSlider : GUIAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat floatVariable;
		[RequiredField]
		public FsmFloat leftValue;
		[RequiredField]
		public FsmFloat rightValue;
		public FsmString sliderStyle;
		public FsmString thumbStyle;
		
		public override void Reset()
		{
			base.Reset();
			floatVariable = null;
			leftValue = 0f;
			rightValue = 100f;
			sliderStyle = "horizontalslider";
			thumbStyle = "horizontalsliderthumb";
		}
		
		public override void OnGUI()
		{
			base.OnGUI();
			
			if(floatVariable != null)
			{
				floatVariable.Value = GUI.HorizontalSlider(rect, floatVariable.Value, leftValue.Value, rightValue.Value, 
					sliderStyle.Value != "" ? sliderStyle.Value : "horizontalslider", 
					thumbStyle.Value != "" ? thumbStyle.Value : "horizontalsliderthumb");
			}
		}
	}
}
