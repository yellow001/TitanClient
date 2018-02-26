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
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Enables/Disables an Animation on a GameObject.\nAnimation time is paused while disabled. Animation must also have a non zero weight to play.")]
	public class EnableAnimation : BaseAnimationAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
        [Tooltip("The GameObject playing the animation.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Animation)]
        [Tooltip("The name of the animation to enable/disable.")]
		public FsmString animName;
		
		[RequiredField]
        [Tooltip("Set to True to enable, False to disable.")]
		public FsmBool enable;
		
        [Tooltip("Reset the initial enabled state when exiting the state.")]
		public FsmBool resetOnExit;
		
		private AnimationState anim;
		
		public override void Reset()
		{
			gameObject = null;
			animName = null;
			enable = true;
			resetOnExit = false;
		}

		public override void OnEnter()
		{
			DoEnableAnimation(Fsm.GetOwnerDefaultTarget(gameObject));
			
			Finish();
		}

		void DoEnableAnimation(GameObject go)
		{
		    if (UpdateCache(go))
		    {
                anim = animation[animName.Value];
                if (anim != null)
                {
                    anim.enabled = enable.Value;
                }
		    }
		}
		
		public override void OnExit()
		{
			if (resetOnExit.Value && anim != null)
			{
				anim.enabled = !enable.Value;
			}
		}
	}
}
