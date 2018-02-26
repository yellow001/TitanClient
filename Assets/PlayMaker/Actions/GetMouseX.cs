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
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Gets the X Position of the mouse and stores it in a Float Variable.")]
	public class GetMouseX : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeResult;
		public bool normalize;
		
		public override void Reset()
		{
			storeResult = null;
			normalize = true;
		}

		public override void OnEnter()
		{
			DoGetMouseX();
		}

		public override void OnUpdate()
		{
			DoGetMouseX();
		}
		
		void DoGetMouseX()
		{
			if (storeResult != null)
			{
				float xpos = Input.mousePosition.x;
				
				if (normalize)
					xpos /= Screen.width;
			
				storeResult.Value = xpos;
			}
		}
	}
}

