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
	[Tooltip("Adds a 2d torque (rotational force) to a Game Object.")]
    public class AddTorque2d : ComponentAction<Rigidbody2D>
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject to add torque to.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Option for applying the force")]
		public ForceMode2D forceMode;

		[Tooltip("Torque")]
		public FsmFloat torque;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;


        public override void OnPreprocess()
        {
            Fsm.HandleFixedUpdate = true;
        }

		public override void Reset()
		{
			gameObject = null;
		    torque = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoAddTorque();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnFixedUpdate()
		{
			DoAddTorque();
		}
		
		void DoAddTorque()
		{
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (!UpdateCache(go))
            {
                return;
            }

			rigidbody2d.AddTorque(torque.Value,forceMode);
		}
		
		
	}
}
