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
	[Tooltip("Check if two quaternions are equals or not. Takes in account inversed representations of quaternions")]
	public class QuaternionCompare : QuaternionBaseAction
	{
		
		[RequiredField]
		[Tooltip("First Quaternion")]
		public FsmQuaternion Quaternion1;
		
		[RequiredField]
		[Tooltip("Second Quaternion")]
		public FsmQuaternion Quaternion2;

		[Tooltip("true if Quaternions are equal")]
		public FsmBool equal;

		[Tooltip("Event sent if Quaternions are equal")]
		public FsmEvent equalEvent;
	
		[Tooltip("Event sent if Quaternions are not equal")]
		public FsmEvent notEqualEvent;

		
		public override void Reset()
		{
			Quaternion1 = new FsmQuaternion { UseVariable = true };
			Quaternion2 = new FsmQuaternion { UseVariable = true };
			equal = null;
			equalEvent = null;
			notEqualEvent = null;
			everyFrameOption = everyFrameOptions.Update;
		}
		
		public override void OnEnter()
		{
			DoQuatCompare();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			if (everyFrameOption == everyFrameOptions.Update )
			{
				DoQuatCompare();
			}
		}
		public override void OnLateUpdate()
		{
			if (everyFrameOption == everyFrameOptions.LateUpdate )
			{
				DoQuatCompare();
			}
		}
		public override void OnFixedUpdate()
		{
			if (everyFrameOption == everyFrameOptions.FixedUpdate )
			{
				DoQuatCompare();
			}
		}
		
		void DoQuatCompare()
		{
		
			bool _equal = Mathf.Abs( Quaternion.Dot(Quaternion1.Value,Quaternion2.Value)) > 1-Quaternion.kEpsilon;

			equal.Value = _equal;

			if (_equal)
			{
				Fsm.Event(equalEvent);
			}else{
				Fsm.Event(notEqualEvent);
			}

		}
	}
}

