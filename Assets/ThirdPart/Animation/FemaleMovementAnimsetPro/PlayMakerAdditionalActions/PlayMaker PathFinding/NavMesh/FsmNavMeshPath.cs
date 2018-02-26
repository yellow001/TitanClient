/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
//
// TODO: implement FsmNavMeshPath properly in NavMeshCalculatePath and NaMeshCalculatePathBetweenGameObject.
// this is currently very much under progress, not sure if this is the right way to go about this. maybe too advanced and should be left to user to implement this?

using UnityEngine;
using System.Collections;

public class FsmNavMeshPath : MonoBehaviour {
	
	//Corner points of path
	public Vector3[] corners;
	/*
	{
		get { 
			if (path== null)
			{
			 return null;
			}
			return path.corners;
		}
	}
	*/
	
	public UnityEngine.AI.NavMeshPathStatus status
	{
		get
		{ 
			if (path== null)
			{
			 return UnityEngine.AI.NavMeshPathStatus.PathInvalid;
			}	
		return path.status;
		}
	}

	public UnityEngine.AI.NavMeshPath path;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void ClearCorners()
	{
		path.ClearCorners();
	}
	
	public string GetStatusString()
	{
		if (path ==null){
			return "n/a";
		}else{
			return path.status.ToString();
		}
	}
	
}
