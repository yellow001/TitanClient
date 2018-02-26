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
	[Tooltip("Get a scene isLoaded flag.")]
	public class GetSceneIsLoaded : GetSceneActionBase
	{
		[ActionSection("Result")]

		[Tooltip("true if the scene is loaded.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isLoaded;

		[Tooltip("Event sent if the scene is loaded.")]
		public FsmEvent isLoadedEvent;

		[Tooltip("Event sent if the scene is not loaded.")]
		public FsmEvent isNotLoadedEvent;

		[Tooltip("Repeat every Frame")]
		public bool everyFrame;
	
		public override void Reset()
		{
			base.Reset ();

			isLoaded = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			base.OnEnter ();
			DoGetSceneIsLoaded();

			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoGetSceneIsLoaded();
		}

		void DoGetSceneIsLoaded()
		{
			if (!_sceneFound) {
				return;
			}
			
			if (!isLoaded.IsNone) {
				isLoaded.Value = _scene.isLoaded;
			}

			Fsm.Event(sceneFoundEvent);
		}
	}
}

#endif
