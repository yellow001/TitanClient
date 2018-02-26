/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Assign path to NavMesh Agent. Uses FsmNavMeshPath component. \nNOTE: The Game Object must have a NavMeshAgent component attached.")]
	public class SetAgentPath : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(UnityEngine.AI.NavMeshAgent))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The Game Object holding the path. NOTE: The Game Object must have a FsmNavMeshPath component attached.")]
		[CheckForComponent(typeof(FsmNavMeshPath))]
		public FsmOwnerDefault path;
		
		[Tooltip("True if succesfully assigned.")]
		public FsmBool pathAssigned;
		
		[Tooltip("Trigger event if path assigned.")]
		public FsmEvent pathAssignedEvent;

		[Tooltip("Trigger event if path not assigned.")]
		public FsmEvent pathNotAssignedEvent;			

		private UnityEngine.AI.NavMeshAgent _agent;
		private FsmNavMeshPath _pathProxy;
		
		private void _getAgent()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) 
			{
				return;
			}
			
			_agent =  go.GetComponent<UnityEngine.AI.NavMeshAgent>();
		}	

		private void _getPathProxy()
		{
			GameObject go = path.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : path.GameObject.Value;
			if (go == null) 
			{
				return;
			}
			
			_pathProxy =  go.GetComponent<FsmNavMeshPath>();
		}	
		
		public override void Reset()
		{
			gameObject = null;
			path = null;

		}

		public override void OnEnter()
		{
			_getAgent();
			_getPathProxy();
			
			DoSetPath();

			Finish();		
		}
		
		void DoSetPath()
		{
			if (_pathProxy == null || _agent == null) 
			{
				return;
			}
			
			
			bool _ok = _agent.SetPath(_pathProxy.path);
			
			pathAssigned.Value = _ok;
			
			if (_ok)
			{
				if ( ! FsmEvent.IsNullOrEmpty(pathAssignedEvent) ){
					Fsm.Event(pathAssignedEvent);
				}
			}else
			{
				if (! FsmEvent.IsNullOrEmpty(pathNotAssignedEvent) ){
					Fsm.Event(pathNotAssignedEvent);
				}
			}
		}

	}
}
