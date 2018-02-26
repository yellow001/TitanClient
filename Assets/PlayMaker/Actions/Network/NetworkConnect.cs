/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

#if PLAYMAKER_LEGACY_NETWORK &&  !(UNITY_FLASH || UNITY_NACL || UNITY_METRO || UNITY_WP8 || UNITY_WIIU || UNITY_PSM || UNITY_WEBGL || UNITY_PS3 || UNITY_PS4 || UNITY_XBOXONE)

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Network)]
	[Tooltip("Connect to a server.")]
	public class NetworkConnect : FsmStateAction
	{
		[RequiredField]
		[Tooltip("IP address of the host. Either a dotted IP address or a domain name.")]
		public FsmString remoteIP;

		[RequiredField]
		[Tooltip("The port on the remote machine to connect to.")]
		public FsmInt remotePort;
		
		[Tooltip("Optional password for the server.")]
		public FsmString password;

		[ActionSection("Errors")]

		[Tooltip("Event to send in case of an error connecting to the server.")]
		public FsmEvent errorEvent;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the error string in a variable.")]
		public FsmString errorString;

		public override void Reset()
		{
			remoteIP = "127.0.0.1";
			remotePort = 25001;
			password = "";
			errorEvent = null;
			errorString = null;
		}

		public override void OnEnter()
		{
			var error = Network.Connect(remoteIP.Value, remotePort.Value, password.Value);

			if (error != NetworkConnectionError.NoError)
			{
				errorString.Value = error.ToString();
				LogError(errorString.Value);
				Fsm.Event(errorEvent);
			}

			Finish();
		}

	}
}

#endif
