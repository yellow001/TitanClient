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
	[Tooltip("Use a low pass filter to reduce the influence of sudden changes in a Vector3 Variable. Useful when working with Get Device Acceleration to isolate gravity.")]
	public class Vector3LowPassFilter : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Vector3 Variable to filter. Should generally come from some constantly updated input, e.g., acceleration.")]
		public FsmVector3 vector3Variable;
		[Tooltip("Determines how much influence new changes have. E.g., 0.1 keeps 10 percent of the unfiltered vector and 90 percent of the previously filtered value.")]
		public FsmFloat filteringFactor;		
		
		Vector3 filteredVector;
		
		public override void Reset()
		{
			vector3Variable = null;
			filteringFactor = 0.1f;
		}

		public override void OnEnter()
		{
			filteredVector = new Vector3(vector3Variable.Value.x, vector3Variable.Value.y, vector3Variable.Value.z);
		}

		public override void OnUpdate()
		{
			filteredVector.x = (vector3Variable.Value.x * filteringFactor.Value) + (filteredVector.x * (1.0f - filteringFactor.Value));
			filteredVector.y = (vector3Variable.Value.y * filteringFactor.Value) + (filteredVector.y * (1.0f - filteringFactor.Value));
			filteredVector.z = (vector3Variable.Value.z * filteringFactor.Value) + (filteredVector.z * (1.0f - filteringFactor.Value));

			vector3Variable.Value = new Vector3(filteredVector.x, filteredVector.y, filteredVector.z);
		}
	}
}

