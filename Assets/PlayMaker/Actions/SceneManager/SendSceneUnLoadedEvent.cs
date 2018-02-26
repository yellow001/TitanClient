/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_4_OR_NEWER

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Scene)]
	[Tooltip("Send an event when a scene was unloaded.")]
	public class SendSceneUnloadedEvent : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The event to send when scene was unloaded")]
		public FsmEvent sceneUnloaded;
	
		public static Scene lastUnLoadedScene;

		public override void Reset()
		{
			sceneUnloaded = null;
		}

		public override void OnEnter()
		{
			SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;

			Finish();
		}

		void SceneManager_sceneUnloaded (Scene scene)
		{
			UnityEngine.Debug.Log(scene.name);

			lastUnLoadedScene = scene;

			Fsm.Event (sceneUnloaded);

			Finish ();
		}

		public override void OnExit()
		{
			SceneManager.sceneUnloaded -= SceneManager_sceneUnloaded;
		}
	}
}

#endif
