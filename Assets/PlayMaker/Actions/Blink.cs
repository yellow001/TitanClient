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
	[ActionCategory(ActionCategory.Effects)]
	[Tooltip("Turns a Game Object on/off in a regular repeating pattern.")]
	public class Blink : ComponentAction<Renderer>
	{
		[RequiredField]
        [Tooltip("The GameObject to blink on/off.")]
		public FsmOwnerDefault gameObject;

		[HasFloatSlider(0, 5)]
        [Tooltip("Time to stay off in seconds.")]
		public FsmFloat timeOff;
		
        [HasFloatSlider(0, 5)]
        [Tooltip("Time to stay on in seconds.")]
        public FsmFloat timeOn;
		
        [Tooltip("Should the object start in the active/visible state?")]
		public FsmBool startOn;

        [Tooltip("Only effect the renderer, keeping other components active.")]
		public bool rendererOnly;
		
        [Tooltip("Ignore TimeScale. Useful if the game is paused.")]
		public bool realTime;
		
		private float startTime;
		private float timer;
		private bool blinkOn;
		
		public override void Reset()
		{
			gameObject = null;
			timeOff = 0.5f;
			timeOn = 0.5f;
			rendererOnly = true;
			startOn = false;
			realTime = false;
		}
	
		public override void OnEnter()
		{
			startTime = FsmTime.RealtimeSinceStartup;
			timer = 0f;
			
			UpdateBlinkState(startOn.Value);
		}
		
		public override void OnUpdate()
		{
			// update time
			
			if (realTime)
			{
				timer = FsmTime.RealtimeSinceStartup - startTime;
			}
			else
			{
				timer += Time.deltaTime;
			}
			
			// update blink
			
			if (blinkOn && timer > timeOn.Value)
			{
				UpdateBlinkState(false);
			}
			
			if (blinkOn == false && timer > timeOff.Value)
			{
				UpdateBlinkState(true);
			}
		}
			
		void UpdateBlinkState(bool state)
		{
			var go = gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value;
		    if (go == null)
		    {
		        return;
		    }

			if (rendererOnly)
			{
                if(UpdateCache(go))
			    {
			        renderer.enabled = state;
			    }
			}
			else
            {
#if UNITY_3_5 || UNITY_3_4
                go.active = state;
#else          
                go.SetActive(state);
#endif
            }
			
			blinkOn = state;
			
			// reset timer
			
			startTime = FsmTime.RealtimeSinceStartup;
			timer = 0f;
		}
	}
}

