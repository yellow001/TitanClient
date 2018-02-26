/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

public class iTweenFSMEvents : MonoBehaviour {
	static public int itweenIDCount = 0;
	public int itweenID = 0;
	public iTweenFsmAction itweenFSMAction = null;
	public bool donotfinish = false;
	public bool islooping = false;
	
	void iTweenOnStart(int aniTweenID){
		if(itweenID == aniTweenID){
			itweenFSMAction.Fsm.Event(itweenFSMAction.startEvent);
		}
	}
	
	void iTweenOnComplete(int aniTweenID){
		if(itweenID == aniTweenID) {
			if(islooping) {
				if(!donotfinish){
					itweenFSMAction.Fsm.Event(itweenFSMAction.finishEvent);
					itweenFSMAction.Finish();	
				}
			} else {
				itweenFSMAction.Fsm.Event(itweenFSMAction.finishEvent);
				itweenFSMAction.Finish();
			}
		}
	}
}

public enum iTweenFSMType{
	all,
	move,
	rotate,
	scale,
	shake,
	position,
	value,
	look
}
