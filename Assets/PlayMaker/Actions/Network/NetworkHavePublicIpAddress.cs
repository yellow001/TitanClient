/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

#if PLAYMAKER_LEGACY_NETWORK &&  !(UNITY_FLASH || UNITY_NACL || UNITY_METRO || UNITY_WP8 || UNITY_WIIU || UNITY_PSM || UNITY_WEBGL || UNITY_PS3 || UNITY_PS4 || UNITY_XBOXONE)

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Network)]
	[Tooltip("Check if this machine has a public IP address.")]
	public class NetworkHavePublicIpAddress : FsmStateAction
	{	
		[UIHint(UIHint.Variable)]
		[Tooltip("True if this machine has a public IP address")]
		public FsmBool havePublicIpAddress;

		[Tooltip("Event to send if this machine has a public IP address")]
		public FsmEvent publicIpAddressFoundEvent;

		[Tooltip("Event to send if this machine has no public IP address")]
		public FsmEvent publicIpAddressNotFoundEvent;

		public override void Reset()
		{
			havePublicIpAddress = null;
			publicIpAddressFoundEvent = null;
			publicIpAddressNotFoundEvent =null;			
		}

		public override void OnEnter()
		{
			
			bool _publicIpAddress = Network.HavePublicAddress();
			
			havePublicIpAddress.Value = _publicIpAddress;

			if (_publicIpAddress && publicIpAddressFoundEvent != null)
			{
				Fsm.Event(publicIpAddressFoundEvent);
			}
			else if (!_publicIpAddress && publicIpAddressNotFoundEvent != null)
			{
				Fsm.Event(publicIpAddressNotFoundEvent);
			}

			Finish();
		}
	}
}

#endif
