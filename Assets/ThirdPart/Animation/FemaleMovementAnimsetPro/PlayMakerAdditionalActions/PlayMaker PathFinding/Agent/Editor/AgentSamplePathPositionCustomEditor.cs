/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

using HutongGames.PlayMaker;
using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace HutongGames.PlayMakerEditor
{
	
	[CustomActionEditor(typeof(AgentSamplePathPosition))]
	public class AgentSamplePathPositionCustomEditor : CustomActionEditor
	{
		
		private PlayMakerNavMeshAreaMaskField _maskField;
		
		
		public override bool OnGUI()
		{
			
			AgentSamplePathPosition _target = (AgentSamplePathPosition)target;
			
			bool edited = false;
			
			EditField("gameObject");

			edited = EditMaskField(_target);

			EditField("maxDistance");
			
			EditField("reachedBeforeMaxDistance");
			EditField("reachedBeforeMaxDistanceEvent");
			EditField("reachedAfterMaxDistanceEvent");
			
			EditField("position");
			EditField("normal");
			EditField("distance");
			EditField("mask");
			EditField("hit");
			
			
			return GUI.changed || edited;
		}
		
		bool EditMaskField(AgentSamplePathPosition _target)
		{
			bool edited = false;
			
			if (_target.passableMask ==null)
			{
				_target.passableMask =  new FsmInt();
				_target.passableMask.Value = -1;
			}
			
			if (_target.passableMask.UseVariable)
			{
				EditField("passableMask");
				
			}else{
				
				GUILayout.BeginHorizontal();
				
				LayerMask _mask = _target.passableMask.Value;
				
				if (_maskField==null)
				{
					_maskField = new PlayMakerNavMeshAreaMaskField();
				}
				LayerMask _newMask = _maskField.AreaMaskField("Passable Mask",_mask,true);
				
				
				if (_newMask!=_mask)
				{
					edited = true;
					_target.passableMask.Value = _newMask.value;
				}
				
				if (PlayMakerEditor.FsmEditorGUILayout.MiniButtonPadded(PlayMakerEditor.FsmEditorContent.VariableButton))
				{
					_target.passableMask.UseVariable = true;
				}
				GUILayout.EndHorizontal();
			}
			
			return edited;
		}
	}
}
