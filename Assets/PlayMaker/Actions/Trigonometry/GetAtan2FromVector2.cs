/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Trigonometry)]
	[Tooltip("Get the Arc Tangent 2 as in atan2(y,x) from a vector 2. You can get the result in degrees, simply check on the RadToDeg conversion")]
	public class GetAtan2FromVector2 : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The vector2 of the tan")]
		public FsmVector2 vector2;
		

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting angle. Note:If you want degrees, simply check RadToDeg")]
		public FsmFloat angle;
		
		[Tooltip("Check on if you want the angle expressed in degrees.")]
		public FsmBool RadToDeg;
		
		[Tooltip("Repeat every frame.")]		
		public bool everyFrame;

		public override void Reset()
		{
			vector2 = null;
			
			RadToDeg = true;
			everyFrame = false;
			angle = null;
		}

		public override void OnEnter()
		{
			DoATan();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoATan();
		}
		
		void DoATan()
		{
			float _angle = Mathf.Atan2(vector2.Value.y,vector2.Value.x);
			
			
			if (RadToDeg.Value)
			{
				_angle	 = _angle*Mathf.Rad2Deg;
			}
			angle.Value = _angle;
			
		}
	}
}
