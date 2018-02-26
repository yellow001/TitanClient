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
	[ActionCategory("iTween")]
	[Tooltip("Pause an iTween action.")]
	public class iTweenPause : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		public iTweenFSMType iTweenType = iTweenFSMType.all;
		public bool includeChildren;
		public bool inScene;
		
		public override void Reset()
		{
			iTweenType = iTweenFSMType.all;
			includeChildren = false;
			inScene = false;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			DoiTween();
			Finish();
		}
							
		void DoiTween()
		{
			if(iTweenType == iTweenFSMType.all){
				iTween.Pause();
			} else {
				if(inScene) {
					iTween.Pause(System.Enum.GetName(typeof(iTweenFSMType), iTweenType));
				} else {
					GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
					if (go == null) return;
					iTween.Pause(go, System.Enum.GetName(typeof(iTweenFSMType), iTweenType), includeChildren);
				}
			}
		}
	}
}
