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

[CustomActionEditor(typeof(MergeScenes))]
public class MergeScenesCustomEditor : CustomActionEditor
{
	MergeScenes _target ;

	public override bool OnGUI()
	{
		_target = (MergeScenes)target;


		EditField ("sourceReference");

		switch (_target.sourceReference) {
		case GetSceneActionBase.SceneAllReferenceOptions.ActiveScene:
			break;
		case GetSceneActionBase.SceneAllReferenceOptions.SceneAtIndex:
			EditField ("sourceAtIndex");
			break;
		case GetSceneActionBase.SceneAllReferenceOptions.SceneByName:
			EditField ("sourceByName");
			break;
		case GetSceneActionBase.SceneAllReferenceOptions.SceneByPath:
			EditField ("sourceByPath");
			break;
		case GetSceneActionBase.SceneAllReferenceOptions.SceneByGameObject:
			EditField ("sourceByGameObject");
			break;
		}

		EditField ("destinationReference");

		switch (_target.destinationReference) {
		case GetSceneActionBase.SceneAllReferenceOptions.ActiveScene:
			break;
		case GetSceneActionBase.SceneAllReferenceOptions.SceneAtIndex:
			EditField ("destinationAtIndex");
			break;
		case GetSceneActionBase.SceneAllReferenceOptions.SceneByName:
			EditField ("destinationByName");
			break;
		case GetSceneActionBase.SceneAllReferenceOptions.SceneByPath:
			EditField ("destinationByPath");
			break;
		case GetSceneActionBase.SceneAllReferenceOptions.SceneByGameObject:
			EditField ("destinationByGameObject");
			break;
		}

		EditField ("success");
		EditField ("successEvent");
		EditField ("failureEvent");

		return GUI.changed;
	}
}

#endif
