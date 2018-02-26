/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Logic)]
    [Tooltip("Compares 2 Enum values and sends Events based on the result.")]
    public class EnumCompare : FsmStateAction
    {
        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmEnum enumVariable;

        [MatchFieldType("enumVariable")]
        public FsmEnum compareTo;

        public FsmEvent equalEvent;

        public FsmEvent notEqualEvent;
        
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the true/false result in a bool variable.")]
        public FsmBool storeResult;
        
        [Tooltip("Repeat every frame. Useful if the enum is changing over time.")]
        public bool everyFrame;

        public override void Reset()
        {
            enumVariable = null;
            compareTo = null;
            equalEvent = null;
            notEqualEvent = null;
            storeResult = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoEnumCompare();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoEnumCompare();
        }

        void DoEnumCompare()
        {
            if (enumVariable == null || compareTo == null) return;

            var equal = Equals(enumVariable.Value, compareTo.Value);

            if (storeResult != null)
            {
                storeResult.Value = equal;
            }

            if (equal && equalEvent != null)
            {
                Fsm.Event(equalEvent);
                return;
            }

            if (!equal && notEqualEvent != null)
            {
                Fsm.Event(notEqualEvent);
            }

        }

    }
}
