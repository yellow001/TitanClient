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
	[Tooltip("Sets the XYZ channels of a Vector3 Variable. To leave any channel unchanged, set variable to 'None'.")]
	public class SetVector3XYZ : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Value;
		public FsmFloat x;
		public FsmFloat y;
		public FsmFloat z;
		public bool everyFrame;

		public override void Reset()
		{
			vector3Variable = null;
			vector3Value = null;
			x = new FsmFloat{ UseVariable = true};
			y = new FsmFloat{ UseVariable = true};
			z = new FsmFloat{ UseVariable = true};
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetVector3XYZ();
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			DoSetVector3XYZ();
		}

		void DoSetVector3XYZ()
		{
			if (vector3Variable == null) return;
			
			Vector3 newVector3 = vector3Variable.Value;
			
			if (!vector3Value.IsNone) newVector3 = vector3Value.Value;
			if (!x.IsNone) newVector3.x = x.Value;
			if (!y.IsNone) newVector3.y = y.Value;
			if (!z.IsNone) newVector3.z = z.Value;
			
			vector3Variable.Value = newVector3;
		}
	}
}
