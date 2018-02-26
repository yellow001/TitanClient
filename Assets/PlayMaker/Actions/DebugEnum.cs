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
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Logs the value of an Enum Variable in the PlayMaker Log Window.")]
	public class DebugEnum : BaseLogAction
	{
        [Tooltip("Info, Warning, or Error.")]
        public LogLevel logLevel;
		
        [UIHint(UIHint.Variable)]
        [Tooltip("The Enum Variable to debug.")]
		public FsmEnum enumVariable;

		public override void Reset()
		{
			logLevel = LogLevel.Info;
			enumVariable = null;
            base.Reset();
		}

		public override void OnEnter()
		{
			var text = "None";

			if (!enumVariable.IsNone)
			{
				text = enumVariable.Name + ": " + enumVariable.Value;
			}
			
			ActionHelpers.DebugLog(Fsm, logLevel, text, sendToUnityLog);
						
			Finish();
		}
	}
}
