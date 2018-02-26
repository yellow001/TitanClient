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
	[Tooltip("Compare 2 Object Variables and send events based on the result.")]
	public class ObjectCompare : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmObject objectVariable;
		
		[RequiredField]
		public FsmObject compareTo;

		//[ActionSection("")]

		[Tooltip("Event to send if the 2 object values are equal.")]
		public FsmEvent equalEvent;
		
		[Tooltip("Event to send if the 2 object values are not equal.")]
		public FsmEvent notEqualEvent;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a variable.")]
		public FsmBool storeResult;

		//[ActionSection("")]

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			objectVariable = null;
			compareTo = null;
			storeResult = null;
			equalEvent = null;
			notEqualEvent = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoObjectCompare();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoObjectCompare();
		}

		void DoObjectCompare()
		{
			var result = objectVariable.Value == compareTo.Value;

			storeResult.Value = result;

			Fsm.Event(result ? equalEvent : notEqualEvent);
		}
	}
}
