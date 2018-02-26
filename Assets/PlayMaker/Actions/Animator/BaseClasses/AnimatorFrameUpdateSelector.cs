/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	#pragma warning disable 414

	public abstract class FsmStateActionAnimatorBase : FsmStateAction
	{
		public enum AnimatorFrameUpdateSelector {OnUpdate,OnAnimatorMove,OnAnimatorIK};

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		[Tooltip("Select when to perform the action, during OnUpdate, OnAnimatorMove, OnAnimatorIK")]
		public AnimatorFrameUpdateSelector everyFrameOption;

		/// <summary>
		/// The layerIndex index passed when processing action during OnAnimatorIK
		/// </summary>
		protected int IklayerIndex;

		/// <summary>
		/// Raises the action update event. Could be fired during onUpdate or OnAnimatorMove based on the action setup.
		/// </summary>
		public abstract void OnActionUpdate();
		
		public override void Reset()
		{
			everyFrame = false;
			everyFrameOption = AnimatorFrameUpdateSelector.OnUpdate;
		}

		public override void OnPreprocess()
		{
			if (everyFrameOption == AnimatorFrameUpdateSelector.OnAnimatorMove)
			{
				Fsm.HandleAnimatorMove = true;
			}

			if (everyFrameOption == AnimatorFrameUpdateSelector.OnAnimatorIK)
			{
				Fsm.HandleAnimatorIK = true;
			}
		}

		public override void OnUpdate()
		{

			if (everyFrameOption == AnimatorFrameUpdateSelector.OnUpdate)
			{
				OnActionUpdate();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void DoAnimatorMove ()
		{
			if (everyFrameOption == AnimatorFrameUpdateSelector.OnAnimatorMove)
			{

				OnActionUpdate();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void DoAnimatorIK (int layerIndex)
		{
			IklayerIndex = layerIndex;

			if (everyFrameOption == AnimatorFrameUpdateSelector.OnAnimatorIK)
			{
				OnActionUpdate();
			}
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
	}
}
