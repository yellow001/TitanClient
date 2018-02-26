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
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Gets info on the last Trigger 2d event and store in variables.  See Unity and PlayMaker docs on Unity 2D physics.")]
	public class GetTrigger2dInfo : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the GameObject hit.")]
		public FsmGameObject gameObjectHit;

		[UIHint(UIHint.Variable)]
		[Tooltip("The number of separate shaped regions in the collider.")]
		public FsmInt shapeCount;
	
		[UIHint(UIHint.Variable)]
		[Tooltip("Useful for triggering different effects. Audio, particles...")]
		public FsmString physics2dMaterialName;
		
		public override void Reset()
		{
			gameObjectHit = null;
			shapeCount = null;
			physics2dMaterialName = null;
		}
		
		void StoreTriggerInfo()
		{
            if (Fsm.TriggerCollider2D == null) return;

            gameObjectHit.Value = Fsm.TriggerCollider2D.gameObject;
            shapeCount.Value = Fsm.TriggerCollider2D.shapeCount;
            physics2dMaterialName.Value = Fsm.TriggerCollider2D.sharedMaterial != null ? Fsm.TriggerCollider2D.sharedMaterial.name : "";
		}
		
		public override void OnEnter()
		{
			StoreTriggerInfo();
			
			Finish();
		}
	}
}
