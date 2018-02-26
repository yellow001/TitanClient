/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets info on the last RaycastAll and store in array variables.")]
	public class GetRaycastAllInfo : FsmStateAction
	{

		[Tooltip("Store the GameObjects hit in an array variable.")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject)]
		public FsmArray storeHitObjects;
		
		[Tooltip("Get the world position of all ray hit point and store them in an array variable.")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.Vector3)]
		public FsmArray points;
		
		[Tooltip("Get the normal at all hit points and store them in an array variable.")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.Vector3)]
		public FsmArray normals;

		[Tooltip("Get the distance along the ray to all hit points and store tjem in an array variable.")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.Float)]
		public FsmArray distances;

        [Tooltip("Repeat every frame. Warning, this could be affecting performances")]
	    public bool everyFrame;

		public override void Reset()
		{
			storeHitObjects = null;
			points = null;
			normals = null;
			distances = null;
		    everyFrame = false;
		}

		void StoreRaycastAllInfo()
		{
			if (RaycastAll.RaycastAllHitInfo == null)
			{
				return;
			}

			storeHitObjects.Resize(RaycastAll.RaycastAllHitInfo.Length);
			points.Resize(RaycastAll.RaycastAllHitInfo.Length);
			normals.Resize(RaycastAll.RaycastAllHitInfo.Length);
			distances.Resize(RaycastAll.RaycastAllHitInfo.Length);

			for (int i = 0; i < RaycastAll.RaycastAllHitInfo.Length; i++)
			{
				storeHitObjects.Values[i] = RaycastAll.RaycastAllHitInfo[i].collider.gameObject;

				points.Values[i] =  RaycastAll.RaycastAllHitInfo[i].point;
				normals.Values[i] =  RaycastAll.RaycastAllHitInfo[i].normal;
				distances.Values[i] =  RaycastAll.RaycastAllHitInfo[i].distance;
			}
		}

		public override void OnEnter()
		{
			StoreRaycastAllInfo();
			
            if (!everyFrame)
            {
                Finish();
            }
		}

        public override void OnUpdate()
        {
			StoreRaycastAllInfo();
        }
	}
}
