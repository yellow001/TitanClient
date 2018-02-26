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
	[Tooltip("Gets the remaining distance on the current path of a NavMesh Agent. Can also send an event when arrived. \n" +
		"NOTE: The Game Object must have a NavMeshAgent component attached.")]
	public class GetAgentRemainingDistance : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(UnityEngine.AI.NavMeshAgent))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Store the remaining distance on the current path.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeResult;
		
		[Tooltip("When remaining distance is 0, sends event.")]
		public FsmEvent arrivedEvent;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
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
			storeResult = null;
			arrivedEvent = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			_getAgent();

			DoGetRemainingDistance();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetRemainingDistance();
		}

		
		void DoGetRemainingDistance()
		{
			if (storeResult == null || _agent == null) 
			{
				return;
			}
				
			storeResult.Value = _agent.remainingDistance;
	
			if (_agent.remainingDistance==0 && arrivedEvent!=null){
				Fsm.Event(arrivedEvent);
			}		
		}

	}
}
