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
	[Tooltip("Send Events based on the status of the network interface peer type: Disconneced, Server, Client, Connecting.")]
	public class NetworkPeerTypeSwitch : FsmStateAction
	{
		[Tooltip("Event to send if no client connection running. Server not initialized.")]
		public FsmEvent isDisconnected;
		
		[Tooltip("Event to send if running as server.")]
		public FsmEvent isServer;

		[Tooltip("Event to send if running as client.")]
		public FsmEvent isClient;

		[Tooltip("Event to send attempting to connect to a server.")]
		public FsmEvent isConnecting;

		//[Tooltip("Only send the event when the peer type changes. NOTE: Event always sent the first time the state is entered.")]
		//public bool OnlyOnChange;

		[Tooltip("Repeat every frame. Useful if you're waiting for a particular network state.")]
		public bool everyFrame;

		public override void Reset()
		{
			isDisconnected = null;
			isServer = null;
			isClient = null;
			isConnecting = null;
			//OnlyOnChange = true;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoNetworkPeerTypeSwitch();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoNetworkPeerTypeSwitch();
		}

		void DoNetworkPeerTypeSwitch()
		{
			switch (Network.peerType)
			{
				case NetworkPeerType.Disconnected:
					
					Fsm.Event(isDisconnected);	
					break;

				case NetworkPeerType.Server:

					Fsm.Event(isServer);
					break;
				
				case NetworkPeerType.Client:
					
					Fsm.Event(isClient);
					break;
				
				case NetworkPeerType.Connecting:
					
					Fsm.Event(isConnecting);
					break;
			}
		}
	}
}

#endif
