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

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Network)]
	[Tooltip("Destroy the object across the network.\n\nThe object is destroyed locally and remotely.\n\n" +
		"Optionally remove any RPCs accociated with the object.")]
	public class NetworkDestroy : ComponentAction<NetworkView>
	{
		[RequiredField]
		[CheckForComponent(typeof(NetworkView))]
		[Tooltip("The Game Object to destroy.\nNOTE: The Game Object must have a NetworkView attached.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Remove all RPC calls associated with the Game Object.")]
		public FsmBool removeRPCs;		
		
		public override void Reset()
		{
			gameObject = null;
			removeRPCs = true;
		}

		public override void OnEnter()
		{
			DoDestroy();
			
			Finish();
		}

		void DoDestroy()
		{
			// get the target
			var targetGo = Fsm.GetOwnerDefaultTarget(gameObject);
			if (!UpdateCache(targetGo))
			{
				return;
			}

			if (removeRPCs.Value)
			{
				Network.RemoveRPCs(networkView.owner);
			}
			Network.DestroyPlayerObjects(networkView.owner);		
		}		
	}
}

#endif
