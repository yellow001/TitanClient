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
	[Tooltip("Gets the Collision Flags from a Character Controller on a Game Object. Collision flags give you a broad overview of where the character collided with any other object.")]
	public class GetControllerCollisionFlags : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(CharacterController))]
        [Tooltip("The GameObject with a Character Controller component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
        [Tooltip("True if the Character Controller capsule is on the ground")]
        public FsmBool isGrounded;

		[UIHint(UIHint.Variable)]
        [Tooltip("True if no collisions in last move.")]
        public FsmBool none;

		[UIHint(UIHint.Variable)]
        [Tooltip("True if the Character Controller capsule was hit on the sides.")]
		public FsmBool sides;

		[UIHint(UIHint.Variable)]
        [Tooltip("True if the Character Controller capsule was hit from above.")]
		public FsmBool above;

		[UIHint(UIHint.Variable)]
        [Tooltip("True if the Character Controller capsule was hit from below.")]
		public FsmBool below;
		
		private GameObject previousGo; // remember so we can get new controller only when it changes.
		private CharacterController controller;
		
		public override void Reset()
		{
			gameObject = null;
			isGrounded = null;
			none = null;
			sides = null;
			above = null;
			below = null;
		}

		public override void OnUpdate()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
			    return;
			}
		
			if (go != previousGo)
			{
				controller = go.GetComponent<CharacterController>();
				previousGo = go;
			}
			
			if (controller != null)
			{
				isGrounded.Value = controller.isGrounded;
				none.Value = (controller.collisionFlags & CollisionFlags.None) != 0;
				sides.Value = (controller.collisionFlags & CollisionFlags.Sides) != 0;
				above.Value = (controller.collisionFlags & CollisionFlags.Above) != 0;
				below.Value = (controller.collisionFlags & CollisionFlags.Below) != 0;
			}
			
		}
	}
}
