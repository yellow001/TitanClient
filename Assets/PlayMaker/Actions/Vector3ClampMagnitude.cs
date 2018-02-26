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
	[Tooltip("Clamps the Magnitude of Vector3 Variable.")]
	public class Vector3ClampMagnitude : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;
		[RequiredField]
		public FsmFloat maxLength;
		public bool everyFrame;

		public override void Reset()
		{
			vector3Variable = null;
			maxLength = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoVector3ClampMagnitude();
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			DoVector3ClampMagnitude();
		}
		
		void DoVector3ClampMagnitude()
		{
			vector3Variable.Value = Vector3.ClampMagnitude(vector3Variable.Value, maxLength.Value);
		}
	}
}

