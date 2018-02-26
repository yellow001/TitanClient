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
	[Tooltip("Sets the value of a float parameter")]
	public class SetAnimatorFloat : FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target.")]
		public FsmOwnerDefault gameObject;

        [RequiredField]
        [UIHint(UIHint.AnimatorFloat)]
		[Tooltip("The animator parameter")]
		public FsmString parameter;
		
		[Tooltip("The float value to assign to the animator parameter")]
		public FsmFloat Value;
		
		[Tooltip("Optional: The time allowed to parameter to reach the value. Requires everyFrame Checked on")]
		public FsmFloat dampTime;

		private Animator _animator;
		private int _paramID;
		
		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			parameter = null;
			dampTime = new FsmFloat() {UseVariable=true};
			Value = null;
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

			// get hash from the param for efficiency:
			_paramID = Animator.StringToHash(parameter.Value);
			
			SetParameter();
			
			if (!everyFrame) 
			{
				Finish();
			}
		}

		public override void OnActionUpdate ()
		{
			SetParameter();
		}
		
		void SetParameter()
		{
		    if (_animator == null) return;
		    
            if (dampTime.Value>0f)
		    {
		        _animator.SetFloat(_paramID,Value.Value,dampTime.Value,Time.deltaTime);
		    }
		    else
		    {
		        _animator.SetFloat(_paramID,Value.Value) ;
		    }
		}
	}
}
