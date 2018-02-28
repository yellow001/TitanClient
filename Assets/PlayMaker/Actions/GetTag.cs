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
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets a Game Object's Tag and stores it in a String Variable.")]
	public class GetTag : FsmStateAction
	{
		[RequiredField]
		public FsmGameObject gameObject;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString storeResult;
		public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetTag();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoGetTag();
		}
		
		void DoGetTag()
		{
			if (gameObject.Value == null) return;
			
			storeResult.Value = gameObject.Value.tag;
		}
		
	}
}