/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
// http://hutonggames.com/playmakerforum/index.php?topic=63.0
// Thanks: MaDDoX

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Transform)]
    [Tooltip("Gets the Scale of a Game Object and stores it in a Vector3 Variable or each Axis in a Float Variable")]
    public class GetScale : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        
		[UIHint(UIHint.Variable)]
        public FsmVector3 vector;
        
		[UIHint(UIHint.Variable)]
        public FsmFloat xScale;
        
		[UIHint(UIHint.Variable)]
        public FsmFloat yScale;
        
		[UIHint(UIHint.Variable)]
        public FsmFloat zScale;
        
		public Space space;
        
		public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            vector = null;
            xScale = null;
            yScale = null;
            zScale = null;
            space = Space.World;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoGetScale();

            if (!everyFrame)
            {
            	Finish();
            }
        }

        public override void OnUpdate()
        {
            DoGetScale();
        }

        void DoGetScale()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
            	return;
            }

        	var scale = space == Space.World ? go.transform.lossyScale : go.transform.localScale;

            vector.Value = scale;
            xScale.Value = scale.x;
            yScale.Value = scale.y;
            zScale.Value = scale.z;
        }


    }
}
