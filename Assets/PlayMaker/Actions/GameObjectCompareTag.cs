/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if a Game Object has a tag.")]
	public class GameObjectCompareTag : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The GameObject to test.")]
		public FsmGameObject gameObject;

		[RequiredField]
		[UIHint(UIHint.Tag)]
        [Tooltip("The Tag to check for.")]
		public FsmString tag;

        [Tooltip("Event to send if the GameObject has the Tag.")]
		public FsmEvent trueEvent;

        [Tooltip("Event to send if the GameObject does not have the Tag.")]
		public FsmEvent falseEvent;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a Bool variable.")]
        public FsmBool storeResult;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			tag = "Untagged";
			trueEvent = null;
			falseEvent = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoCompareTag();
				
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoCompareTag();
		}

		void DoCompareTag()
		{
			var hasTag = false;

			if (gameObject.Value != null)
			{
				hasTag = gameObject.Value.CompareTag(tag.Value);
			}

			storeResult.Value = hasTag;

			Fsm.Event(hasTag ? trueEvent : falseEvent);
		}
	}
}
