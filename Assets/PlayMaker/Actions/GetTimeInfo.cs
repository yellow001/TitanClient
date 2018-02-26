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
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Gets various useful Time measurements.")]
	public class GetTimeInfo : FsmStateAction
	{
		public enum TimeInfo
		{
			DeltaTime,
			TimeScale,
			SmoothDeltaTime,
			TimeInCurrentState,
			TimeSinceStartup,
			TimeSinceLevelLoad,
			RealTimeSinceStartup,
			RealTimeInCurrentState
		}
		
		public TimeInfo getInfo;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeValue;
		
		public bool everyFrame;

		public override void Reset()
		{
			getInfo = TimeInfo.TimeSinceLevelLoad;
			storeValue = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetTimeInfo();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetTimeInfo();
		}
		
		void DoGetTimeInfo()
		{
			switch (getInfo) 
			{
			
			case TimeInfo.DeltaTime:
				storeValue.Value = Time.deltaTime;
				break;
				
			case TimeInfo.TimeScale:
				storeValue.Value = Time.timeScale;
				break;
				
			case TimeInfo.SmoothDeltaTime:
				storeValue.Value = Time.smoothDeltaTime;
				break;
				
			case TimeInfo.TimeInCurrentState:
				storeValue.Value = State.StateTime;
				break;
			
			case TimeInfo.TimeSinceStartup:
				storeValue.Value = Time.time;
				break;
				
			case TimeInfo.TimeSinceLevelLoad:
				storeValue.Value = Time.timeSinceLevelLoad;
				break;
				
			case TimeInfo.RealTimeSinceStartup:
				storeValue.Value = FsmTime.RealtimeSinceStartup;
				break;
			
			case TimeInfo.RealTimeInCurrentState:
				storeValue.Value = FsmTime.RealtimeSinceStartup - State.RealStartTime;
				break;
				
			default:
				storeValue.Value = 0f;
				break;
			}
		}
	}
}
