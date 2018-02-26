/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) copyright Hutong Games, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Debug)]
    [Tooltip("Print the value of any FSM Variable in the PlayMaker Log Window.")]
    public class DebugFsmVariable : BaseLogAction
    {
        [Tooltip("Info, Warning, or Error.")]
        public LogLevel logLevel;

        [HideTypeFilter]
        [UIHint(UIHint.Variable)]
        [Tooltip("The variable to debug.")]
        public FsmVar variable;

        public override void Reset()
        {
            logLevel = LogLevel.Info;
            variable = null;
            base.Reset();
        }

        public override void OnEnter()
        {
            ActionHelpers.DebugLog(Fsm, logLevel, variable.DebugString(), sendToUnityLog);

            Finish();
        }
    }
}
