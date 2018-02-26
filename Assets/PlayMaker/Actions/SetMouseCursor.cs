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
	[Tooltip("Controls the appearance of Mouse Cursor.")]
	public class SetMouseCursor : FsmStateAction
	{
		public FsmTexture cursorTexture;
		public FsmBool hideCursor;
		public FsmBool lockCursor;
		
		public override void Reset()
		{
			cursorTexture = null;
			hideCursor = false;
			lockCursor = false;
		}
		
		public override void OnEnter()
		{
			PlayMakerGUI.LockCursor = lockCursor.Value;
			PlayMakerGUI.HideCursor = hideCursor.Value;
			PlayMakerGUI.MouseCursor = cursorTexture.Value;
			
			Finish();
		}
		
/*		
		public override void OnUpdate()
		{
			// not sure if there is a performance impact to setting these ever frame,
			// so only do it if it's changed.
			
			if (Screen.lockCursor != lockCursor.Value)
				Screen.lockCursor = lockCursor.Value;
			
			if (Screen.showCursor == hideCursor.Value)
				Screen.showCursor = !hideCursor.Value;
		}
		
		public override void OnGUI()
		{
			// draw custom cursor
			
			if (cursorTexture != null)
			{
				var mousePos = Input.mousePosition;
				var pos =  new Rect(mousePos.x - cursorTexture.width * 0.5f, 
					Screen.height - mousePos.y - cursorTexture.height * 0.5f, 
					cursorTexture.width, cursorTexture.height);
				
				GUI.DrawTexture(pos, cursorTexture);
			}
		}*/
	}
}
