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

using UnityEngine;
using UnityEditor;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(SetActiveScene))]
public class SetActiveSceneCustomEditor : CustomActionEditor
{
	SetActiveScene _target ;

	public override bool OnGUI()
	{
		_target = (SetActiveScene)target;

		EditField ("sceneReference");

		switch (_target.sceneReference) {
		case SetActiveScene.SceneReferenceOptions.SceneAtBuildIndex:
			EditField ("sceneAtBuildIndex");
			break;
		case SetActiveScene.SceneReferenceOptions.SceneAtIndex:
			EditField ("sceneAtIndex");
			break;
		case SetActiveScene.SceneReferenceOptions.SceneByName:
			EditField ("sceneByName");
			break;
		case SetActiveScene.SceneReferenceOptions.SceneByPath:
			EditField ("sceneByPath");
			break;
		case SetActiveScene.SceneReferenceOptions.SceneByGameObject:
			EditField ("sceneAtGameObject");
			break;
		default:
			throw new System.ArgumentOutOfRangeException ();
		}

		EditField("success");
		EditField("successEvent");
		EditField("sceneNotActivatedEvent");

		EditField("sceneFound");
		EditField("sceneNotFoundEvent");

		return GUI.changed;
	}
}

#endif
