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
	[Tooltip("Measures the Distance betweens 2 Game Objects and stores the result in a Float Variable.")]
	public class GetDistance : FsmStateAction
	{
		[RequiredField]
        [Tooltip("Measure distance from this GameObject.")]
		public FsmOwnerDefault gameObject;
		
        [RequiredField]
		[Tooltip("Target GameObject.")]
        public FsmGameObject target;
		
        [RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the distance in a float variable.")]
		public FsmFloat storeResult;
		
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			target = null;
			storeResult = null;
			everyFrame = true;
		}
		
		public override void OnEnter()
		{
			DoGetDistance();

		    if (!everyFrame)
		    {
		        Finish();
		    }
		}
		public override void OnUpdate()
		{
			DoGetDistance();
		}		
		
		void DoGetDistance()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (go == null || target.Value == null || storeResult == null)
		    {
		        return;
		    }
					
			storeResult.Value = Vector3.Distance(go.transform.position, target.Value.transform.position);
		}

	}
}
