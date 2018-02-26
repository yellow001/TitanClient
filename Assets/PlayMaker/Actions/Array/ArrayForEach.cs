/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Iterate through the items in an Array and run an FSM on each item. NOTE: The FSM has to Finish before being run on the next item.")]
    public class ArrayForEach : RunFSMAction
    {
        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Array to iterate through.")]
        public FsmArray array;

        [HideTypeFilter]
        [MatchElementType("array")] 
        [UIHint(UIHint.Variable)] 
        [Tooltip("Store the item in a variable")]
        public FsmVar storeItem;

        [ActionSection("Run FSM")]

        public FsmTemplateControl fsmTemplateControl = new FsmTemplateControl();

        [Tooltip("Event to send after iterating through all items in the Array.")]
        public FsmEvent finishEvent;
    
        private int currentIndex;

        public override void Reset()
        {
            array = null;
            fsmTemplateControl = new FsmTemplateControl();
            runFsm = null;
        }

        /// <summary>
        /// Initialize FSM on awake so it doesn't cause hitches later
        /// </summary>
        public override void Awake()
        {
            if (array != null && fsmTemplateControl.fsmTemplate != null && Application.isPlaying)
            {
                runFsm = Fsm.CreateSubFsm(fsmTemplateControl);
            }
        }

	    public override void OnEnter()
	    {
            if (array == null || runFsm == null)
            {
                Finish();
                return;
            }

	        currentIndex = 0;
            StartFsm();
	    }

        public override void OnUpdate()
        {
            runFsm.Update();
            if (!runFsm.Finished)
            {
                return; // continue later
            }

            StartNextFsm();
        }

        public override void OnFixedUpdate()
        {
            runFsm.LateUpdate();
            if (!runFsm.Finished)
            {
                return; // continue later
            }

            StartNextFsm();
        }

        public override void OnLateUpdate()
        {
            runFsm.LateUpdate();
            if (!runFsm.Finished)
            {
                return; // continue later
            }

            StartNextFsm();
        }

        void StartNextFsm()
        {
            currentIndex++;
            StartFsm();
        }

        void StartFsm()
        {
            while (currentIndex < array.Length)
            {
                DoStartFsm();
                if (!runFsm.Finished)
                {
                    return; // continue later
                }
                currentIndex++;
            }

            Fsm.Event(finishEvent);
            Finish();
        }

        void DoStartFsm()
        {
            storeItem.SetValue(array.Values[currentIndex]);

            fsmTemplateControl.UpdateValues();
            fsmTemplateControl.ApplyOverrides(runFsm);

            runFsm.OnEnable();

            if (!runFsm.Started)
            {
                runFsm.Start();
            }
        }

        protected override void CheckIfFinished()
        {
        }
    }


}
