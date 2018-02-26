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
	[ActionCategory(ActionCategory.StateMachine)]
    [ActionTarget(typeof(PlayMakerFSM), "fsmComponent")]
	[Tooltip("Gets the name of the specified FSMs current state. Either reference the fsm component directly, or find it on a game object.")]
	public class GetFsmState : FsmStateAction
	{
        [Tooltip("Drag a PlayMakerFSM component here.")]
		public PlayMakerFSM fsmComponent;

        [Tooltip("If not specifyng the component above, specify the GameObject that owns the FSM")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of Fsm on Game Object. If left blank it will find the first PlayMakerFSM on the GameObject.")]
		public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the state name in a string variable.")]
		public FsmString storeResult;
		
        [Tooltip("Repeat every frame. E.g.,  useful if you're waiting for the state to change.")]
        public bool everyFrame;
		
		private PlayMakerFSM fsm;

		public override void Reset()
		{
			fsmComponent = null;
			gameObject = null;
			fsmName = "";
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetFsmState();
			
			if (!everyFrame)
			{
			    Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetFsmState();
		}
		
		void DoGetFsmState()
		{
			if (fsm == null)
			{
				if (fsmComponent != null)
				{
					fsm = fsmComponent;
				}
				else
				{
					var go = Fsm.GetOwnerDefaultTarget(gameObject);
					if (go != null)
					{
						fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
					}
				}
				
				if (fsm == null)
				{
					storeResult.Value = "";
					return;
				}
			}

			storeResult.Value = fsm.ActiveStateName;
		}

	}
}
