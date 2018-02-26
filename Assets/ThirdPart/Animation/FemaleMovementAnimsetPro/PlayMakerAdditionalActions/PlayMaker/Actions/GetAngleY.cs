/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) copyright Hutong Games, LLC 2010-2011. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Get the angle around the y axis between the gameObject and a target's Y axis.")]
	public class GetAngleY : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		
		public FsmGameObject target;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The shortest Angle. unsigned.")]
		public FsmFloat angle;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The signed Angle. This is also the shortest distance.")]
		public FsmFloat signedAngle;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The positive delta angle. This means the ClockWise travel to reach the target")]
		public FsmFloat resultPositiveAngle;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The negative delta angle. This means the Counter ClockWise travel to reach the target")]
		public FsmFloat resultNegativeAngle;
		
		
		[Tooltip("Repeat this action every frame.")]
		public bool everyFrame;
		

		public override void Reset()
		{
			gameObject = null;
			target = null;
			signedAngle = null;
			resultPositiveAngle = null;
			resultNegativeAngle = null;
		}

		public override void OnEnter()
		{
			DoGetSignedAngle();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetSignedAngle();
		}


		void DoGetSignedAngle()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (go == null)
			{
				return;
			}
			
			GameObject goTarget = target.Value;
			if (goTarget == null)
			{
				return;
			}
			
			angle.Value = Quaternion.Angle(go.transform.rotation, goTarget.transform.rotation);
			
			// get a "forward vector" for each rotation
			Vector3 forwardA = go.transform.rotation * Vector3.forward;
			Vector3 forwardB = goTarget.transform.rotation * Vector3.forward;
			
			// get a numeric angle for each vector, on the X-Z plane (relative to world forward)
			float angleA = Mathf.Atan2(forwardA.x, forwardA.z) * Mathf.Rad2Deg;
			float angleB = Mathf.Atan2(forwardB.x, forwardB.z) * Mathf.Rad2Deg;
			
			// get the signed difference in these angles
			float _signedAngle = Mathf.DeltaAngle( angleA, angleB );
			
			signedAngle.Value = _signedAngle;
			if (_signedAngle <0){
				resultNegativeAngle.Value = _signedAngle;
				resultPositiveAngle.Value = 360f +_signedAngle;
			}else{
				resultNegativeAngle.Value = -360f+_signedAngle;
				resultPositiveAngle.Value = _signedAngle;
			}
			
		}
	}
}
