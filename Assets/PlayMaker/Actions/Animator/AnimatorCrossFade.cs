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
	[Tooltip("Create a dynamic transition between the current state and the destination state.Both state as to be on the same layer. note: You cannot change the current state on a synchronized layer, you need to change it on the referenced layer.")]
	public class AnimatorCrossFade : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The name of the state that will be played.")]
		public FsmString stateName;

		[Tooltip("The duration of the transition. Value is in source state normalized time.")]
		public FsmFloat transitionDuration;

		[Tooltip("Layer index containing the destination state. Leave to none to ignore")]
		public FsmInt layer;
		
		[Tooltip("Start time of the current destination state. Value is in source state normalized time, should be between 0 and 1.")]
		public FsmFloat normalizedTime;


		private Animator _animator;
		
		private int _paramID;
		
		public override void Reset()
		{
			gameObject = null;
			stateName = null;
			transitionDuration = 1f;
			layer = new FsmInt(){UseVariable=true};
			normalizedTime = new FsmFloat(){UseVariable=true};
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
			
			if (_animator!=null)
			{
				int _layer = layer.IsNone?-1:layer.Value;
				
				float _normalizedTime = normalizedTime.IsNone?Mathf.NegativeInfinity:normalizedTime.Value;
				
				_animator.CrossFade(stateName.Value,transitionDuration.Value,_layer,_normalizedTime);
			}
			
			Finish();
			
		}
		
		
	}
}
