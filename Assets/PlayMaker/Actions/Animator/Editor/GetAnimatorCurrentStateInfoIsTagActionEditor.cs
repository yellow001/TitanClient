/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using UnityEditor;

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(GetAnimatorCurrentStateInfoIsTag))]
public class GetAnimatorCurrentStateInfoIsTagActionEditor : OnAnimatorUpdateActionEditorBase
{

	public override bool OnGUI()
	{
		EditField("gameObject");
		EditField("layerIndex");
		EditField("tag");

		EditField("tagMatch");
		EditField("tagMatchEvent");
		EditField("tagDoNotMatchEvent");

		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
	}

}