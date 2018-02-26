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
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends an Event based on the value of a Float Variable. The float could represent distance, angle to a target, health left... The array sets up float ranges that correspond to Events.")]
	public class FloatSwitch : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The float variable to test.")]
		public FsmFloat floatVariable;

		[CompoundArray("Float Switches", "Less Than", "Send Event")]
		public FsmFloat[] lessThan;
		public FsmEvent[] sendEvent;
		
        [Tooltip("Repeat every frame. Useful if the variable is changing.")]
        public bool everyFrame;

		public override void Reset()
		{
			floatVariable = null;
			lessThan = new FsmFloat[1];
			sendEvent = new FsmEvent[1];
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoFloatSwitch();
			
			if (!everyFrame)
			{
			    Finish();
			}
		}

		public override void OnUpdate()
		{
			DoFloatSwitch();
		}
		
		void DoFloatSwitch()
		{
			if (floatVariable.IsNone)
			{
			    return;
			}
			
			for (var i = 0; i < lessThan.Length; i++) 
			{
				if (floatVariable.Value < lessThan[i].Value)
				{
					Fsm.Event(sendEvent[i]);
					return;
				}
			}
		}
	}
}
