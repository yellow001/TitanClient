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
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Moves a Game Object towards a Target. Optionally sends an event when successful. The Target can be specified as a Game Object or a world Position. If you specify both, then the Position is used as a local offset from the Object's Position.")]
	public class MoveTowards : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to Move")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("A target GameObject to move towards. Or use a world Target Position below.")]
		public FsmGameObject targetObject;
		
		[Tooltip("A world position if no Target Object. Otherwise used as a local offset from the Target Object.")]
		public FsmVector3 targetPosition;
		
		[Tooltip("Ignore any height difference in the target.")]
		public FsmBool ignoreVertical;
		
		[HasFloatSlider(0, 20)]
		[Tooltip("The maximum movement speed. HINT: You can make this a variable to change it over time.")]
		public FsmFloat maxSpeed;
		
		[HasFloatSlider(0, 5)]
		[Tooltip("Distance at which the move is considered finished, and the Finish Event is sent.")]
		public FsmFloat finishDistance;
		
		[Tooltip("Event to send when the Finish Distance is reached.")]
		public FsmEvent finishEvent;

        private GameObject go;
        private GameObject goTarget;
	    private Vector3 targetPos;
        private Vector3 targetPosWithVertical;

		public override void Reset()
		{
			gameObject = null;
			targetObject = null;
			maxSpeed = 10f;
			finishDistance = 1f;
			finishEvent = null;
		}

		public override void OnUpdate()
		{
			DoMoveTowards();
		}

		void DoMoveTowards()
		{
            if (!UpdateTargetPos())
            {
                return;
            }
			
			go.transform.position = Vector3.MoveTowards(go.transform.position, targetPos, maxSpeed.Value * Time.deltaTime);
			
			var distance = (go.transform.position - targetPos).magnitude;
			if (distance < finishDistance.Value)
			{
				Fsm.Event(finishEvent);
				Finish();
			}
		}

        public bool UpdateTargetPos()
        {
            go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return false;
            }

            goTarget = targetObject.Value;
            if (goTarget == null && targetPosition.IsNone)
            {
                return false;
            }

            if (goTarget != null)
            {
                targetPos = !targetPosition.IsNone ?
                    goTarget.transform.TransformPoint(targetPosition.Value) :
                    goTarget.transform.position;
            }
            else
            {
                targetPos = targetPosition.Value;
            }

            targetPosWithVertical = targetPos;

            if (ignoreVertical.Value)
            {
                targetPos.y = go.transform.position.y;
            }

            return true;
        }

        public Vector3 GetTargetPos()
        {
            return targetPos;
        }

        public Vector3 GetTargetPosWithVertical()
        {
            return targetPosWithVertical;
        }
	}
}
