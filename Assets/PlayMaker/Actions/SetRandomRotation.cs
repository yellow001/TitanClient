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
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Sets Random Rotation for a Game Object. Uncheck an axis to keep its current value.")]
	public class SetRandomRotation : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		[RequiredField]
		public FsmBool x;
		[RequiredField]
		public FsmBool y;
		[RequiredField]
		public FsmBool z;

		public override void Reset()
		{
			gameObject = null;
			x = true;
			y = true;
			z = true;
		}

		public override void OnEnter()
		{
			DoRandomRotation();
			
			Finish();		
		}

		void DoRandomRotation()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			
			Vector3 rotation = go.transform.localEulerAngles;

			float xAngle = rotation.x;
			float yAngle = rotation.y;
			float zAngle = rotation.z;
			
			if (x.Value) xAngle = Random.Range(0,360);
			if (y.Value) yAngle = Random.Range(0,360);
			if (z.Value) zAngle = Random.Range(0,360);
				
			go.transform.localEulerAngles = new Vector3(xAngle, yAngle, zAngle);
		}


	}
}
