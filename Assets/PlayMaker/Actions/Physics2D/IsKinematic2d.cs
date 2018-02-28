/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Tests if a Game Object's Rigid Body 2D is Kinematic.")]
    public class IsKinematic2d : ComponentAction<Rigidbody2D>
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("the GameObject with a Rigidbody2D attached")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Event Sent if Kinematic")]
		public FsmEvent trueEvent;

		[Tooltip("Event sent if not Kinematic")]
		public FsmEvent falseEvent;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the Kinematic state")]
		public FsmBool store;

		[Tooltip("Repeat every frame")]
		public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			trueEvent = null;
			falseEvent = null;
			store = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoIsKinematic();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoIsKinematic();
		}
		
		void DoIsKinematic()
		{
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (!UpdateCache(go))
            {
                return;
            }
			
			var isKinematic = rigidbody2d.isKinematic;
			store.Value = isKinematic;
			
			Fsm.Event(isKinematic ? trueEvent : falseEvent);
		}
	}
}
