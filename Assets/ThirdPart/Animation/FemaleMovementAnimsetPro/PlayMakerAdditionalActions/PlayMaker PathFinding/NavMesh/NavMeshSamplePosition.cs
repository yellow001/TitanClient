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
	[ActionCategory(ActionCategory.NavMesh)]
	[Tooltip("Sample the NavMesh closest to the point specified. \nYou can dispatch events If terminated before reaching the target position or not. \nYou can then store information about the location (navMeshHit).")]
	public class NavMeshSamplePosition : FsmStateAction
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The origin of the sample query.")]
		public FsmVector3 sourcePosition;
		
		[RequiredField]
		[Tooltip("The mask specifying which NavMesh layer is allowed when finding the nearest point.")]
		public FsmInt allowedMask;

		[Tooltip("Sample within this distance from sourcePosition.")]
		public FsmFloat maxDistance;	
		
		
		[ActionSection("Result")]
		
		[Tooltip("True if a nearest point is found.")]
		public FsmBool nearestPointFound;
		
		[Tooltip("Trigger event if a nearest point is found.")]
		public FsmEvent nearestPointFoundEvent;

		[Tooltip("Trigger event if a nearest point is not found.")]
		public FsmEvent nearestPointNotFoundEvent;
		
		
		[ActionSection("Hit information of the sample")]
		
		[Tooltip("Position of hit")]
		public FsmVector3 position;
		
		[Tooltip("Normal at the point of hit")]
		public FsmVector3 normal;
		
		[Tooltip("Distance to the point of hit")]
		public FsmFloat distance;

		[Tooltip("Mask specifying NavMeshLayers at point of hit.")]
		public FsmInt mask;

		[Tooltip("Flag when hit")]
		public FsmBool hit;
			
		
		public override void Reset()
		{
			sourcePosition = new FsmVector3 { UseVariable = true };
			
			allowedMask = new FsmInt { Value = -1}; // so that by default mask is "everything"
			
			maxDistance = new FsmFloat();
			maxDistance.Value = 10;
			
			nearestPointFound  = new FsmBool { UseVariable = true };
			nearestPointFoundEvent = null;
			nearestPointNotFoundEvent = null;
			
			position = new FsmVector3 { UseVariable = true};
			normal = new FsmVector3 { UseVariable = true};
			distance = new FsmFloat { UseVariable = true};
			mask = new FsmInt { UseVariable = true};
			hit = new FsmBool { UseVariable = true};
			
		}

		public override void OnEnter()
		{
			DoSamplePosition();

			Finish();		
		}
		
		void DoSamplePosition()
		{
			UnityEngine.AI.NavMeshHit _NavMeshHit;
			bool _nearestPointFound = UnityEngine.AI.NavMesh.SamplePosition(sourcePosition.Value,out _NavMeshHit,maxDistance.Value,allowedMask.Value);
		 	nearestPointFound.Value = _nearestPointFound;
			
			position.Value = _NavMeshHit.position;
			normal.Value = _NavMeshHit.normal;
			distance.Value = _NavMeshHit.distance;
			mask.Value = _NavMeshHit.mask;
			hit.Value = _NavMeshHit.hit;
			
			if (_nearestPointFound)
			{
				if ( ! FsmEvent.IsNullOrEmpty(nearestPointFoundEvent) ){
					Fsm.Event(nearestPointFoundEvent);
				}
			}else
			{
				if (! FsmEvent.IsNullOrEmpty(nearestPointNotFoundEvent) ){
					Fsm.Event(nearestPointNotFoundEvent);
				}
			}
			
			
		}	

	}
}
