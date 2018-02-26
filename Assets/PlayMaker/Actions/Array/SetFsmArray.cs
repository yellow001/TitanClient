/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
	[Tooltip("Copy an Array Variable in another FSM.")]
    public class SetFsmArray : BaseFsmVariableAction
	{
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM.")]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object.")]
		public FsmString fsmName;
		
		[RequiredField]
        [UIHint(UIHint.FsmArray)]
		[Tooltip("The name of the FSM variable.")]
		public FsmString variableName;

		[RequiredField]
		[Tooltip("Set the content of the array variable.")]
		[UIHint(UIHint.Variable)]
		public FsmArray setValue;

		[Tooltip("If true, makes copies. if false, values share the same reference and editing one array item value will affect the source and vice versa. Warning, this only affect the current items of the source array. Adding or removing items doesn't affect other FsmArrays.")]
		public bool copyValues;

		
		public override void Reset()
		{
			gameObject = null;
			fsmName = "";
			variableName = null;
			setValue = null;
			copyValues = true;
		}
		
		public override void OnEnter()
		{
			DoSetFsmArrayCopy();
			
			Finish();
		}

	    private void DoSetFsmArrayCopy()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (!UpdateCache(go, fsmName.Value))
		    {
		        return;
		    }
         		
			var fsmArray = fsm.FsmVariables.GetFsmArray(variableName.Value);
            if (fsmArray != null)
			{

				if (fsmArray.ElementType != setValue.ElementType)
				{
					LogError("Can only copy arrays with the same elements type. Found <"+fsmArray.ElementType+"> and <"+setValue.ElementType+">");
					return;
				}

				fsmArray.Resize(0);

				if (copyValues)
				{
					fsmArray.Values = setValue.Values.Clone() as object[];

				}
                else
                {
					fsmArray.Values = setValue.Values;
				}


			}
			else
			{
                DoVariableNotFound(variableName.Value);
			}			
		}

	}
}
