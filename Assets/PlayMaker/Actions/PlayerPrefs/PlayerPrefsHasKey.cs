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
	[ActionCategory("PlayerPrefs")]
	[Tooltip("Returns true if key exists in the preferences.")]
	public class PlayerPrefsHasKey : FsmStateAction
	{
		[RequiredField]
		public FsmString key;

		[UIHint(UIHint.Variable)]
		[Title("Store Result")]
		public FsmBool variable;

		[Tooltip("Event to send if key exists.")]
		public FsmEvent trueEvent;

		[Tooltip("Event to send if key does not exist.")]
		public FsmEvent falseEvent;

		public override void Reset()
		{
			key = "";
		}

		public override void OnEnter()
		{
			Finish();

			if (!key.IsNone && !key.Value.Equals(""))
			{
				variable.Value = PlayerPrefs.HasKey(key.Value);
			}

			Fsm.Event(variable.Value ? trueEvent : falseEvent);
		}
	}
}
