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
	[Tooltip("Get the last Loaded Scene Event data when event was sent from the action 'SendSceneLoadedEvent")]
	public class GetSceneLoadedEventData : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("The scene loaded mode")]
		[ObjectType(typeof(LoadSceneMode))]
		public FsmEnum loadedMode;

		[UIHint(UIHint.Variable)]
		[Tooltip("The scene name")]
		public FsmString name;

		[Tooltip("The scene path")]
		[UIHint(UIHint.Variable)]
		public FsmString path;

		[Tooltip("true if the scene is valid.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isValid;

		[Tooltip("The scene Build Index")]
		[UIHint(UIHint.Variable)]
		public FsmInt buildIndex;

		[Tooltip("true if the scene is loaded.")]
		[UIHint(UIHint.Variable)]
		public FsmBool isLoaded;

		[UIHint(UIHint.Variable)]
		[Tooltip("true if the scene is modified.")]
		public FsmBool isDirty;

		[Tooltip("The scene RootCount")]
		[UIHint(UIHint.Variable)]
		public FsmInt rootCount;

		[Tooltip("The scene Root GameObjects")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject)]
		public FsmArray rootGameObjects;

		Scene _scene;
	
		public override void Reset()
		{
			loadedMode = null;
			name = null;
			path = null;
			isValid = null;
			buildIndex = null;
			isLoaded = null;
			rootCount = null;
			rootGameObjects = null;
			isDirty = null;
		}

		public override void OnEnter()
		{
			DoGetSceneProperties();

			Finish();
		}
			
		void DoGetSceneProperties()
		{

			_scene = SendSceneLoadedEvent.lastLoadedScene;

			if (!name.IsNone) {
				loadedMode.Value = SendSceneLoadedEvent.lastLoadedMode;
			}

			if (!name.IsNone) {
				name.Value = _scene.name;
			}

			if (!buildIndex.IsNone) {
				buildIndex.Value = _scene.buildIndex;
			}

			if (!path.IsNone) {
				path.Value = _scene.path;
			}

			if (!isValid.IsNone) {
				isValid.Value = _scene.IsValid();
			}

			if (!isDirty.IsNone) {
				isDirty.Value = _scene.isDirty;
			}

			if (!isLoaded.IsNone) {
				isLoaded.Value = _scene.isLoaded;
			}

			if (!rootCount.IsNone) {
				rootCount.Value = _scene.rootCount;
			}

			if (!rootGameObjects.IsNone) {
				if (_scene.IsValid ()) {
					rootGameObjects.Values = _scene.GetRootGameObjects ();
				} else {
					rootGameObjects.Resize (0);
				}
			}
		}
	}
}

#endif
