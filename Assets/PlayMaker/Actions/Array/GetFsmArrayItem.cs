/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [Tooltip("Gets an item in an Array Variable in another FSM.")]
    public class GetFsmArrayItem : BaseFsmVariableIndexAction
    {
        [RequiredField]
        [Tooltip("The GameObject that owns the FSM.")]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        [Tooltip("Optional name of FSM on Game Object.")]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmArray)]
        [Tooltip("The name of the FSM variable.")]
        public FsmString variableName;

        [Tooltip("The index into the array.")]
        public FsmInt index;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        //[MatchElementType("array")] TODO
        [Tooltip("Get the value of the array at the specified index.")]
        public FsmVar storeValue;

        [Tooltip("Repeat every frame. Useful if the value is changing.")]
        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            fsmName = "";
            storeValue = null;
        }

        public override void OnEnter()
        {
            DoGetFsmArray();

            if (!everyFrame)
            {
                Finish();
            }
        }

        private void DoGetFsmArray()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (!UpdateCache(go, fsmName.Value))
            {
                return;
            }

            var fsmArray = fsm.FsmVariables.GetFsmArray(variableName.Value);
            if (fsmArray != null)
            {
                if (index.Value < 0 || index.Value >= fsmArray.Length)
                {
                    Fsm.Event(indexOutOfRange);
                    Finish();
                    return;
                }

                if (fsmArray.ElementType == storeValue.NamedVar.VariableType)
                {
                    storeValue.SetValue(fsmArray.Get(index.Value));
                }
                else
                {
                    LogWarning("Incompatible variable type: " + variableName.Value);
                }
            }
            else
            {
                DoVariableNotFound(variableName.Value);
            }
        }

        public override void OnUpdate()
        {
            DoGetFsmArray();
        }

    }
}
