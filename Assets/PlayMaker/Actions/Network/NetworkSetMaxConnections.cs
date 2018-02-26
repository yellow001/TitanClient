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
	[Tooltip("Set the maximum amount of connections/players allowed.\n\n" +
		"This cannot be set higher than the connection count given in Launch Server.\n\n" +
		"Setting it to 0 means no new connections can be made but the existing ones stay connected.\n\n" +
		"Setting it to -1 means the maximum connections count is set to the same number of current open connections. " +
		"In that case, if a players drops then the slot is still open for him.")]
	public class NetworkSetMaximumConnections : FsmStateAction
	{
		[Tooltip("The maximum amount of connections/players allowed.")]
		public FsmInt maximumConnections;

		public override void Reset()
		{
			maximumConnections = 32;
		}

		public override void OnEnter()
		{
			if (maximumConnections.Value <-1)
			{
				LogWarning("Network Maximum connections can not be less than -1");
				maximumConnections.Value = -1;
			}

			Network.maxConnections  = maximumConnections.Value;
			
			Finish();
		}
		
		public override string ErrorCheck()
		{
			if (maximumConnections.Value <-1)
			{
				return "Network Maximum connections can not be less than -1";
			}
			
			return "";
		}

	}
}

#endif
