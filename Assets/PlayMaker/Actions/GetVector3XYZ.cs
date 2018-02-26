/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Get the XYZ channels of a Vector3 Variable and store them in Float Variables.")]
	public class GetVector3XYZ : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;
		[UIHint(UIHint.Variable)]
		public FsmFloat storeX;		
		[UIHint(UIHint.Variable)]
		public FsmFloat storeY;		
		[UIHint(UIHint.Variable)]
		public FsmFloat storeZ;	
		public bool everyFrame;
		
		public override void Reset()
		{
			vector3Variable = null;
			storeX = null;
			storeY = null;
			storeZ = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetVector3XYZ();
			
			if(!everyFrame)
				Finish();
		}
		
		public override void OnUpdate ()
		{
			DoGetVector3XYZ();
		}
		
		void DoGetVector3XYZ()
		{
			if (vector3Variable == null) return;
			
			if (storeX != null)
				storeX.Value = vector3Variable.Value.x;

			if (storeY != null)
				storeY.Value = vector3Variable.Value.y;

			if (storeZ != null)
				storeZ.Value = vector3Variable.Value.z;
		}
	}
}
