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
    [ActionCategory(ActionCategory.String)]
    [Tooltip("Replaces each format item in a specified string with the text equivalent of variable's value. Stores the result in a string variable.")]
    public class FormatString : FsmStateAction
    {
        [RequiredField]
        [Tooltip("E.g. Hello {0} and {1}\nWith 2 variables that replace {0} and {1}\nSee C# string.Format docs.")]
        public FsmString format;

        [Tooltip("Variables to use for each formatting item.")]
        public FsmVar[] variables;

        [RequiredField]
        [UIHint(UIHint.Variable)] 
        [Tooltip("Store the formatted result in a string variable.")]
        public FsmString storeResult;

        [Tooltip("Repeat every frame. This is useful if the variables are changing.")]
        public bool everyFrame;

        private object[] objectArray;

        public override void Reset()
        {
            format = null;
            variables = null;
            storeResult = null;
            everyFrame = false;
        }

        // Code that runs on entering the state.
        public override void OnEnter()
        {
            objectArray = new object[variables.Length];

            DoFormatString();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoFormatString();
        }

        void DoFormatString()
        {
            for (var i = 0; i < variables.Length; i++)
            {
				variables[i].UpdateValue();
                objectArray[i] = variables[i].GetValue();
            } 
            
            try
            {
                storeResult.Value = string.Format(format.Value, objectArray);
            }
            catch (System.FormatException e)
            {
                LogError(e.Message);
                Finish();
            }   
        }
    }
}
