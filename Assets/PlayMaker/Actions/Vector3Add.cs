/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Adds a value to Vector3 Variable.")]
	public class Vector3Add : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;
		[RequiredField]
		public FsmVector3 addVector;
		public bool everyFrame;
		public bool perSecond;

		public override void Reset()
		{
			vector3Variable = null;
			addVector = new FsmVector3 { UseVariable = true };
			everyFrame = false;
			perSecond = false;
		}

		public override void OnEnter()
		{
			DoVector3Add();
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			DoVector3Add();
		}
		
		void DoVector3Add()
		{
			if(perSecond)
				vector3Variable.Value = vector3Variable.Value + addVector.Value * Time.deltaTime;
			else
				vector3Variable.Value = vector3Variable.Value + addVector.Value;
				
		}
	}
}

