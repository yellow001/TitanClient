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
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [Tooltip("Get the value of a variable in another FSM and store it in a variable of the same name in this FSM.")]
    public class GetFsmVariable : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject that owns the FSM")]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        [Tooltip("Optional name of FSM on Game Object")]
        public FsmString fsmName;
        
        [RequiredField]
        [HideTypeFilter]
        [UIHint(UIHint.Variable)]
		[Tooltip("Store the value of the FsmVariable")]
        public FsmVar storeValue;

        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        private GameObject cachedGO;
        private PlayMakerFSM sourceFsm;
        private INamedVariable sourceVariable;
        private NamedVariable targetVariable;

        public override void Reset()
        {
            gameObject = null;
            fsmName = "";
            storeValue = new FsmVar();
        }

        public override void OnEnter()
        {
            InitFsmVar();

            DoGetFsmVariable();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoGetFsmVariable();
        }

        void InitFsmVar()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            if (go != cachedGO)
            {
                sourceFsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
                sourceVariable = sourceFsm.FsmVariables.GetVariable(storeValue.variableName);
                targetVariable = Fsm.Variables.GetVariable(storeValue.variableName);
                storeValue.Type = targetVariable.VariableType;

                if (!string.IsNullOrEmpty(storeValue.variableName) && sourceVariable == null)
                {
                    LogWarning("Missing Variable: " + storeValue.variableName);
                }

                cachedGO = go;
            }
        }

        void DoGetFsmVariable()
        {
            if (storeValue.IsNone)
            {
                return;
            }

            InitFsmVar();
            storeValue.GetValueFrom(sourceVariable);
            storeValue.ApplyValueTo(targetVariable);
        }

#if UNITY_EDITOR
        public override string AutoName()
        {
            return ("Get FSM Variable: " + ActionHelpers.GetValueLabel(storeValue.NamedVar));
        }
#endif
    }
}
