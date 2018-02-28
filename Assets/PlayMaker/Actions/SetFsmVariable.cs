/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) copyright Hutong Games, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [Tooltip("Set the value of a variable in another FSM.")]
    public class SetFsmVariable : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject that owns the FSM")]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        [Tooltip("Optional name of FSM on Game Object")]
        public FsmString fsmName;

        [Tooltip("The name of the variable in the target FSM.")]
        public FsmString variableName;

        [RequiredField]
        public FsmVar setValue;

        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        private PlayMakerFSM targetFsm;
        private NamedVariable targetVariable;
        private INamedVariable sourceVariable;

        private GameObject cachedGameObject;
        private string cachedFsmName;
        private string cachedVariableName;

        public override void Reset()
        {
            gameObject = null;
            fsmName = "";
            setValue = new FsmVar();
        }

        public override void OnEnter()
        {
            DoSetFsmVariable();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoSetFsmVariable();
        }

        private void DoSetFsmVariable()
        {
            if (setValue.IsNone || string.IsNullOrEmpty(variableName.Value))
            {
                return;
            }

            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            if (go != cachedGameObject || fsmName.Value != cachedFsmName)
            {
                targetFsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
                if (targetFsm == null)
                {
                    return;
                }
                cachedGameObject = go;
                cachedFsmName = fsmName.Value;
            }

            if (variableName.Value != cachedVariableName)
            {
                targetVariable = targetFsm.FsmVariables.FindVariable(setValue.Type, variableName.Value);
                cachedVariableName = variableName.Value;
            }

            if (targetVariable == null)
            {
                LogWarning("Missing Variable: " + variableName.Value);
                return;
            }

            setValue.ApplyValueTo(targetVariable);
        }

#if UNITY_EDITOR
        public override string AutoName()
        {
            return ("Set FSM Variable: " + ActionHelpers.GetValueLabel(variableName));
        }
#endif
    }
}