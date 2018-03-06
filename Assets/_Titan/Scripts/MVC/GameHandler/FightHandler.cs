using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightHandler : MonoBehaviour {
    FightModel fightModel;
	// Use this for initialization
	void Start () {
        //Debug.Log(FightCtrl.Ins.model.fightRoom);

        fightModel = FightCtrl.Ins.model;

        InitFightData();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitFightData() {
        //初始化模型  当是玩家自己时，加上 实时控制 脚本，其余加上 同步控制 脚本


        //发送初始化完成
        FightCtrl.Ins.InitCompletedCREQ();
    }
}
