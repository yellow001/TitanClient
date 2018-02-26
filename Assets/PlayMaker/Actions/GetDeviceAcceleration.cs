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
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Gets the last measured linear acceleration of a device and stores it in a Vector3 Variable.")]
	public class GetDeviceAcceleration : FsmStateAction
	{
		// TODO: Figure out some nice mapping options for common use cases.
/*		public enum MappingOptions
		{
			Flat,
			Vertical
		}
		
		[Tooltip("Flat is god for marble rolling games, vertical is good for Doodle Jump type games.")]
		public MappingOptions mappingOptions;
*/

		[UIHint(UIHint.Variable)]
		public FsmVector3 storeVector;
		[UIHint(UIHint.Variable)]
		public FsmFloat storeX;
		[UIHint(UIHint.Variable)]
		public FsmFloat storeY;
		[UIHint(UIHint.Variable)]
		public FsmFloat storeZ;
		public FsmFloat multiplier;
		public bool everyFrame;
		
		public override void Reset()
		{
			storeVector = null;
			storeX = null;
			storeY = null;
			storeZ = null;
			multiplier = 1;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoGetDeviceAcceleration();
			
			if (!everyFrame)
				Finish();
		}
		

		public override void OnUpdate()
		{
			DoGetDeviceAcceleration();
		}
		
		void DoGetDeviceAcceleration()
		{
/*			var dir = Vector3.zero;
			
			switch (mappingOptions) 
			{
			case MappingOptions.Flat:
				
				dir.x = Input.acceleration.x;
				dir.y = Input.acceleration.z;
				dir.z = Input.acceleration.y;
				break;
					
				
			case MappingOptions.Vertical:
				dir.x = Input.acceleration.x;
				dir.y = Input.acceleration.y;
				dir.z = Input.acceleration.x;
				break;
			}
*/
			var dir = new Vector3(Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
			
			if (!multiplier.IsNone)
			{
				dir *= multiplier.Value;
			}
			
			storeVector.Value = dir;
			storeX.Value = dir.x;
			storeY.Value = dir.y;
			storeZ.Value = dir.z;
		}
		
	}
}
