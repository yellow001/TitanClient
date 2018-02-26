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
	[Tooltip("Begins a ScrollView. Use GUILayoutEndScrollView at the end of the block.")]
	public class GUILayoutBeginScrollView : GUILayoutAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Assign a Vector2 variable to store the scroll position of this view.")]
		public FsmVector2 scrollPosition;

		[Tooltip("Always show the horizontal scrollbars.")]
		public FsmBool horizontalScrollbar;

		[Tooltip("Always show the vertical scrollbars.")]
		public FsmBool verticalScrollbar;

		[Tooltip("Define custom styles below. NOTE: You have to define all the styles if you check this option.")]
		public FsmBool useCustomStyle;

		[Tooltip("Named style in the active GUISkin for the horizontal scrollbars.")]
		public FsmString horizontalStyle;

		[Tooltip("Named style in the active GUISkin for the vertical scrollbars.")]
		public FsmString verticalStyle;

		[Tooltip("Named style in the active GUISkin for the background.")]
		public FsmString backgroundStyle;

		public override void Reset()
		{
			base.Reset();
			scrollPosition = null;
			horizontalScrollbar = null;
			verticalScrollbar = null;
			useCustomStyle = null;
			horizontalStyle = null;
			verticalStyle = null;
			backgroundStyle = null;
		}

		public override void OnGUI()
		{
			if (useCustomStyle.Value)
			{
				scrollPosition.Value = GUILayout.BeginScrollView(scrollPosition.Value, horizontalScrollbar.Value, verticalScrollbar.Value, horizontalStyle.Value, verticalStyle.Value, backgroundStyle.Value, LayoutOptions);
			}
			else
			{
				scrollPosition.Value = GUILayout.BeginScrollView(scrollPosition.Value, horizontalScrollbar.Value, verticalScrollbar.Value, LayoutOptions);
			}
		}
	}
}
