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
	[Tooltip("Applies a jolt of force to a GameObject's scale and wobbles it back to its initial scale.")]
	public class iTweenPunchScale : iTweenFsmAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;
		
		[RequiredField]
		[Tooltip("A vector punch range.")]
		public FsmVector3 vector;
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType = iTween.LoopType.none;
						
		public override void Reset()
		{
			base.Reset();
			id = new FsmString{UseVariable = true};
			time = 1f;
			delay = 0f;
			loopType = iTween.LoopType.none;
			vector = new FsmVector3 { UseVariable = true };
		}

		public override void OnEnter()
		{
			base.OnEnteriTween(gameObject);
			if(loopType != iTween.LoopType.none) base.IsLoop(true);
			DoiTween();	
		}
		
		public override void OnExit(){
			base.OnExitiTween(gameObject);
		}
		
		void DoiTween()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			// init position
			
			Vector3 amount = Vector3.zero;
			if (vector.IsNone) { 
				
			} else {
				amount = vector.Value;
			}
			
			itweenType = "punch";
			iTween.PunchScale(go, iTween.Hash(
			                              "amount", amount,
			                              "name", id.IsNone ? "" : id.Value,    
			                              "time", time.IsNone ? 1f : time.Value,
			                              "delay", delay.IsNone ? 0f : delay.Value,
			                              "looptype", loopType,
			                              "oncomplete", "iTweenOnComplete",
			                              "oncompleteparams", itweenID,
			                              "onstart", "iTweenOnStart",
			                              "onstartparams", itweenID,
			                              "ignoretimescale", realTime.IsNone ? false : realTime.Value  
			                              ));
			}
	}
}