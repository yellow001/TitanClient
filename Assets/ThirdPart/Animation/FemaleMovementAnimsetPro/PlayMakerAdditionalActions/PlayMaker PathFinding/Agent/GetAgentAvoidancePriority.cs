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
	[Tooltip("Gets the avoidance priority level.\n" +
	         "When the agent is performing avoidance, agents of lower priority are ignored.\n" +
	         "The valid range is from 0 to 99 where: Most important = 0. Least important = 99. Default = 50\n" +
	         "NOTE: The Game Object must have a NavMeshAgent.")]
	public class GetAgentAvoidancePriority : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(UnityEngine.AI.NavMeshAgent))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("the avoidance priority level. Most important = 0. Least important = 99. Default = 50")]
		[UIHint(UIHint.Variable)]
		public FsmFloat avoidancePriority;
	
		
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
			avoidancePriority = null;
		}
		
		public override void OnEnter()
		{
			_getAgent();
			
			DoGetAvoidancePriority();
				
		}

		void DoGetAvoidancePriority()
		{
			
			if (_agent == null) 
			{
				return;
			}
			
			if (avoidancePriority!=null){
				avoidancePriority.Value = _agent.avoidancePriority;
			}		
		}
		
	}
}
