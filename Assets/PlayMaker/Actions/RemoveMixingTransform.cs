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
	[Tooltip("Removes a mixing transform previously added with Add Mixing Transform. If transform has been added as recursive, then it will be removed as recursive. Once you remove all mixing transforms added to animation state all curves become animated again.")]
    public class RemoveMixingTransform : BaseAnimationAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		[Tooltip("The GameObject playing the animation.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The name of the animation.")]
		public FsmString animationName;
		[RequiredField]
		
		[Tooltip("The mixing transform to remove. E.g., root/upper_body/left_shoulder")]
		public FsmString transfrom;

		public override void Reset()
		{
			gameObject = null;
			animationName = "";
		}

		public override void OnEnter()
		{
			DoRemoveMixingTransform();
			
			Finish();
		}

		void DoRemoveMixingTransform()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (!UpdateCache(go))
			{
				return;
			}

			var animClip = animation[animationName.Value];
			if (animClip == null)
			{
				return;
			}

			var mixingTransform = go.transform.Find(transfrom.Value);
			animClip.AddMixingTransform(mixingTransform);
		}
	}
}
