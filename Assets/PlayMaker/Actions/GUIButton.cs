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
	[Tooltip("GUI button. Sends an Event when pressed. Optionally store the button state in a Bool Variable.")]
	public class GUIButton : GUIContentAction
	{
		public FsmEvent sendEvent;
		[UIHint(UIHint.Variable)]
		public FsmBool storeButtonState;
	
		public override void Reset()
		{
			base.Reset();
			sendEvent = null;
			storeButtonState = null;
			style = "Button";
		}
		
		public override void OnGUI()
		{
			base.OnGUI();
			
			bool pressed = false;
			
			if (GUI.Button(rect, content, style.Value))
			{
				Fsm.Event(sendEvent);
				pressed = true;
			}
			
			if (storeButtonState != null)
			{
				storeButtonState.Value = pressed;
			}
		}
	}
}
