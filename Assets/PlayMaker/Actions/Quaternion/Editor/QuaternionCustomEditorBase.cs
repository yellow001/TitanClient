/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;


public class QuaternionCustomEditorBase : CustomActionEditor {

	public override bool OnGUI()
	{
		return false;
	}
		
	public bool EditEveryFrameField()
	{
		QuaternionBaseAction _target = (QuaternionBaseAction)target;
		
		if (_target.everyFrame) 
		{
			GUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Every Frame");
			_target.everyFrame = GUILayout.Toggle(_target.everyFrame,"");
			_target.everyFrameOption = (QuaternionBaseAction.everyFrameOptions)EditorGUILayout.EnumPopup(_target.everyFrameOption);
			GUILayout.EndHorizontal();
		
		}else{
			EditField("everyFrame");
		}
		
		return GUI.changed;
	}
	
}
