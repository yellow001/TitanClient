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
	[Tooltip("Get a scene isDirty flag. true if the scene is modified. ")]
	public class GetSceneIsDirty : GetSceneActionBase
	{
		[ActionSection("Result")]

		[UIHint(UIHint.Variable)]
		[Tooltip("true if the scene is modified.")]
		public FsmBool isDirty;

		[Tooltip("Event sent if the scene is modified.")]
		public FsmEvent isDirtyEvent;

		[Tooltip("Event sent if the scene is unmodified.")]
		public FsmEvent isNotDirtyEvent;
	
		[Tooltip("Repeat every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			base.Reset ();

			isDirty = null;

			everyFrame = false;
		}

		public override void OnEnter()
		{
			base.OnEnter ();

			DoGetSceneIsDirty();

			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoGetSceneIsDirty();
		}


		void DoGetSceneIsDirty()
		{
			if (!_sceneFound) {
				return;
			}
			
			if (!isDirty.IsNone) {
				isDirty.Value = _scene.isDirty;
			}

			Fsm.Event(sceneFoundEvent);
		}
	}
}

#endif
