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
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Connect a joint to a game object.")]
	public class SetJointConnectedBody : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Joint))]
		[Tooltip("The joint to connect. Requires a Joint component.")]
		public FsmOwnerDefault joint;

		[CheckForComponent(typeof (Rigidbody))] 
		[Tooltip("The game object to connect to the Joint. Set to none to connect the Joint to the world.")] 
		public FsmGameObject rigidBody;

		public override void Reset()
		{
			joint = null;
			rigidBody = null;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(joint);
			if (go != null)
			{
				var jointComponent = go.GetComponent<Joint>();
				
				if (jointComponent != null)
				{
					jointComponent.connectedBody = rigidBody.Value == null ? null : rigidBody.Value.GetComponent<Rigidbody>();
				}
			}

			Finish();
		}
	}
}
