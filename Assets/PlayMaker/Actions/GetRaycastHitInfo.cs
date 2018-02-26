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
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets info on the last Raycast and store in variables.")]
	public class GetRaycastHitInfo : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the GameObject hit by the last Raycast and store it in a variable.")]
		public FsmGameObject gameObjectHit;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the world position of the ray hit point and store it in a variable.")]
		[Title("Hit Point")]
		public FsmVector3 point;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the normal at the hit point and store it in a variable.")]
		public FsmVector3 normal;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the distance along the ray to the hit point and store it in a variable.")]
		public FsmFloat distance;

        [Tooltip("Repeat every frame.")]
	    public bool everyFrame;

		public override void Reset()
		{
			gameObjectHit = null;
			point = null;
			normal = null;
			distance = null;
		    everyFrame = false;
		}

		void StoreRaycastInfo()
		{
			if (Fsm.RaycastHitInfo.collider != null)
			{
				gameObjectHit.Value = Fsm.RaycastHitInfo.collider.gameObject;
				point.Value = Fsm.RaycastHitInfo.point;
				normal.Value = Fsm.RaycastHitInfo.normal;
				distance.Value = Fsm.RaycastHitInfo.distance;
			}
		}

		public override void OnEnter()
		{
			StoreRaycastInfo();
			
            if (!everyFrame)
            {
                Finish();
            }
		}

        public override void OnUpdate()
        {
            StoreRaycastInfo();
        }
	}
}
