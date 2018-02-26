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
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;


public class GetSceneActionBaseCustomEditor : CustomActionEditor {

	public override bool OnGUI()
	{
		return false;
	}

	GetSceneActionBase _action;

	public bool EditSceneReferenceField()
	{
		_action = (GetSceneActionBase)target;
	
		EditField ("sceneReference");

		switch (_action.sceneReference) {
		case GetSceneActionBase.SceneAllReferenceOptions.ActiveScene:
			break;
		case GetSceneActionBase.SceneAllReferenceOptions.SceneAtIndex:
			EditField ("sceneAtIndex");
			break;
		case GetSceneActionBase.SceneAllReferenceOptions.SceneByName:
			EditField ("sceneByName");
			break;
		case GetSceneActionBase.SceneAllReferenceOptions.SceneByPath:
			EditField ("sceneByPath");
			break;
		case GetSceneActionBase.SceneAllReferenceOptions.SceneByGameObject:
			EditField ("sceneByGameObject");
			break;
		}
			
		return GUI.changed;
	}

	public void EditSceneReferenceResultFields()
	{
		EditField ("sceneFound");
		EditField ("sceneFoundEvent");
		EditField ("sceneNotFoundEvent");
	}


}

#endif
