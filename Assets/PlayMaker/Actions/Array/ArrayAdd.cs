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
    [Tooltip("Add an item to the end of an Array.")]
    public class ArrayAdd : FsmStateAction
    {
        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The Array Variable to use.")]
        public FsmArray array;

        [RequiredField]
        [MatchElementType("array")]
        [Tooltip("Item to add.")]
        public FsmVar value;

        public override void Reset()
        {
            array = null;
            value = null;
        }

        public override void OnEnter()
        {
            DoAddValue();
            Finish();
        }

        private void DoAddValue()
        {
            array.Resize(array.Length + 1);
            value.UpdateValue();
            array.Set(array.Length - 1, value.GetValue());
        }

    }

}

