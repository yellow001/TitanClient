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


[CustomActionEditor(typeof(QuaternionLerp))]
public class QuaternionLerpCustomEditor : QuaternionCustomEditorBase
{

    public override bool OnGUI()
    {
		EditField("fromQuaternion");
		EditField("toQuaternion");
		EditField("amount");
		EditField("storeResult");
		
		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
    }


}
