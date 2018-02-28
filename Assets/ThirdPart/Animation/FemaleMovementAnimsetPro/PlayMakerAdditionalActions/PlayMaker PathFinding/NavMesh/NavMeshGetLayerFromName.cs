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
	[Tooltip("Gets the area index for a named area.")]
	public class NavMeshGetLayerFromName : FsmStateAction
	{	
		[Tooltip("The area Name")]
		public FsmString areaName;
		
		[ActionSection("Result")]
		
		[Tooltip("Store the area Index for this area Name")]
		[UIHint(UIHint.Variable)]
		public FsmInt areaIndex;

		
		public override void Reset()
		{
			areaName = null;
			areaIndex = null;
		}
		
		public override void OnEnter()
		{	
			DoGetAreaFromName();
			
			Finish();		
		}
		
		void DoGetAreaFromName()
		{
			areaIndex.Value = UnityEngine.AI.NavMesh.GetAreaFromName(areaName.Value);
		}
		

	}
}