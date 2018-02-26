/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) copyright Hutong Games, LLC 2010-2012. All rights reserved.

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [Note("Kill all queued delayed events.")]
    [Tooltip("Kill all queued delayed events. " +
             "Normally delayed events are automatically killed when the active state is exited, " +
             "but you can override this behaviour in FSM settings. " +
             "If you choose to keep delayed events you can use this action to kill them when needed.")]
    public class KillDelayedEvents : FsmStateAction
    {
        public override void OnEnter()
        {
            Fsm.KillDelayedEvents();           
            Finish();
        }
    }
}
