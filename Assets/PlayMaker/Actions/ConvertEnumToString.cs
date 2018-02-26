/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Convert)]
    [Tooltip("Converts an Enum value to a String value.")]
    public class ConvertEnumToString : FsmStateAction
    {
        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The Enum variable to convert.")]
        public FsmEnum enumVariable;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The String variable to store the converted value.")]
        public FsmString stringVariable;

        [Tooltip("Repeat every frame. Useful if the Enum variable is changing.")]
        public bool everyFrame;

        public override void Reset()
        {
            enumVariable = null;
            stringVariable = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoConvertEnumToString();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoConvertEnumToString();
        }

        void DoConvertEnumToString()
        {
            stringVariable.Value = enumVariable.Value != null ? enumVariable.Value.ToString() : "";
        }
    }
}
