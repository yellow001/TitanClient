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
	[ActionCategory(ActionCategory.UnityObject)]
	[Tooltip("Gets a Component attached to a GameObject and stores it in an Object variable. NOTE: Set the Object variable's Object Type to get a component of that type. E.g., set Object Type to UnityEngine.AudioListener to get the AudioListener component on the camera.")]
	public class GetComponent : FsmStateAction
	{
        [Tooltip("The GameObject that owns the component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[RequiredField]
        [Tooltip("Store the component in an Object variable.\nNOTE: Set theObject variable's Object Type to get a component of that type. E.g., set Object Type to UnityEngine.AudioListener to get the AudioListener component on the camera.")]
		public FsmObject storeComponent;
		
        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			storeComponent = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetComponent();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetComponent();
		}
		
		void DoGetComponent()
		{
			if (storeComponent == null)
			{
				return;
			}
			
			var targetObject = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (targetObject == null)
			{
				return;
			}

			if (storeComponent.IsNone)
			{
				return;
			}

			storeComponent.Value = targetObject.GetComponent(storeComponent.ObjectType);
		}
	}
}