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
	[Tooltip("Set the destination of a NavMesh Agent. \n" +
		"An Event can be triggered when the new path calculation is finished (pathPending flag). \n" +
		"NOTE: The Game Object must have a NavMeshAgentcomponent attached.")]
	public class SetAgentDestination : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(UnityEngine.AI.NavMeshAgent))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("Set the destination to navigate towards.")]
		public FsmVector3 destination;
		
		[Tooltip("Trigger event when path is computed (pathPending flag)")]
		public FsmEvent pathPendingDoneEvent;
		
		private UnityEngine.AI.NavMeshAgent _agent;
		
		private void _getAgent()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) 
			{
				return;
			}
			
			_agent =  go.GetComponent<UnityEngine.AI.NavMeshAgent>();
		}	
		
		public override void Reset()
		{
			gameObject = null;
			destination = null;
			pathPendingDoneEvent = null;
		}

		public override void OnEnter()
		{
			_getAgent();
			
			DoSetDestination();
			
			if (pathPendingDoneEvent == null)
			{
				Finish();		
			}
		}

		public override void OnUpdate()
		{
			if (_agent.pathPending == false){
				Fsm.Event(pathPendingDoneEvent);
				Finish();
			}
		}

		void DoSetDestination()
		{
			if (destination == null || _agent == null) 
			{
				return;
			}
				
			_agent.SetDestination(destination.Value);
		}
	

	}
}
