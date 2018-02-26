/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_5_OR_NEWER
    using UnityEngine.AI;
#endif
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Synchronize a NavMesh Agent velocity and rotation with the animator process.")]
	public class NavMeshAgentAnimatorSynchronizer : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(NavMeshAgent))]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Agent target. An Animator component and a NavMeshAgent component are required")]
		public FsmOwnerDefault gameObject;

		private Animator _animator;
		private NavMeshAgent _agent;
		
		private Transform _trans;
		
		public override void Reset()
		{
			gameObject = null;
		}

		public override void OnPreprocess ()
		{
			Fsm.HandleAnimatorMove = true;
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
			_agent = go.GetComponent<NavMeshAgent>();

			_animator = go.GetComponent<Animator>();
			
			if (_animator==null)
			{
				Finish();
				return;
			}
			
			_trans = go.transform;
		}
	
		public override void DoAnimatorMove()
		{
			_agent.velocity = _animator.deltaPosition / Time.deltaTime;
			_trans.rotation = _animator.rootRotation;
		}	
		
	}
}
