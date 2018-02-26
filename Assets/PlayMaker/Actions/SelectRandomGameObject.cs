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
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Selects a Random Game Object from an array of Game Objects.")]
	public class SelectRandomGameObject : FsmStateAction
	{
		[CompoundArray("Game Objects", "Game Object", "Weight")]
		public FsmGameObject[] gameObjects;
		[HasFloatSlider(0, 1)]
		public FsmFloat[] weights;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeGameObject;
		
		public override void Reset ()
		{
			gameObjects = new FsmGameObject[3];
			weights = new FsmFloat[] {1,1,1};
			storeGameObject = null;
		}
		
		public override void OnEnter ()
		{
			DoSelectRandomGameObject();
			Finish();
		}
		
		void DoSelectRandomGameObject()
		{
			if (gameObjects == null) return;
			if (gameObjects.Length == 0) return;
			if (storeGameObject == null) return;

			int randomIndex = ActionHelpers.GetRandomWeightedIndex(weights);
			
			if (randomIndex != -1)
			{
				storeGameObject.Value = gameObjects[randomIndex].Value;
			}
			
		}
	}
}
