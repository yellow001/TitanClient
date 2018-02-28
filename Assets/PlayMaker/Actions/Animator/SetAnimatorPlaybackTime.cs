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
	[ActionCategory(ActionCategory.Animator)]
	[Tooltip("Sets the playback position in the recording buffer. When in playback mode (use AnimatorStartPlayback), this value is used for controlling the current playback position in the buffer (in seconds). The value can range between recordingStartTime and recordingStopTime ")]
	public class SetAnimatorPlayBackTime: FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Animator))]
		[Tooltip("The Target. An Animator component is required")]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("The playBack time")]
		public FsmFloat playbackTime;
		
		[Tooltip("Repeat every frame. Useful for changing over time.")]
		public bool everyFrame;
		
		private Animator _animator;
		
		public override void Reset()
		{
			gameObject = null;
			playbackTime= null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			// get the animator component
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (go==null)
			{
				Finish();
				return;
			}
			
			_animator = go.GetComponent<Animator>();
			
			if (_animator==null)
			{
				Finish();
				return;
			}
			
			DoPlaybackTime();
			
			if (!everyFrame) 
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoPlaybackTime();
		}
		
		
		void DoPlaybackTime()
		{		
			if (_animator==null)
			{
				return;
			}
			
			_animator.playbackTime = playbackTime.Value;
			
		}
		
	}
}