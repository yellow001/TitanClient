/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

#if (UNITY_EDITOR || UNITY_IPHONE || UNITY_IOS || UNITY_ANDROID)

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Plays a full-screen movie on a handheld device. Please consult the Unity docs for Handheld.PlayFullScreenMovie for proper usage.")]
	public class DevicePlayFullScreenMovie : FsmStateAction
	{
        [RequiredField]
		[Tooltip("Note that player will stream movie directly from the iPhone disc, therefore you have to provide movie as a separate files and not as an usual asset.\nYou will have to create a folder named StreamingAssets inside your Unity project (inside your Assets folder). Store your movies inside that folder. Unity will automatically copy contents of that folder into the iPhone application bundle.")]
		public FsmString moviePath;

		[RequiredField]
		[Tooltip("This action will initiate a transition that fades the screen from your current content to the designated background color of the player. When playback finishes, the player uses another fade effect to transition back to your content.")]
		public FsmColor fadeColor;

		[Tooltip("Options for displaying movie playback controls. See Unity docs.")]
		public FullScreenMovieControlMode movieControlMode;

		[Tooltip("Scaling modes for displaying movies.. See Unity docs.")]
		public FullScreenMovieScalingMode movieScalingMode;

        public override void Reset()
		{
			moviePath = "";
			fadeColor = Color.black;

			movieControlMode = FullScreenMovieControlMode.Full;
			movieScalingMode = FullScreenMovieScalingMode.AspectFit;
		}

		public override void OnEnter()
		{
            Handheld.PlayFullScreenMovie(moviePath.Value, fadeColor.Value, movieControlMode, movieScalingMode);
        }
	}
}

#endif
