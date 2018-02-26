/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Interpolates between 2 Vector3 values over a specified Time.")]
	public class Vector3Interpolate : FsmStateAction
	{
		public InterpolationType mode;
		[RequiredField]
		public FsmVector3 fromVector;
		[RequiredField]
		public FsmVector3 toVector;
		[RequiredField]
		public FsmFloat time;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeResult;
		public FsmEvent finishEvent;
		[Tooltip("Ignore TimeScale")]
		public bool realTime;

		private float startTime;
		private float currentTime;
		
		public override void Reset()
		{
			mode = InterpolationType.Linear;
			fromVector = new FsmVector3 { UseVariable = true };
			toVector = new FsmVector3 { UseVariable = true };
			time = 1.0f;
			storeResult = null;
			finishEvent = null;
			realTime = false;
		}

		public override void OnEnter()
		{
			startTime = FsmTime.RealtimeSinceStartup;
			currentTime = 0f;
			
			if (storeResult == null)
				Finish();
			else
				storeResult.Value = fromVector.Value;
		}
		
		public override void OnUpdate()
		{
			// update time
			
			if (realTime)
			{
				currentTime = FsmTime.RealtimeSinceStartup - startTime;
			}
			else
			{
				currentTime += Time.deltaTime;
			}
			
			float weight = currentTime/time.Value;
			
			switch (mode) {
			
			case InterpolationType.Linear:
				break;
				
			case InterpolationType.EaseInOut:
				weight = Mathf.SmoothStep(0, 1, weight);				
				break;
			}

			storeResult.Value = Vector3.Lerp(fromVector.Value, toVector.Value, weight);
			
			if (weight >= 1)
			{
				if (finishEvent != null)
					Fsm.Event(finishEvent);

				Finish();
			}
				
		}
	}
}

