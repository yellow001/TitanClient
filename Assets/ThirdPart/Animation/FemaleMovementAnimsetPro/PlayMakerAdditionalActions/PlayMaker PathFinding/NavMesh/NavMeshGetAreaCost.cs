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
	[Tooltip("Gets the cost for path finding over geometry of the area type.\n" +
	         "The value applies to all agents unless you the value has been customized per agent by calling NavMeshAgentSetAreaCost.")]
	public class NavMeshGetAreaCost : FsmStateAction
	{	
		[ActionSection("Set up")]
		
		[Tooltip("The Area index.")]
		public FsmInt area;

		[Tooltip("OR the layer name.")]
		public FsmString orAreaName;
		
		[ActionSection("Result")]
		
		[Tooltip("Store the Layer Cost")]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeResult;
		
		private UnityEngine.AI.NavMeshAgent _agent;
		
		public override void Reset()
		{
			area = null;
			orAreaName = new FsmString(){UseVariable=true};
			storeResult = null;
		}

		public override void OnEnter()
		{	
			DoGetAreaCost();

			Finish();		
		}
		
		void DoGetAreaCost()
		{
			int areaId = area.Value;
			if (orAreaName.Value!=""){
				
				areaId = UnityEngine.AI.NavMesh.GetAreaFromName(orAreaName.Value);
			}
			
			storeResult.Value =	UnityEngine.AI.NavMesh.GetAreaCost(areaId);
		}
		
		public override string ErrorCheck()
		{
			if (orAreaName.Value!="")
			{
				int areaId = UnityEngine.AI.NavMesh.GetAreaFromName(orAreaName.Value);
				if (areaId==-1){
					return "Layer Name '"+orAreaName.Value+"' doesn't exists";
				}else if(area.Value != 0){
					if (areaId == area.Value){
						return "Area reference redundancy. Use 'Area' OR 'Area Name', not both at the same time..";
					}else{
						return "Area conflict, area name '"+orAreaName.Value+"' will be used";
					}
					
				}
			}
			
			return "";
		}

	}
}
