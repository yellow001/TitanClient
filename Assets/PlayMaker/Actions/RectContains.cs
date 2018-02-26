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
	[Tooltip("Tests if a point is inside a rectangle.")]
	public class RectContains : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Rectangle to test.")]
		public FsmRect rectangle;

		[Tooltip("Point to test.")]
		public FsmVector3 point;

		[Tooltip("Specify/override X value.")]
		public FsmFloat x;

		[Tooltip("Specify/override Y value.")]
		public FsmFloat y;

		//[ActionSection("")]

		[Tooltip("Event to send if the Point is inside the Rectangle.")]
		public FsmEvent trueEvent;

		[Tooltip("Event to send if the Point is outside the Rectangle.")]
		public FsmEvent falseEvent;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a variable.")]
		public FsmBool storeResult;

		//[ActionSection("")]

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			rectangle = new FsmRect { UseVariable = true };
			point = new FsmVector3 { UseVariable = true };
			x = new FsmFloat { UseVariable = true };
			y = new FsmFloat { UseVariable = true };
			storeResult = null;
			trueEvent = null;
			falseEvent = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoRectContains();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoRectContains();
		}

		void DoRectContains()
		{
			if (rectangle.IsNone)
			{
				return;
			}

			// get point to test from inputs

			var testPoint = point.Value;

			if (!x.IsNone)
			{
				testPoint.x = x.Value;
			}

			if (!y.IsNone)
			{
				testPoint.y = y.Value;
			}

			// do results

			var contained = rectangle.Value.Contains(testPoint);

			storeResult.Value = contained;

			Fsm.Event(contained ? trueEvent : falseEvent);
		}
	}
}
