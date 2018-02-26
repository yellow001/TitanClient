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
	[Tooltip("Loads the scene by its name or index in Build Settings. ")]
	public class LoadScene : FsmStateAction
	{
		[Tooltip("The reference options of the Scene")]
		public GetSceneActionBase.SceneSimpleReferenceOptions sceneReference;

		[Tooltip("The name of the scene to load. The given sceneName can either be the last part of the path, without .unity extension or the full path still without the .unity extension")]
		public FsmString sceneByName;

		[Tooltip("The index of the scene to load.")]
		public FsmInt sceneAtIndex;

		[Tooltip("Allows you to specify whether or not to load the scene additively. See LoadSceneMode Unity doc for more information about the options.")]
		[ObjectType(typeof(LoadSceneMode))]
		public FsmEnum loadSceneMode;

		[ActionSection("Result")]

		[Tooltip("True if the scene was loaded")]
		public FsmBool  success;

		[Tooltip("Event sent if the scene was loaded")]
		public FsmEvent successEvent;

		[Tooltip("Event sent if a problem occured, check log for information")]
		public FsmEvent failureEvent;

		Scene _scene;
		bool _sceneFound;

		public override void Reset()
		{
			sceneReference = GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex;
			sceneByName = null;
			sceneAtIndex = null;
			loadSceneMode = null;

			success = null;
			successEvent = null;
			failureEvent = null;
		}

		public override void OnEnter()
		{

			bool _result = DoLoadScene ();

			if(!success.IsNone)	success.Value = _result;

			if (_result) {
				Fsm.Event (successEvent);
			} else {
				Fsm.Event (failureEvent);
			}

			Finish();
		}

		bool DoLoadScene()
		{
			if (sceneReference == GetSceneActionBase.SceneSimpleReferenceOptions.SceneAtIndex)
			{
				if (SceneManager.GetActiveScene ().buildIndex == sceneAtIndex.Value) {
					return false;
				} else {
					SceneManager.LoadScene(sceneAtIndex.Value, (LoadSceneMode)loadSceneMode.Value);
				}

			} else {
				if (SceneManager.GetActiveScene ().name == sceneByName.Value) {
					return false;
				} else {
					SceneManager.LoadScene (sceneByName.Value, (LoadSceneMode)loadSceneMode.Value);
				}
			}
			return true;
		}




	}
}

#endif
