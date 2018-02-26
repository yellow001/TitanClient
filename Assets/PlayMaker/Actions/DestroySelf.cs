/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Destroys the Owner of the Fsm! Useful for spawned Prefabs that need to kill themselves, e.g., a projectile that explodes on impact.")]
	public class DestroySelf : FsmStateAction
	{
		[Tooltip("Detach children before destroying the Owner.")]
		public FsmBool detachChildren;

		public override void Reset()
		{
			detachChildren = false;
		}

		public override void OnEnter()
		{
			if (Owner != null)
			{
				if (detachChildren.Value)
				{
					Owner.transform.DetachChildren();
				}
				
				Object.Destroy(Owner);
			}
			
			Finish();
		}
	}
}
