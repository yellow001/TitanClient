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
	[Tooltip("CSimilar to ScaleTo but incredibly less expensive for usage inside the Update function or similar looping situations involving a 'live' set of changing values. Does not utilize an EaseType.")]
	public class iTweenScaleUpdate: FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		[Tooltip("Scale To a transform scale.")]
		public FsmGameObject transformScale;
		[Tooltip("A scale vector the GameObject will animate To.")]
		public FsmVector3 vectorScale;
		[Tooltip("The time in seconds the animation will take to complete. If transformScale is set, this is used as an offset.")]
		public FsmFloat time;
		
		System.Collections.Hashtable hash;			
		GameObject go;
		
		public override void Reset()
		{
			transformScale = new FsmGameObject { UseVariable = true};
			vectorScale = new FsmVector3 { UseVariable = true };
			time = 1f;
		}

		public override void OnEnter()
		{
			hash = new System.Collections.Hashtable();
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) {
				Finish();
				return;
			}
			if(transformScale.IsNone){
				hash.Add("scale", vectorScale.IsNone ? Vector3.zero : vectorScale.Value);
			} else {
				if(vectorScale.IsNone){
					hash.Add("scale", transformScale.Value.transform);
				} else {
					hash.Add("scale", transformScale.Value.transform.localScale + vectorScale.Value);
				}
				
			}
			hash.Add("time", time.IsNone ? 1f : time.Value);
			DoiTween();
		}
		
		public override void OnExit(){
			
		}
		
		public override void OnUpdate(){
			hash.Remove("scale");
			if(transformScale.IsNone){
				hash.Add("scale", vectorScale.IsNone ? Vector3.zero : vectorScale.Value);
			} else {
				if(vectorScale.IsNone){
					hash.Add("scale", transformScale.Value.transform);
				} else {
					hash.Add("scale", transformScale.Value.transform.localScale + vectorScale.Value);
				}
				
			}
			DoiTween();	
		}

		void DoiTween()
		{
			iTween.ScaleUpdate(go, hash);
		}
	}
}
