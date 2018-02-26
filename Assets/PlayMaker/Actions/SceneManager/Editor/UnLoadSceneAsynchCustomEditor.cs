/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#if UNITY_5_5_OR_NEWER

using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(UnloadSceneAsynch))]
public class UnLoadSceneAsynchCustomEditor : CustomActionEditor
{
	UnloadSceneAsynch _target ;

	public override bool OnGUI()
	{
		_target = (UnloadSceneAsynch)target;

		EditField ("sceneReference");

		switch (_target.sceneReference) {
		case UnloadSceneAsynch.SceneReferenceOptions.ActiveScene:
			break;
		case UnloadSceneAsynch.SceneReferenceOptions.SceneAtIndex:
			EditField ("sceneAtIndex");
			break;
		case UnloadSceneAsynch.SceneReferenceOptions.SceneAtBuildIndex:
			EditField ("sceneAtBuildIndex");
			break;
		case UnloadSceneAsynch.SceneReferenceOptions.SceneByName:
			EditField ("sceneByName");
			break;
		case UnloadSceneAsynch.SceneReferenceOptions.SceneByPath:
			EditField ("sceneByPath");
			break;
		case UnloadSceneAsynch.SceneReferenceOptions.SceneByGameObject:
			EditField ("sceneByGameObject");
			break;
		}


		EditField("operationPriority");



		EditField("progress");
		EditField("isDone");

		EditField("doneEvent");
		EditField("sceneNotFoundEvent");

		return GUI.changed;
	}
}

#endif
