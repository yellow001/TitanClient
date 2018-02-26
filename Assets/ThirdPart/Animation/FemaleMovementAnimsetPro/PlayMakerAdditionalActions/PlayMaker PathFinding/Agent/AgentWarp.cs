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
	[Tooltip("Warps agent to the provided position. Send events base on result: Returns true if successful, otherwise returns false \n" +
	         "NOTE: The Game Object must have a NavMeshAgent component attached.")]
	public class AgentWarp : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(UnityEngine.AI.NavMeshAgent))]
		public FsmOwnerDefault gameObject;

		[Tooltip("New position to warp the agent to.")]
		public FsmVector3 newPosition;

		[ActionSection("Result")]

		[Tooltip("True if successful, otherwise returns false")]
		[UIHint(UIHint.Variable)]
		public FsmBool success;

		[Tooltip("Trigger this event Warp to new position is successful")]
		public FsmEvent successEvent;
		
		[Tooltip("Trigger this event when Warp to new position failed")]
		public FsmEvent failureEvent;

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
			newPosition = null;
			success = null;
			successEvent= null;
			failureEvent = null;
		}
		
		public override void OnEnter()
		{
			_getAgent();
			
			DoWarp();
			
			Finish();		
		}
		
		void DoWarp()
		{
			if (_agent == null) 
			{
				return;
			}
			
			bool ok =_agent.Warp(newPosition.Value);
			success.Value = ok;
			if (ok)
			{
			 Fsm.Event(successEvent);
			}else{
				Fsm.Event(failureEvent);
			}

		}
		
	}
}
