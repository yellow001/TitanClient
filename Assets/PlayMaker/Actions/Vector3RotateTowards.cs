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
    [ActionCategory(ActionCategory.Vector3)]
    [Tooltip("Rotates a Vector3 direction from Current towards Target.")]
    public class Vector3RotateTowards : FsmStateAction
    {
        [RequiredField]
        public FsmVector3 currentDirection;
        [RequiredField]
        public FsmVector3 targetDirection;
        [RequiredField]
        [Tooltip("Rotation speed in degrees per second")]
        public FsmFloat rotateSpeed;
        [RequiredField]
        [Tooltip("Max Magnitude per second")]
        public FsmFloat maxMagnitude;
        public override void Reset()
        {
            currentDirection = new FsmVector3 { UseVariable = true };
            targetDirection = new FsmVector3 { UseVariable = true };
            rotateSpeed = 360;
            maxMagnitude = 1;
        }

        public override void OnUpdate()
        {
            currentDirection.Value = Vector3.RotateTowards(currentDirection.Value, targetDirection.Value, rotateSpeed.Value * Mathf.Deg2Rad * Time.deltaTime, maxMagnitude.Value);
        }
    }
}

