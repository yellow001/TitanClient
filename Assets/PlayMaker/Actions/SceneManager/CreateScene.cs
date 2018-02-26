/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if !UNITY_5_3_0 && !UNITY_5_3_1 && (  UNITY_5_3 || UNITY_5_3_OR_NEWER )

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Create an empty new scene with the given name additively. The path of the new scene will be empty")]
	public class CreateScene : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name of the new scene. It cannot be empty or null, or same as the name of the existing scenes.")]
		public FsmString sceneName;
	
		public override void Reset()
		{
			sceneName = null;
		}

		public override void OnEnter()
		{
			SceneManager.CreateScene(sceneName.Value);

			Finish();
		}
	}
}

#endif
