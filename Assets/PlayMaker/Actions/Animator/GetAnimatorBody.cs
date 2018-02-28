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
	[Tooltip("Gets the avatar body mass center position and rotation. Optionally accepts a GameObject to get the body transform. \nThe position and rotation are local to the gameobject")]
	public class GetAnimatorBody: FsmStateActionAnimatorBase
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[ActionSection("Results")]
			
		[UIHint(UIHint.Variable)]
		[Tooltip("The avatar body mass center")]
		public FsmVector3 bodyPosition;

		[UIHint(UIHint.Variable)]
		[Tooltip("The avatar body mass center")]
		public FsmQuaternion bodyRotation;
		
		[Tooltip("If set, apply the body mass center position and rotation to this gameObject")]
		public FsmGameObject bodyGameObject;

		private Animator _animator;
		
		private Transform _transform;
		
		public override void Reset()
		{
			gameObject = null;
			bodyPosition= null;
			bodyRotation = null;
			bodyGameObject = null;
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

			GameObject _body = bodyGameObject.Value;
			if (_body!=null)
			{
				_transform = _body.transform;
			}
			
			DoGetBodyPosition();
			
			if (!everyFrame)
			{
				Finish();
			}
			
		}
	
		public override void OnActionUpdate()
		{
			DoGetBodyPosition();
		}	
		
		void DoGetBodyPosition()
		{		
			if (_animator==null)
			{
				return;
			}
			
			bodyPosition.Value = _animator.bodyPosition;
			bodyRotation.Value = _animator.bodyRotation;
			
			if (_transform!=null)
			{
				_transform.position = _animator.bodyPosition;
				_transform.rotation = _animator.bodyRotation;
			}
		}

	}
}