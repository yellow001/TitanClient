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
	[Tooltip("Create an empty new scene with the given name additively. The path of the new scene will be empty")]
	public class AllowSceneActivation : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The name of the new scene. It cannot be empty or null, or same as the name of the existing scenes.")]
		public FsmInt aSynchOperationHashCode;

		[Tooltip("Allow the scene to be activated as soon as it's ready")]
		public FsmBool allowSceneActivation;
	
		[Tooltip("useful if activation will be set during update")]
		public bool everyframe;


		[ActionSection("Result")]

		[Tooltip("The loading's progress.")]
		[UIHint(UIHint.Variable)]
		public FsmFloat progress;

		[Tooltip("True when loading is done")]
		[UIHint(UIHint.Variable)]
		public FsmBool isDone;

		[Tooltip("Event sent when scene loading is done")]
		public FsmEvent doneEvent;

		[Tooltip("Event sent when action could not be performed. Check Log for more information")]
		public FsmEvent failureEvent;

		public override void Reset()
		{
			aSynchOperationHashCode = null;
			allowSceneActivation = null;
			everyframe = false;

			progress = null;
			isDone = null;
			doneEvent = null;
			failureEvent = null;
		}

		public override void OnEnter()
		{
			DoAllowSceneActivation ();
			if (!everyframe) {
				Finish();
			}

		}

		public override void OnUpdate()
		{
			DoAllowSceneActivation ();
		}


		void DoAllowSceneActivation()
		{
			if (aSynchOperationHashCode.IsNone ||
				allowSceneActivation.IsNone ||
				LoadSceneAsynch.aSyncOperationLUT==null ||
				!LoadSceneAsynch.aSyncOperationLUT.ContainsKey(aSynchOperationHashCode.Value)
			) {
				Fsm.Event(failureEvent);
				Finish();
				return;
			}

			if (!progress.IsNone)
				progress.Value = LoadSceneAsynch.aSyncOperationLUT [aSynchOperationHashCode.Value].progress;


			if (!isDone.IsNone) {
				isDone.Value = LoadSceneAsynch.aSyncOperationLUT [aSynchOperationHashCode.Value].isDone;
				if (LoadSceneAsynch.aSyncOperationLUT [aSynchOperationHashCode.Value].isDone) {
					LoadSceneAsynch.aSyncOperationLUT.Remove (aSynchOperationHashCode.Value);
					Fsm.Event (doneEvent);
					Finish ();
					return;
				}
			}

			LoadSceneAsynch.aSyncOperationLUT[aSynchOperationHashCode.Value].allowSceneActivation = allowSceneActivation.Value;
		}
	}
}

#endif
