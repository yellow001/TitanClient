/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
// TODO: implement FsmNavMeshPath properly.
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMesh)]
	[Tooltip("Calculate a path between two GameObjects and store the resulting path.")]
	public class NavMeshCalculatePathBetweenGameObjects : FsmStateAction
	{
		[ActionSection("Set up")]
		
		[RequiredField]
		[Tooltip("The mask specifying which NavMesh layers can be passed when calculating the path.")]
		public FsmInt passableMask;
		
		[RequiredField]
		[Tooltip("The initial position of the path requested.")]
		public FsmOwnerDefault sourceGameObject;

		[RequiredField]
		[Tooltip("The final position of the path requested.")]
		public FsmGameObject targetGameObject;
		
		[ActionSection("Result")]
		
		
		[RequiredField]
		[Tooltip("The Fsm NavMeshPath proxy component to hold the resulting path")]
		[UIHint(UIHint.Variable)]
		[CheckForComponent(typeof(FsmNavMeshPath))]
		public FsmOwnerDefault calculatedPath;
		
		
		[Tooltip("True If a resulting path is found.")]
		[UIHint(UIHint.Variable)]
		public FsmBool resultingPathFound;
		
		[Tooltip("Trigger event if resulting path found.")]
		public FsmEvent resultingPathFoundEvent;

		[Tooltip("Trigger event if no path could be found.")]
		public FsmEvent resultingPathNotFoundEvent;	
		
		
		
		private FsmNavMeshPath _NavMeshPathProxy;
		
		private void _getNavMeshPathProxy()
		{
			GameObject go = calculatedPath.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : calculatedPath.GameObject.Value;
			if (go == null) 
			{
				return;
			}
			
			_NavMeshPathProxy =  go.GetComponent<FsmNavMeshPath>();
		}	
		
		public override void Reset()
		{
			calculatedPath = null;
			
			passableMask = -1; // so that by default mask is "everything"
			sourceGameObject = null;
			targetGameObject = null;
			resultingPathFound = null;
			resultingPathFoundEvent = null;
			resultingPathNotFoundEvent = null;
		}

		public override void OnEnter()
		{	
			DoCalculatePath();

			Finish();		
		}
		
		void DoCalculatePath()
		{
			
			GameObject _sourceGameObject = Fsm.GetOwnerDefaultTarget(sourceGameObject);
			if (_sourceGameObject == null) 
			{
				return;
			}
			GameObject _targetGameObject = targetGameObject.Value;
			if (_targetGameObject == null)
			{
				return;
			}
			
			
			_getNavMeshPathProxy();
			if (_NavMeshPathProxy ==null)
			{
				return;
			}
			
			UnityEngine.AI.NavMeshPath _path = new UnityEngine.AI.NavMeshPath();
			
			bool _found = UnityEngine.AI.NavMesh.CalculatePath(_sourceGameObject.transform.position,_targetGameObject.transform.position,passableMask.Value,_path);
			
			_NavMeshPathProxy.path = _path;
			
			resultingPathFound.Value = _found;
			
			if (_found)
			{
				if ( ! FsmEvent.IsNullOrEmpty(resultingPathFoundEvent) ){
					Fsm.Event(resultingPathFoundEvent);
				}
			}else
			{
				if (! FsmEvent.IsNullOrEmpty(resultingPathNotFoundEvent) ){
					Fsm.Event(resultingPathNotFoundEvent);
				}
			}
			
			
		}

	}
}
