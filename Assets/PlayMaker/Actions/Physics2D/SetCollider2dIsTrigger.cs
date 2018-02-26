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
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Set the isTrigger option of a Collider2D. Optionally set all collider2D found on the gameobject Target.")]
	public class SetCollider2dIsTrigger : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Collider2D))]
		[Tooltip("The GameObject with the Collider2D attached")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The flag value")]
		public FsmBool isTrigger;
		
		[Tooltip("Set all Colliders on the GameObject target")]
		public bool setAllColliders;

		public override void Reset()
		{
			gameObject = null;
			isTrigger = false;
			setAllColliders = false;
		}
		
		public override void OnEnter()
		{
			DoSetIsTrigger();

			Finish();
		}
		
		void DoSetIsTrigger()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;

			
			if (setAllColliders)
			{
				// Find all of the colliders on the gameobject and set them all to be triggers.
				Collider2D[] cols = go.GetComponents<Collider2D> ();
				foreach (Collider2D c in cols) {
						c.isTrigger = isTrigger.Value;
				}
			}else{
				if (go.GetComponent<Collider2D>() != null)go.GetComponent<Collider2D>().isTrigger  = isTrigger.Value;
			}
		}
	}
}

