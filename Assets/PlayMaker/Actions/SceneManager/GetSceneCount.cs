/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Get the total number of currently loaded scenes.")]
	public class GetSceneCount : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The number of currently loaded scenes.")]
		public FsmInt sceneCount;

		[Tooltip("Repeat every Frame")]
		public bool everyFrame;

		public override void Reset()
		{
			sceneCount = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetSceneCount ();

			if (!everyFrame) {
				Finish ();
			}
		}

		public override void OnUpdate()
		{
			DoGetSceneCount ();
		}

		void DoGetSceneCount()
		{
			sceneCount.Value =	SceneManager.sceneCount;
		}
	}
}

#endif
