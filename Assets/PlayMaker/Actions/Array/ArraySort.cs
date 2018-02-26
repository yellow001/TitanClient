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
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Sort items in an Array.")]
    public class ArraySort : FsmStateAction
    {
        [RequiredField]
        [UIHint(UIHint.Variable)] 
        [Tooltip("The Array to sort.")] 
        public FsmArray array;

        public override void Reset()
        {
            array = null;
        }

        public override void OnEnter()
        {
            var list = new List<object>(array.Values);
            list.Sort();
            array.Values = list.ToArray();

            Finish();
        }
    }
}
