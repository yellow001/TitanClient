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
	[ActionCategory(ActionCategory.Rect)]
	[Tooltip("Sets the individual fields of a Rect Variable. To leave any field unchanged, set variable to 'None'.")]
	public class SetRectFields : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmRect rectVariable;

		public FsmFloat x;
		public FsmFloat y;
		public FsmFloat width;
		public FsmFloat height;
		
		public bool everyFrame;

		public override void Reset()
		{
			rectVariable = null;
			x = new FsmFloat {UseVariable = true};
			y = new FsmFloat { UseVariable = true };
			width = new FsmFloat { UseVariable = true };
			height = new FsmFloat { UseVariable = true };
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetRectFields();

			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetRectFields();
		}

		void DoSetRectFields()
		{
			if (rectVariable.IsNone)
			{
				return;
			}

			var newRect = rectVariable.Value;

			if (!x.IsNone)
			{
				newRect.x = x.Value;
			}

			if (!y.IsNone)
			{
				newRect.y = y.Value;
			}

			if (!width.IsNone)
			{
				newRect.width = width.Value;
			}

			if (!height.IsNone)
			{
				newRect.height = height.Value;
			}

			rectVariable.Value = newRect;
		}
	}
}
