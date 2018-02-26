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
	[Tooltip("Logs the value of a Float Variable in the PlayMaker Log Window.")]
	public class DebugFloat : BaseLogAction
	{
        [Tooltip("Info, Warning, or Error.")]
        public LogLevel logLevel;
		
        [UIHint(UIHint.Variable)]
        [Tooltip("The Float variable to debug.")]
		public FsmFloat floatVariable;

		public override void Reset()
		{
			logLevel = LogLevel.Info;
			floatVariable = null;
            base.Reset();
		}

		public override void OnEnter()
		{
			string text = "None";
			
			if (!floatVariable.IsNone)
			{
				text = floatVariable.Name + ": " + floatVariable.Value;
			}

			ActionHelpers.DebugLog(Fsm, logLevel, text, sendToUnityLog);

			Finish();
		}
	}
}
