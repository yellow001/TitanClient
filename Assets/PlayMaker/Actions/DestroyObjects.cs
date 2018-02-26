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
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Destroys GameObjects in an array.")]
	public class DestroyObjects : FsmStateAction
	{
        [RequiredField]
        [ArrayEditor(VariableType.GameObject)]
        [Tooltip("The GameObjects to destroy.")]
        public FsmArray gameObjects;

		[HasFloatSlider(0, 5)]
		[Tooltip("Optional delay before destroying the Game Objects.")]
		public FsmFloat delay;

		[Tooltip("Detach children before destroying the Game Objects.")]
		public FsmBool detachChildren;

		public override void Reset()
		{
			gameObjects = null;
			delay = 0;
		}

		public override void OnEnter()
		{
            if (gameObjects.Values != null)
		    {
                foreach (GameObject go in gameObjects.Values)
                {
                    if (go != null)
                    {
                        if (delay.Value <= 0)
                        {
                            Object.Destroy(go);
                        }
                        else
                        {
                            Object.Destroy(go, delay.Value);
                        }

                        if (detachChildren.Value)
                        {
                            go.transform.DetachChildren();
                        }
                    }
                }
		        
		    }

			Finish();
		}
	}
}
