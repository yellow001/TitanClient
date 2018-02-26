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
	[Tooltip("Adds a 2d force to a Game Object. Use Vector2 variable and/or Float variables for each axis.")]
    public class AddForce2d : ComponentAction<Rigidbody2D>
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject to apply the force to.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Option for applying the force")]
		public ForceMode2D forceMode;

		[UIHint(UIHint.Variable)]
		[Tooltip("Optionally apply the force at a position on the object. This will also add some torque. The position is often returned from MousePick or GetCollision2dInfo actions.")]
		public FsmVector2 atPosition;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("A Vector2 force to add. Optionally override any axis with the X, Y parameters.")]
		public FsmVector2 vector;
		
		[Tooltip("Force along the X axis. To leave unchanged, set to 'None'.")]
		public FsmFloat x;
		
		[Tooltip("Force along the Y axis. To leave unchanged, set to 'None'.")]
		public FsmFloat y;

		[Tooltip("A Vector3 force to add. z is ignored")]
		public FsmVector3 vector3;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;


		public override void Reset()
		{
			gameObject = null;
			atPosition = new FsmVector2 { UseVariable = true };
			forceMode = ForceMode2D.Force;
			vector = null;
			vector3 = new FsmVector3 {UseVariable = true};

			// default axis to variable dropdown with None selected.
			x = new FsmFloat { UseVariable = true };
			y = new FsmFloat { UseVariable = true };

			everyFrame = false;
		}


        public override void OnPreprocess()
        {
            Fsm.HandleFixedUpdate = true;
        }

		public override void OnEnter()
		{
			DoAddForce();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}

		public override void OnFixedUpdate()
		{
			DoAddForce();
		}
		
		void DoAddForce()
		{
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (!UpdateCache(go))
            {
                return;
            }

			var force = vector.IsNone ? new Vector2(x.Value, y.Value) : vector.Value;

			if (!vector3.IsNone)
			{
				force.x = vector3.Value.x;
				force.y = vector3.Value.y;
			}

			// override any axis
			
			if (!x.IsNone) force.x = x.Value;
			if (!y.IsNone) force.y = y.Value;
			
			// apply force	
		
			if (!atPosition.IsNone)
			{
				rigidbody2d.AddForceAtPosition(force, atPosition.Value,forceMode);
			}
			else
			{
				rigidbody2d.AddForce(force,forceMode);
			}

		}
		
		
	}
}
