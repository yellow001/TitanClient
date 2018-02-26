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
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Gets Location Info from a mobile device. NOTE: Use StartLocationService before trying to get location info.")]
	public class GetLocationInfo : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		public FsmVector3 vectorPosition;		
		[UIHint(UIHint.Variable)]
		public FsmFloat longitude;
		[UIHint(UIHint.Variable)]
		public FsmFloat latitude;
		[UIHint(UIHint.Variable)]
		public FsmFloat altitude;
		[UIHint(UIHint.Variable)]
		public FsmFloat horizontalAccuracy;
		[UIHint(UIHint.Variable)]
		public FsmFloat verticalAccuracy;
		// TODO: figure out useful way to expose timeStamp
		// maybe how old is the location...?
		//[UIHint(UIHint.Variable)]
		//[Tooltip("Timestamp (in seconds since the game started) when location was last updated.")]
		//public FsmFloat timeStamp;
		[Tooltip("Event to send if the location cannot be queried.")]
		public FsmEvent errorEvent;

		public override void Reset()
		{
			longitude = null;
			latitude = null;
			altitude = null;
			horizontalAccuracy = null;
			verticalAccuracy = null;
			//timeStamp = null;
			errorEvent = null;
		}

		public override void OnEnter()
		{
			DoGetLocationInfo();

			Finish();
		}

		void DoGetLocationInfo()
        {
#if UNITY_IPHONE || UNITY_IOS || UNITY_ANDROID || UNITY_BLACKBERRY || UNITY_WP8

			if (Input.location.status != LocationServiceStatus.Running)
			{
				Fsm.Event(errorEvent);
				return;
			}
			
			float x = Input.location.lastData.longitude;
			float y = Input.location.lastData.latitude;
			float z = Input.location.lastData.altitude;
			
			vectorPosition.Value = new Vector3(x,y,z);
			
			longitude.Value = x;
			latitude.Value = y;
			altitude.Value = z;

			horizontalAccuracy.Value = Input.location.lastData.horizontalAccuracy;
			verticalAccuracy.Value = Input.location.lastData.verticalAccuracy;
			
#endif
        }
	}
}
