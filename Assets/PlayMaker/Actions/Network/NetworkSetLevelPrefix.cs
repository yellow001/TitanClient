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
	[Tooltip("Set the level prefix which will then be prefixed to all network ViewID numbers.\n\n" +
		"This prevents old network updates from straying into a new level from the previous level.\n\n" +
		"This can be set to any number and then incremented with each new level load. " +
		"This doesn't add overhead to network traffic but just diminishes the pool of network ViewID numbers a little bit.")]
	public class NetworkSetLevelPrefix : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The level prefix which will then be prefixed to all network ViewID numbers.")]
		public FsmInt levelPrefix;
		
		public override void Reset()
		{
			levelPrefix = null;
		}

		public override void OnEnter()
		{
			if (levelPrefix.IsNone)
			{
				LogError("Network LevelPrefix not set");
				return;
			}

			Network.SetLevelPrefix(levelPrefix.Value);
			
			Finish();
		}
	}
}

#endif
