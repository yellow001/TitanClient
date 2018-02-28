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
	[Tooltip("Specifies which NavMesh areas are passable. \n" +
		"Changing AreaMask will make the path stale (see isPathStale). \n" +
		"NOTE: The Game Object must have a NavMeshAgent component attached.")]
	public class SetAgentAreaMask : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(UnityEngine.AI.NavMeshAgent))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Pick the walkable NavMesh areas")]
		public FsmInt NavMeshAreaMask;
		
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
			NavMeshAreaMask =  UnityEngine.AI.NavMesh.AllAreas;  // so that by default mask is "everything"
		}

		public override void OnEnter()
		{
			_getAgent();
			
			DoSetAreaMask();

			Finish();		
		}
		
		void DoSetAreaMask()
		{
			if ( _agent == null) 
			{
				return;
			}
			
			_agent.areaMask = NavMeshAreaMask.Value;

		}
	}
}
