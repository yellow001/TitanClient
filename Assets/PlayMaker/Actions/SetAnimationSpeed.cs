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
	[Tooltip("Sets the Speed of an Animation. Check Every Frame to update the animation time continuosly, e.g., if you're manipulating a variable that controls animation speed.")]
	public class SetAnimationSpeed : BaseAnimationAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		public FsmOwnerDefault gameObject;
		[RequiredField]
		[UIHint(UIHint.Animation)]
		public FsmString animName;
		public FsmFloat speed = 1f;
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			animName = null;
			speed = 1f;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetAnimationSpeed(gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value);
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetAnimationSpeed(gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value);
		}

	    private void DoSetAnimationSpeed(GameObject go)
		{
		    if (!UpdateCache(go))
		    {
		        return;
		    }

            var anim = animation[animName.Value];
			if (anim == null)
			{
				LogWarning("Missing animation: " + animName.Value);
				return;
			}

			anim.speed = speed.Value;
		}

		/*
			public override string ErrorCheck()
			{
				return ErrorCheckHelpers.CheckAnimationSetup(gameObject.value);
			}*/
	}
}
