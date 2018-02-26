/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Resize an array.")]
    public class ArrayResize : FsmStateAction
    {
        [RequiredField] 
        [UIHint(UIHint.Variable)] 
        [Tooltip("The Array Variable to resize")] 
        public FsmArray array;

        [Tooltip("The new size of the array.")] 
        public FsmInt newSize;

        [Tooltip("The event to trigger if the new size is out of range")] 
        public FsmEvent sizeOutOfRangeEvent;

        public override void OnEnter()
        {
            if (newSize.Value >= 0)
            {
                array.Resize(newSize.Value);
            }
            else
            {
                LogError("Size out of range: " + newSize.Value);
                Fsm.Event(sizeOutOfRangeEvent);
            }

            Finish();
        }

        /* Should be disallowed by the UI now
        public override string ErrorCheck()
        {
            if (newSize.Value<0)
            {
                return "newSize must be a positive value.";
            }
            return "";
        }*/

    }
}
