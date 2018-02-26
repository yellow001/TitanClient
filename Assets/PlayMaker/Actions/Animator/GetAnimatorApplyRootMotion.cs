/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Gets the value of ApplyRootMotion of an avatar. If true, root is controlled by animations")]
	public class GetAnimatorApplyRootMotion : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[ActionSection("Results")]
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Is the rootMotionapplied. If true, root is controlled by animations")]
		public FsmBool rootMotionApplied;
		
		[Tooltip("Event send if the root motion is applied")]
		public FsmEvent rootMotionIsAppliedEvent;
		
		[Tooltip("Event send if the root motion is not applied")]
		public FsmEvent rootMotionIsNotAppliedEvent;
		
		private Animator _animator;

		
		public override void Reset()
		{
			gameObject = null;
			rootMotionApplied = null;
			rootMotionIsAppliedEvent = null;
			rootMotionIsNotAppliedEvent = null;
		}
		
		
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			// get the animator component
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (go==null)
			{
				Finish();
				return;
			}
			
			_animator = go.GetComponent<Animator>();
			
			if (_animator==null)
			{
				Finish();
				return;
			}
			
			GetApplyMotionRoot();
			
			Finish();
			
		}
	
		void GetApplyMotionRoot()
		{		
			if (_animator!=null)
			{
				bool _applyRootMotion = _animator.applyRootMotion;
			
				rootMotionApplied.Value = _applyRootMotion;
				if (_applyRootMotion)
				{
					Fsm.Event(rootMotionIsAppliedEvent);
				}else{
					Fsm.Event(rootMotionIsNotAppliedEvent);
				}
			}
		}
	}
}
