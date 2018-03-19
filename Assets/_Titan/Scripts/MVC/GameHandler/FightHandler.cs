using NetFrame;
using ServerSimple.DTO.Fight;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightHandler : MonoBehaviour {
    FightModel fightModel;

    UserModel userModel;

    Dictionary<int, GameObject> IdToObjDic = new Dictionary<int, GameObject>();

    public KulyCtrlRealTime kuly_user;

    public KulyCtrlSync kuly_sync;

    public KulyGunUser gun_user;

    public GameObject gun_player;

    public CameraController normalCam;

    int currentID;

    public int height = 300;

	// Use this for initialization
	void Start () {
        //Debug.Log(FightCtrl.Ins.model.fightRoom);

        userModel = LoginCtrl.Ins.model;

        fightModel = FightCtrl.Ins.model;

        AddEvent();

        InitFightData();
	}

    void AddEvent() {
        this.AddEventFun(FightEvent.AllInitCompleted.ToString(), (args) => AllCompleted(args));

        fightModel.BindEvent(FightEvent.Move, (args) => MoveBRO(args));
        fightModel.BindEvent(FightEvent.Shoot, (args) => ShootBRO(args));
        fightModel.BindEvent(FightEvent.Damage, (args) => DamageBRO(args));
    }

    private void DamageBRO(object[] args) {
        TransModel model = (TransModel)args[0];
        DamageDTO dto = model.GetMsg<DamageDTO>();

        if (IdToObjDic.ContainsKey(dto.DstID)) {
            if (dto.DstID == currentID) {
                //受伤害的是自己时刷新血量
            }
        }
    }

    private void ShootBRO(object[] args) {
        TransModel model = (TransModel)args[0];
        ShootDTO dto = model.GetMsg<ShootDTO>();

        if (IdToObjDic.ContainsKey(dto.modelID)) {
            //IdToObjDic[dto.modelID].GetComponentInChildren<NetCtrlReceiver>().shootDto = dto;

            //不是玩家自身时发射子弹
            if (currentID != dto.modelID) {
                //生成子弹（只是用于显示的而已）
            }
        }
    }

    private void MoveBRO(object[] args) {
        TransModel model = (TransModel)args[0];

        MoveDataDTO dto = model.GetMsg<MoveDataDTO>();

        if (IdToObjDic.ContainsKey(dto.modelID)) {
            IdToObjDic[dto.modelID].GetComponentInChildren<NetCtrlReceiver>().movData = dto;
        }
    }



    // Update is called once per frame
    void Update () {
		
	}

    void InitFightData() {
        //初始化模型  当是玩家自己时，加上 实时控制 脚本，其余加上 同步控制 脚本
        foreach (var item in fightModel.fightRoom.baseModelDic) {
            GameObject ga;
            if (item.Key == fightModel.fightRoom.nameToModelID[userModel.GetUserName()]) {
                //自己的
                ga = InitPlayer(item);
            }
            else {
                //其他人的
                ga = InitUser(item);
            }

            IdToObjDic.Add(item.Key, ga);

        }

        //发送初始化完成
        FightCtrl.Ins.InitCompletedCREQ();
    }

    GameObject InitPlayer(KeyValuePair<int,ServerSimple.DTO.Fight.BaseModel> item) {
        GameObject ga;
        ga = Instantiate(kuly_user.gameObject);

        Vector3 pos = item.Value.position.ToVec3();

        pos.y = height;

        RaycastHit hit;
        if (Physics.Raycast(pos, Vector3.down, out hit, LayerMask.NameToLayer("Ground"))){
            pos.y = hit.point.y;
        }

        ga.transform.position = pos;

        currentID = item.Key;
        ga.GetComponentInChildren<NetCtrlSender>().modelID = item.Key;

        GameObject gunObj = Instantiate(gun_user.gameObject);

        KulyCtrlRealTime ctrl = ga.GetComponentInChildren<KulyCtrlRealTime>();

        ctrl.gun = gunObj;

        ga.GetComponentInChildren<AimWeaponCam>().gun = gunObj.GetComponentInChildren<KulyGunUser>();

        normalCam.target = ga.transform;
        normalCam.rotationSpace = ga.transform;

        normalCam.GetComponent<ProtectCam>().m_Pivot = ga.transform.Find("ProtectCam");

        //gunObj.GetComponent<KulyGunUser>().enabled = false;

        //ctrl.enabled = false;
        ga.SetActive(false);

        return ga;
    }

    GameObject InitUser(KeyValuePair<int, ServerSimple.DTO.Fight.BaseModel> item) {
        GameObject ga;
        ga = Instantiate(kuly_user.gameObject);

        Vector3 pos = item.Value.position.ToVec3();

        pos.y = height;

        RaycastHit hit;
        if (Physics.Raycast(pos, Vector3.down, out hit, LayerMask.NameToLayer("Ground"))) {
            pos.y = hit.point.y;
        }

        ga.transform.position = pos;

        GameObject gunObj = Instantiate(gun_player);

        KulyCtrlSync ctrl = ga.GetComponentInChildren<KulyCtrlSync>();

        ctrl.gun = gunObj;

        //gunObj.GetComponent<KulyGunUser>().enabled = false;

        //ctrl.enabled = false;
        ga.SetActive(false);

        return ga;
    }

    /// <summary>
    /// 所有人加载完毕，激活控制脚本
    /// </summary>
    /// <param name="args"></param>
    void AllCompleted(object[] args) {
        //throw new NotImplementedException();
        foreach (var item in IdToObjDic) {
            //GameObject ga=item.Value;
            //if (item.Key == currentID) {
            //    ga.GetComponentInChildren<KulyCtrlRealTime>().enabled = true;
            //    ga.GetComponentInChildren<KulyCtrlRealTime>().gun.GetComponentInChildren<KulyGunUser>().enabled = true;
            //}
            //else {
            //    ga.GetComponentInChildren<KulyCtrlSync>().enabled = true;
            //    ga.GetComponentInChildren<KulyCtrlSync>().gun.GetComponentInChildren<KulyGunUser>().enabled = true;
            //}
            item.Value.SetActive(true);
        }

        normalCam.enabled = true;

        normalCam.GetComponent<ProtectCam>().enabled = true;
    }
}
