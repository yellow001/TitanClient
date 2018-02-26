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
	#pragma warning disable 618
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Is the rigidbody2D constrained from rotating?" +
	"Note: Prefer SetRigidBody2dConstraints when working in Unity 5")]
    public class IsFixedAngle2d : ComponentAction<Rigidbody2D>
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with the Rigidbody2D attached")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Event sent if the Rigidbody2D does have fixed angle")]
		public FsmEvent trueEvent;

		[Tooltip("Event sent if the Rigidbody2D doesn't have fixed angle")]
		public FsmEvent falseEvent;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the fixedAngle flag")]
		public FsmBool store;

		[Tooltip("Repeat every frame.")]
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
			DoIsFixedAngle();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoIsFixedAngle();
		}
		
		void DoIsFixedAngle()
		{
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (!UpdateCache(go))
            {
                return;
            }
			
			bool  isfixedAngle = false;
			
			#if UNITY_5_3_5 || UNITY_5_4_OR_NEWER
					isfixedAngle = (rigidbody2d.constraints & RigidbodyConstraints2D.FreezeRotation) != 0;
			#else
					isfixedAngle = rigidbody2d.fixedAngle;
			#endif
			
			store.Value = isfixedAngle;
			
			Fsm.Event(isfixedAngle ? trueEvent : falseEvent);
		}
	}
}

