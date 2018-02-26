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
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Checks if an Object has a Component. Optionally remove the Component on exiting the state.")]
	public class HasComponent : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		[RequiredField]
		[UIHint(UIHint.ScriptComponent)]
		public FsmString component;
		public FsmBool removeOnExit;
		public FsmEvent trueEvent;
		public FsmEvent falseEvent;
		[UIHint(UIHint.Variable)]
		public FsmBool store;
		public bool everyFrame;

		Component aComponent;

		public override void Reset()
		{
			aComponent = null;
			gameObject = null;
			trueEvent = null;
			falseEvent = null;
			component = null;
			store = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoHasComponent(gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value);
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoHasComponent(gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value);
		}

		public override void OnExit()
		{
			if (removeOnExit.Value && aComponent != null)
			{
				Object.Destroy(aComponent);
			}
		}

		void DoHasComponent(GameObject go)
		{
		
			if ( go==null)
			{
				if (!store.IsNone)
				{
					store.Value = false;
				}
				Fsm.Event(falseEvent);
				return;
			}
		
			aComponent = go.GetComponent(ReflectionUtils.GetGlobalType(component.Value));
			
			if (!store.IsNone)
			{
				store.Value = aComponent != null;
			}
			
			Fsm.Event(aComponent != null ? trueEvent : falseEvent);
		}
	}
}
