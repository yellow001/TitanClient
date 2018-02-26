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
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Gets the value of a curve at a given time and stores it in a Float Variable. NOTE: This can be used for more than just animation! It's a general way to transform an input number into an output number using a curve (e.g., linear input -> bell curve).")]
	public class SampleCurve : FsmStateAction
	{
		[RequiredField]
		public FsmAnimationCurve curve;
		[RequiredField]
		public FsmFloat sampleAt;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeValue;
		public bool everyFrame;
		
		public override void Reset()
		{
			curve = null;
			sampleAt = null;
			storeValue = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSampleCurve();
			
			if(!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSampleCurve();
		}
		
		void DoSampleCurve()
		{
			if (curve == null || curve.curve == null || storeValue == null)
				return;

			storeValue.Value = curve.curve.Evaluate(sampleAt.Value);
		}
	}
}
