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
    [Tooltip("Sets all items in an Array to their default value: 0, empty string, false, or null depending on their type. Optionally defines a reset value to use.")]
    public class ArrayClear : FsmStateAction
    {
        [UIHint(UIHint.Variable)] 
        [Tooltip("The Array Variable to clear.")] 
        public FsmArray array;

        [MatchElementType("array")] 
        [Tooltip("Optional reset value. Leave as None for default value.")] 
        public FsmVar resetValue;

        public override void Reset()
        {
            array = null;
            resetValue = new FsmVar() {useVariable = true};
        }

        public override void OnEnter()
        {
            int count = array.Length;

            array.Reset();
            array.Resize(count);

            if (!resetValue.IsNone)
            {
                resetValue.UpdateValue();
                object _val = resetValue.GetValue();
                for (int i = 0; i < count; i++)
                {
                    array.Set(i, _val);
                }
            }
            Finish();
        }
    }
}
