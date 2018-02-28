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
	[ActionCategory(ActionCategory.RenderSettings)]
	[Tooltip("Sets the color of the Fog in the scene.")]
	public class SetFogColor : FsmStateAction
	{
		[RequiredField]
		public FsmColor fogColor;
		public bool everyFrame;

		public override void Reset()
		{
			fogColor = Color.white;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetFogColor();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoSetFogColor();
		}
		
		void DoSetFogColor()
		{
			RenderSettings.fogColor = fogColor.Value;
		}
	}
}