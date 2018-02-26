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
	[Tooltip("Adds a Component to a Game Object. Use this to change the behaviour of objects on the fly. Optionally remove the Component on exiting the state.")]
	public class AddComponent : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to add the Component to.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.ScriptComponent)]
        [Title("Component Type"), Tooltip("The type of Component to add to the Game Object.")]
		public FsmString component;

        [UIHint(UIHint.Variable)]
        [ObjectType(typeof(Component))]
        [Tooltip("Store the component in an Object variable. E.g., to use with Set Property.")]
	    public FsmObject storeComponent;

		[Tooltip("Remove the Component when this State is exited.")]
		public FsmBool removeOnExit;

        // save added component so we can remove on exit
	    private Component addedComponent;

		public override void Reset()
		{
			gameObject = null;
			component = null;
		    storeComponent = null;
		}

		public override void OnEnter()
		{
			DoAddComponent();
			
			Finish();
		}

		public override void OnExit()
		{
			if (removeOnExit.Value && addedComponent != null)
			{
				Object.Destroy(addedComponent);
			}
		}

	    private void DoAddComponent()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;

			addedComponent = go.AddComponent(ReflectionUtils.GetGlobalType(component.Value));
		    storeComponent.Value = addedComponent;

			if (addedComponent == null)
			{
				LogError("Can't add component: " + component.Value);
			}
		}
	}
}
