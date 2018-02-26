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
    /// <summary>
    /// Base class for Get/Set FSM Variable actions
    /// </summary>
    [ActionCategory(ActionCategory.StateMachine)]
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    public abstract class BaseFsmVariableIndexAction : FsmStateAction
    {
        [ActionSection("Events")]

        [Tooltip("The event to trigger if the index is out of range")]
        public FsmEvent indexOutOfRange;

        [Tooltip("The event to send if the FSM is not found.")]
        public FsmEvent fsmNotFound;

        [Tooltip("The event to send if the Variable is not found.")]
        public FsmEvent variableNotFound;

        private GameObject cachedGameObject;
        private string cachedFsmName;

        protected PlayMakerFSM fsm;

        public override void Reset()
        {
            fsmNotFound = null;
            variableNotFound = null;
        }

        protected bool UpdateCache(GameObject go, string fsmName)
        {
            if (go == null)
            {
                return false;
            }

            if (fsm == null || cachedGameObject != go || cachedFsmName != fsmName)
            {
                fsm = ActionHelpers.GetGameObjectFsm(go, fsmName);
                cachedGameObject = go;
                cachedFsmName = fsmName;

                if (fsm == null)
                {
                    LogWarning("Could not find FSM: " + fsmName);
                    Fsm.Event(fsmNotFound);
                }
            }

            return true;
        }

        protected void DoVariableNotFound(string variableName)
        {
            LogWarning("Could not find variable: " + variableName);
            Fsm.Event(variableNotFound);
        }
    }
}
