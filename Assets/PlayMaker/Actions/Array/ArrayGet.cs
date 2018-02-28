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
    [Tooltip("Get a value at an index. Index must be between 0 and the number of items -1. First item is index 0.")]
    public class ArrayGet : FsmStateAction
    {
        [RequiredField] 
        [UIHint(UIHint.Variable)] 
        [Tooltip("The Array Variable to use.")] 
        public FsmArray array;

        [Tooltip("The index into the array.")] 
        public FsmInt index;

        [RequiredField]
        [MatchElementType("array")]
        [UIHint(UIHint.Variable)] 
        [Tooltip("Store the value in a variable.")] 
        public FsmVar storeValue;

        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;

        [ActionSection("Events")]

        [Tooltip("The event to trigger if the index is out of range")] 
        public FsmEvent indexOutOfRange;

        public override void Reset()
        {
            array = null;
            index = null;
            everyFrame = false;
            storeValue = null;
            indexOutOfRange = null;
        }

        public override void OnEnter()
        {
            DoGetValue();

            if (!everyFrame)
            {
                Finish();
            }

        }

        public override void OnUpdate()
        {
            DoGetValue();

        }

        private void DoGetValue()
        {
            if (array.IsNone || storeValue.IsNone)
            {
                return;
            }

            if (index.Value >= 0 && index.Value < array.Length)
            {
                storeValue.SetValue(array.Get(index.Value));
            }
            else
            {
                //LogError("Index out of Range: " + index.Value);
                Fsm.Event(indexOutOfRange);
            }
        }

        /* Not sure it's a good idea to range check at edit time since it can change at runtime
        public override string ErrorCheck()
        {
            if (index.Value<0 || index.Value >= array.Length)
            {
                return "Index out of Range. Please select an index between 0 and the number of items -1. First item is index 0.";
            }
            return "";
        }*/

#if UNITY_EDITOR
        public override string AutoName()
        {
            return ActionHelpers.GetValueLabel(storeValue.NamedVar) + "=" + array.Name + "[" + ActionHelpers.GetValueLabel(index) + "]";
        }
#endif
    }
}