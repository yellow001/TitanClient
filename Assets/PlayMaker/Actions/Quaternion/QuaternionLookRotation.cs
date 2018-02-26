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
	[ActionCategory(ActionCategory.Quaternion)]
	[Tooltip("Creates a rotation that looks along forward with the the head upwards along upwards.")]
	public class QuaternionLookRotation : QuaternionBaseAction
	{
		[RequiredField]
		[Tooltip("the rotation direction")]
		public FsmVector3 direction;
		
		[Tooltip("The up direction")]
		public FsmVector3 upVector;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the inverse of the rotation variable.")]
		public FsmQuaternion result;

		public override void Reset()
		{
			direction = null;
			upVector = new FsmVector3(){UseVariable=true};
			result = null;
			everyFrame = true;
			everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		public override void OnEnter()
		{
			DoQuatLookRotation();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (everyFrameOption == everyFrameOptions.Update )
			{
				DoQuatLookRotation();
			}
		}
		public override void OnLateUpdate()
		{
			if (everyFrameOption == everyFrameOptions.LateUpdate )
			{
				DoQuatLookRotation();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (everyFrameOption == everyFrameOptions.FixedUpdate )
			{
				DoQuatLookRotation();
			}
		}

		void DoQuatLookRotation()
		{
			if (!upVector.IsNone)
			{
				result.Value = Quaternion.LookRotation(direction.Value,upVector.Value);
			}
            else
            {
				result.Value = Quaternion.LookRotation(direction.Value);
			}
		}
	}
}

