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
	[Tooltip("Sample a position along the current path. \nYou can dispatch events If terminated before reaching position at maxDistance or not. \nYou can then store information about the location (navMeshHit). \nNOTE: The Game Object must have a NavMeshAgent component attached.")]
	public class AgentSamplePathPosition : FsmStateAction
	{
		
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(UnityEngine.AI.NavMeshAgent))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The mask specifying which NavMesh layers can be passed when tracing the path.")]
		public FsmInt passableMask;
		
		[Tooltip("Terminate scanning the path at this distance.")]
		public FsmFloat maxDistance;		
		
		[ActionSection("Result")]
		
		[Tooltip("True If terminated before reaching position at maxDistance.")]
		[UIHint(UIHint.Variable)]
		public FsmBool reachedBeforeMaxDistance;
		
		[Tooltip("Trigger event if sample reached before the maxDistance.")]
		public FsmEvent reachedBeforeMaxDistanceEvent;

		[Tooltip("Trigger event if sample reached after the maxDistance.")]
		public FsmEvent reachedAfterMaxDistanceEvent;
		
		
		[ActionSection("Hit information of the sample")]
		
		[Tooltip("Position of hit")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 position;
		
		[Tooltip("Normal at the point of hit")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 normal;
		
		[Tooltip("Distance to the point of hit")]
		[UIHint(UIHint.Variable)]
		public FsmFloat distance;

		[Tooltip("Mask specifying NavMeshLayers at point of hit.")]
		[UIHint(UIHint.Variable)]
		public FsmInt mask;

		[Tooltip("Flag when hit")]
		[UIHint(UIHint.Variable)]
		public FsmBool hit;
		
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
			
			passableMask = -1; // so that by default mask is "everything"
			
			reachedBeforeMaxDistance = null;
			reachedBeforeMaxDistanceEvent = null;
			reachedAfterMaxDistanceEvent = null;
			
			position = null;
			normal = null;
			distance = null;
			mask = null;
			hit = null;
			
		}

		public override void OnEnter()
		{
			_getAgent();
			
			DoSamplePath();

			Finish();		
		}
		
		void DoSamplePath()
		{
			if (_agent == null) 
			{
				return;
			}
			
			UnityEngine.AI.NavMeshHit _NavMeshHit;
			bool _reachedBeforeMaxDistance = _agent.SamplePathPosition(passableMask.Value,maxDistance.Value,out _NavMeshHit);
			reachedBeforeMaxDistance.Value = _reachedBeforeMaxDistance;
			
			position.Value = _NavMeshHit.position;
			normal.Value = _NavMeshHit.normal;
			distance.Value = _NavMeshHit.distance;
			mask.Value = _NavMeshHit.mask;
			hit.Value = _NavMeshHit.hit;
			
			if (_reachedBeforeMaxDistance)
			{
				if ( ! FsmEvent.IsNullOrEmpty(reachedBeforeMaxDistanceEvent) ){
					Fsm.Event(reachedBeforeMaxDistanceEvent);
				}
			}else
			{
				if (! FsmEvent.IsNullOrEmpty(reachedAfterMaxDistanceEvent) ){
					Fsm.Event(reachedAfterMaxDistanceEvent);
				}
			}
			
			
		}

	}
}
