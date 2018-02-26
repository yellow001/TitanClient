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
	[Tooltip("Check the current State name on a specified layer, this is more than the layer name, it holds the current state as well.")]
	public class GetAnimatorCurrentStateInfoIsName : FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component and a PlayMakerAnimatorProxy component are required")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The layer's index")]
		public FsmInt layerIndex;
		
		[Tooltip("The name to check the layer against.")]
		public FsmString name;
		
		[ActionSection("Results")]

		[UIHint(UIHint.Variable)]
		[Tooltip("True if name matches")]
		public FsmBool isMatching;

		[Tooltip("Event send if name matches")]
		public FsmEvent nameMatchEvent;

		[Tooltip("Event send if name doesn't match")]
		public FsmEvent nameDoNotMatchEvent;

		private Animator _animator;
		
		public override void Reset()
		{
			base.Reset();

			gameObject = null;
			layerIndex = null;
			
			name = null;
			
			nameMatchEvent = null;
			nameDoNotMatchEvent = null;
			
			everyFrame = false;
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

			IsName();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
	
		public override void OnActionUpdate()
		{
			IsName();
		}
		
		void IsName()
		{		
			if (_animator!=null)
			{
				AnimatorStateInfo _info = _animator.GetCurrentAnimatorStateInfo(layerIndex.Value);

				if (!isMatching.IsNone)
				{
					isMatching.Value = _info.IsName(name.Value);
				}

				if (_info.IsName(name.Value))
				{
					Fsm.Event(nameMatchEvent);
				}else{
					Fsm.Event(nameDoNotMatchEvent);
				}
			}
		}
	}
}
