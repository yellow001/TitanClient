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
	[Tooltip("Returns true if the current rig is humanoid, false if it is generic. Can also sends events")]
	public class GetAnimatorIsHuman : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[ActionSection("Results")]
		
		[UIHint(UIHint.Variable)]
		[Tooltip("True if the current rig is humanoid, False if it is generic")]
		public FsmBool isHuman;
		
		[Tooltip("Event send if rig is humanoid")]
		public FsmEvent isHumanEvent;
		
		[Tooltip("Event send if rig is generic")]
		public FsmEvent isGenericEvent;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			isHuman = null;
			isHumanEvent = null;
			isGenericEvent = null;
		}
		
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
			
			DoCheckIsHuman();
			
			Finish();
			
		}
	
		void DoCheckIsHuman()
		{		
			if (_animator==null)
			{
				return;
			}
			
			bool _isHuman = _animator.isHuman;

			if (! isHuman.IsNone)
			{
				isHuman.Value = _isHuman;
			}		

			if (_isHuman)
			{
				Fsm.Event(isHumanEvent);
			}else{
				Fsm.Event(isGenericEvent);
			}
		}
		
	}
}
