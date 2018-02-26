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
	[Tooltip("Gets a Random Game Object from the scene.\nOptionally filter by Tag.")]
	public class GetRandomObject : FsmStateAction
	{
		[UIHint(UIHint.Tag)]
		public FsmString withTag;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmGameObject storeResult;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			withTag = "Untagged";
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetRandomObject();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetRandomObject();
		}

		void DoGetRandomObject()
		{
			GameObject[] gameObjects;

			if (withTag.Value != "Untagged")
			{
				gameObjects = GameObject.FindGameObjectsWithTag(withTag.Value);
			}
			else
			{
                gameObjects = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));
			}

			if (gameObjects.Length > 0)
			{
				storeResult.Value = gameObjects[Random.Range(0, gameObjects.Length)];
				return;
			}

			storeResult.Value = null;
		}
	}
}
