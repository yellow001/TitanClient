/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Adds a XY values to Vector2 Variable.")]
	public class Vector2AddXY : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The vector2 target")]
		public FsmVector2 vector2Variable;
		
		[Tooltip("The x component to add")]
		public FsmFloat addX;
		[Tooltip("The y component to add")]
		public FsmFloat addY;
		
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
		
		[Tooltip("Add the value on a per second bases.")]
		public bool perSecond;

		public override void Reset()
		{
			vector2Variable = null;
			addX = 0;
			addY = 0;
			everyFrame = false;
			perSecond = false;
		}

		public override void OnEnter()
		{
			DoVector2AddXYZ();
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			DoVector2AddXYZ();
		}
		
		void DoVector2AddXYZ()
		{
			var vector = new Vector2(addX.Value, addY.Value);
			
			if(perSecond)
			{
				vector2Variable.Value += vector * Time.deltaTime;
			}
			else
			{
				vector2Variable.Value += vector;
			}
				
		}
	}
}

