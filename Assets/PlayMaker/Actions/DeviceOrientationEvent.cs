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
	[Tooltip("Sends an Event based on the Orientation of the mobile device.")]
	public class DeviceOrientationEvent : FsmStateAction
	{
		[Tooltip("Note: If device is physically situated between discrete positions, as when (for example) rotated diagonally, system will report Unknown orientation.")]
		public DeviceOrientation orientation;

		[Tooltip("The event to send if the device orientation matches Orientation.")]
		public FsmEvent sendEvent;

		[Tooltip("Repeat every frame. Useful if you want to wait for the orientation to be true.")]
		public bool everyFrame;
		
		public override void Reset()
		{
			orientation = DeviceOrientation.Portrait;
			sendEvent = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoDetectDeviceOrientation();
			
			if (!everyFrame)
			{
			    Finish();
			}
		}
		

		public override void OnUpdate()
		{
			DoDetectDeviceOrientation();
		}
		
		void DoDetectDeviceOrientation()
		{
			if (Input.deviceOrientation == orientation)
			{
			    Fsm.Event(sendEvent);
			}
		}
		
	}
}
