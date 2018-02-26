/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Logs the value of a Vector2 Variable in the PlayMaker Log Window.")]
	public class DebugVector2 : FsmStateAction
	{
        [Tooltip("Info, Warning, or Error.")]
		public LogLevel logLevel;

		[UIHint(UIHint.Variable)]
        [Tooltip("Prints the value of a Vector2 variable in the PlayMaker log window.")]
		public FsmVector2 vector2Variable;

		public override void Reset()
		{
			logLevel = LogLevel.Info;
			vector2Variable = null;
		}

		public override void OnEnter()
		{
			string text = "None";
			
			if (!vector2Variable.IsNone)
			{
				text = vector2Variable.Name + ": " + vector2Variable.Value;
			}

			ActionHelpers.DebugLog(Fsm, logLevel, text);

			Finish();
		}
	}
}
