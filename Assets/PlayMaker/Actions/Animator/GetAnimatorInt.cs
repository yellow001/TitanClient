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
	[Tooltip("Gets the value of an int parameter")]
	public class GetAnimatorInt : FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;

        [RequiredField]
        [UIHint(UIHint.AnimatorInt)]
		[Tooltip("The animator parameter")]
		public FsmString parameter;
		
		[ActionSection("Results")]
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The int value of the animator parameter")]
		public FsmInt result;

		private Animator _animator;
		
		private int _paramID;
		
		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			parameter = null;
			result = null;
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

			// get hash from the param for efficiency:
			_paramID = Animator.StringToHash(parameter.Value);
			
			GetParameter();
			
			if (!everyFrame) 
			{
				Finish();
			}
		}	
		
		public override void OnActionUpdate() 
		{
			GetParameter();
		}
		
		void GetParameter()
		{		
			if (_animator!=null)
			{
				result.Value = _animator.GetInteger(_paramID);
			}
		}
	}
}
