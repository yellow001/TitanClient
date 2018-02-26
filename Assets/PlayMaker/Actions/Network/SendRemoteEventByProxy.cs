/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

// Unity 5.1 introduced a new networking library. 
// Unless we define PLAYMAKER_LEGACY_NETWORK old network actions are disabled
#if !(UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || PLAYMAKER_LEGACY_NETWORK)
#define UNITY_NEW_NETWORK
#endif

// Some platforms do not support networking (at least the old network library)
#if (UNITY_FLASH || UNITY_NACL || UNITY_METRO || UNITY_WP8 || UNITY_WIIU || UNITY_PSM || UNITY_WEBGL || UNITY_PS3 || UNITY_PS4 || UNITY_XBOXONE)
#define PLATFORM_NOT_SUPPORTED
#endif

#if !(PLATFORM_NOT_SUPPORTED || UNITY_NEW_NETWORK || PLAYMAKER_NO_NETWORK)

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Network)]
    [Tooltip("Send an Fsm Event on a remote machine. Uses Unity RPC functions. Use this instead of SendRemoteEvent if you have multiple PlayMakerFSM components on the GameObject that you want to recieve the event.")]
	public class SendRemoteEventByProxy : ComponentAction<NetworkView>
	{
		[RequiredField]
        [CheckForComponent(typeof(NetworkView), typeof(PlayMakerRPCProxy))]
		[Tooltip("The game object that sends the event.")]
		public FsmOwnerDefault gameObject;

        [RequiredField]
        [Tooltip("The event you want to send.")]
        public FsmEvent remoteEvent;
		
		[Tooltip("Optional string data. Use 'Get Event Info' action to retrieve it.")]
		public FsmString stringData;

		[Tooltip("Option for who will receive an RPC.")]
		public RPCMode mode;

		public override void Reset()
		{
			gameObject = null;
			remoteEvent = null;
			mode = RPCMode.All;
			stringData = null;
			mode = RPCMode.All;
		}

		public override void OnEnter()
		{
			DoRPC();
			
			Finish();
		}

		void DoRPC()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (!UpdateCache(go))
			{
				return;
			}

            if (!stringData.IsNone && stringData.Value != "")
            {
                networkView.RPC("ForwardEvent", mode, remoteEvent.Name, stringData.Value);
            }
            else
            {
                networkView.RPC("ForwardEvent", mode, remoteEvent.Name);
            }
			
		}
	}
}

#endif
