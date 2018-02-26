/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) copyright Hutong Games, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Forward an event received by this FSM to another target.")]
    public class ForwardEvent : FsmStateAction
    {
        [Tooltip("Forward to this target.")]
        public FsmEventTarget forwardTo;

        [Tooltip("The events to forward.")]
        public FsmEvent[] eventsToForward;

        [Tooltip("Should this action eat the events or pass them on.")]
        public bool eatEvents;

        public override void Reset()
        {
            forwardTo = new FsmEventTarget { target = FsmEventTarget.EventTarget.FSMComponent };
            eventsToForward = null;
            eatEvents = true;
        }

        /// <summary>
        /// Return true to eat the event
        /// </summary>
        public override bool Event(FsmEvent fsmEvent)
        {
            if (eventsToForward != null)
            {
                foreach (var e in eventsToForward)
                {
                    if (e == fsmEvent)
                    {
                        Fsm.Event(forwardTo, fsmEvent);

                        return eatEvents;
                    }
                }
            }

            return false;
        }
    }
}
