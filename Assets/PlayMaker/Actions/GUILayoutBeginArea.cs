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
	[ActionCategory(ActionCategory.GUILayout)]
	[Tooltip("Begin a GUILayout block of GUI controls in a fixed screen area. NOTE: Block must end with a corresponding GUILayoutEndArea.")]
	public class GUILayoutBeginArea : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		public FsmRect screenRect;
		public FsmFloat left;
		public FsmFloat top;
		public FsmFloat width;
		public FsmFloat height;
		public FsmBool normalized;
		public FsmString style;
		
		private Rect rect;
		
		public override void Reset()
		{
			screenRect = null;
			left = 0;
			top = 0;
			width = 1;
			height = 1;
			normalized = true;
			style = "";
		}

		public override void OnGUI()
		{
			rect = !screenRect.IsNone ? screenRect.Value : new Rect();
			
			if (!left.IsNone) rect.x = left.Value;
			if (!top.IsNone) rect.y = top.Value;
			if (!width.IsNone) rect.width = width.Value;
			if (!height.IsNone) rect.height = height.Value;
			
			if (normalized.Value)
			{
				rect.x *= Screen.width;
				rect.width *= Screen.width;
				rect.y *= Screen.height;
				rect.height *= Screen.height;
			}
			
			// if no GUIContent is given, then the signature is misunderstood as BeginAreay(Rect,String)
			GUILayout.BeginArea(rect, GUIContent.none, style.Value);
		}
	}
}
