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
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Sets the value of a Game Object Variable.")]
	public class SetGameObject : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmGameObject variable;
		public FsmGameObject gameObject;
		public bool everyFrame;

		public override void Reset()
		{
			variable = null;
			gameObject = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			variable.Value = gameObject.Value;
			
			if (!everyFrame)
			{
				Finish();		
			}
		}

		public override void OnUpdate()
		{
			variable.Value = gameObject.Value;
		}
	}
}
