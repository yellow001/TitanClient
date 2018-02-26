/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
    [ActionTarget(typeof(GameObject), "gameObject")]
	[Tooltip("Tests if a Game Object is visible.")]
	public class GameObjectIsVisible : ComponentAction<Renderer>
	{
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
        [Tooltip("The GameObject to test.")]
		public FsmOwnerDefault gameObject;
		
        [Tooltip("Event to send if the GameObject is visible.")]
		public FsmEvent trueEvent;
		
        [Tooltip("Event to send if the GameObject is NOT visible.")]
		public FsmEvent falseEvent;
		
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the result in a bool variable.")]
		public FsmBool storeResult;
		
		public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			trueEvent = null;
			falseEvent = null;
			storeResult = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoIsVisible();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoIsVisible();
		}

		void DoIsVisible()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);			
			if (UpdateCache(go))
			{
                var isVisible = renderer.isVisible;
                storeResult.Value = isVisible;
                Fsm.Event(isVisible ? trueEvent : falseEvent);
			}
		}
	}
}

