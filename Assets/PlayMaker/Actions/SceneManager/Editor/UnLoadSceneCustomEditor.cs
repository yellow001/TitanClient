/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

#pragma warning disable 618

#if UNITY_5_3 || UNITY_5_3_OR_NEWER

using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(UnloadScene))]
public class UnloadSceneCustomEditor : CustomActionEditor
{
	UnloadScene _target ;

	public override bool OnGUI()
	{
		_target = (UnloadScene)target;

		EditField ("sceneReference");

		switch (_target.sceneReference) {
		case UnloadScene.SceneReferenceOptions.ActiveScene:
			break;
		case UnloadScene.SceneReferenceOptions.SceneAtIndex:
			EditField ("sceneAtIndex");
			break;
		case UnloadScene.SceneReferenceOptions.SceneAtBuildIndex:
			EditField ("sceneAtBuildIndex");
			break;
		case UnloadScene.SceneReferenceOptions.SceneByName:
			EditField ("sceneByName");
			break;
		case UnloadScene.SceneReferenceOptions.SceneByPath:
			EditField ("sceneByPath");
			break;
		case UnloadScene.SceneReferenceOptions.SceneByGameObject:
			EditField ("sceneByGameObject");
			break;
		}


		EditField("unloaded");
		EditField("unloadedEvent");
		EditField("failureEvent");

		return GUI.changed;
	}
}

#endif
