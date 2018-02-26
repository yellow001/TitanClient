/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("GUILayout Password Field. Optionally send an event if the text has been edited.")]
	public class GUILayoutPasswordField : GUILayoutAction {

		[UIHint(UIHint.Variable)]
		[Tooltip("The password Text")]
		public FsmString text;

		[Tooltip("The Maximum Length of the field")]
		public FsmInt maxLength;

		[Tooltip("The Style of the Field")]
		public FsmString style;

		[Tooltip("Event sent when field content changed")]
		public FsmEvent changedEvent;

		[Tooltip("Replacement character to hide the password")]
		public FsmString mask;

		public override void Reset()
		{
			text = null;
			maxLength = 25;
			style = "TextField";
			mask = "*";
			changedEvent = null;
		}
		
		public override void OnGUI()
		{
			var guiChanged = GUI.changed;
			GUI.changed = false;
			
			text.Value = GUILayout.PasswordField(text.Value, mask.Value[0], style.Value, LayoutOptions);
			
			if (GUI.changed)
			{
				Fsm.Event(changedEvent);
				GUIUtility.ExitGUI();
			}
			else
			{
				GUI.changed = guiChanged;
			}
		}
	}
}
