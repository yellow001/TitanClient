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
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout Repeat Button. Sends an Event while pressed. Optionally store the button state in a Bool Variable.")]
	public class GUILayoutRepeatButton : GUILayoutAction
	{
		public FsmEvent sendEvent;
		[UIHint(UIHint.Variable)]
		public FsmBool storeButtonState;
		public FsmTexture image;
		public FsmString text;
		public FsmString tooltip;
		public FsmString style;

		public override void Reset()
		{
			base.Reset();
			sendEvent = null;
			storeButtonState = null;
			text = "";
			image = null;
			tooltip = "";
			style = "";
		}
		
		public override void OnGUI()
		{
			bool buttonPressed;
			
			if (string.IsNullOrEmpty(style.Value))
			{
				buttonPressed = GUILayout.RepeatButton(new GUIContent(text.Value, image.Value, tooltip.Value), LayoutOptions);
			}
			else
			{
				buttonPressed = GUILayout.RepeatButton(new GUIContent(text.Value, image.Value, tooltip.Value), style.Value, LayoutOptions);
			}

			if (buttonPressed)
			{
				Fsm.Event(sendEvent);
			}

			storeButtonState.Value = buttonPressed;
		}
	}
}
