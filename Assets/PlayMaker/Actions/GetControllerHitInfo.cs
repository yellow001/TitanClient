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
	[ActionCategory(ActionCategory.Character)]
	[Tooltip("Gets info on the last Character Controller collision and store in variables.")]
	public class GetControllerHitInfo : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the GameObject hit in the last collision.")]
		public FsmGameObject gameObjectHit;

		[UIHint(UIHint.Variable)]
        [Tooltip("Store the contact point of the last collision in world coordinates.")]
		public FsmVector3 contactPoint;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the normal of the last collision.")]
        public FsmVector3 contactNormal;

		[UIHint(UIHint.Variable)]
        [Tooltip("Store the direction of the last move before the collision.")]
		public FsmVector3 moveDirection;
		
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the distance of the last move before the collision.")]
		public FsmFloat moveLength;
		
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the physics material of the Game Object Hit. Useful for triggering different effects. Audio, particles...")]
		public FsmString physicsMaterialName;

		public override void Reset()
		{
			gameObjectHit = null;
			contactPoint = null;
			contactNormal = null;
			moveDirection = null;
			moveLength = null;
			physicsMaterialName = null;
		}

        public override void OnPreprocess()
        {
            Fsm.HandleControllerColliderHit = true;
        }

	    private void StoreTriggerInfo()
		{
			if (Fsm.ControllerCollider == null) return;
			
			gameObjectHit.Value = Fsm.ControllerCollider.gameObject;
			contactPoint.Value = Fsm.ControllerCollider.point;
			contactNormal.Value = Fsm.ControllerCollider.normal;
			moveDirection.Value = Fsm.ControllerCollider.moveDirection;
			moveLength.Value = Fsm.ControllerCollider.moveLength;
			physicsMaterialName.Value = Fsm.ControllerCollider.collider.material.name;
		}

		public override void OnEnter()
		{
			StoreTriggerInfo();
			
			Finish();
		}

		public override string ErrorCheck()
		{
			return ActionHelpers.CheckOwnerPhysicsSetup(Owner);
		}
	}
}
