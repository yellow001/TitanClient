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
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Sets the Tinting Color for all background elements rendered by the GUI. By default only effects GUI rendered by this FSM, check Apply Globally to effect all GUI controls.")]
	public class SetGUIBackgroundColor : FsmStateAction
	{
		[RequiredField]
		public FsmColor backgroundColor;
		public FsmBool applyGlobally;
	
		public override void Reset()
		{
			backgroundColor = Color.white;
		}

		public override void OnGUI()
		{
			GUI.backgroundColor = backgroundColor.Value;
			
			if (applyGlobally.Value)
			{
				PlayMakerGUI.GUIBackgroundColor = GUI.backgroundColor;
			}
		}
	}
}