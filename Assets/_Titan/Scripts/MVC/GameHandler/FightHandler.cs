using NetFrame;
using ServerSimple.DTO.Fight;
using ServerSimple.DTO.Login;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FightHandler : MonoBehaviour {
    FightModel fightModel;

    UserModel userModel;

    Dictionary<int, GameObject> IdToObjDic = new Dictionary<int, GameObject>();
    public Dictionary<GameObject,int> ObjToIdDic = new Dictionary< GameObject,int>();
    Dictionary<int, NetCtrlReceiver> IdToReceiveDic = new Dictionary<int, NetCtrlReceiver>();

    public KulyCtrlRealTime kuly_user;

    public KulyCtrlSync kuly_player;

    public KulyGunUser gun_user;

    public GameObject gun_player;

    public CameraController normalCam;

    public int currentID;

    public int height = 300;
    public string bulletPath;

    // Use this for initialization
    void Start() {
        //Debug.Log(FightCtrl.Ins.model.fightRoom);

        userModel = LoginCtrl.Ins.model;

        fightModel = FightCtrl.Ins.model;

        AddEvent();

        InitFightData();
    }

    void AddEvent() {
        this.AddEventFun(FightEvent.AllInitCompleted.ToString(),AllCompleted);

        fightModel.BindEvent(FightEvent.Move,MoveBRO);
        fightModel.BindEvent(FightEvent.Shoot,ShootBRO);
        fightModel.BindEvent(FightEvent.Damage,DamageBRO);
        fightModel.BindEvent(FightEvent.Kill,KillBRO);
    }

    private void OnDestroy() {
        this.RemoveEventFun(FightEvent.AllInitCompleted.ToString(),AllCompleted);

        fightModel.UnBindEvent(FightEvent.Move,MoveBRO);
        fightModel.UnBindEvent(FightEvent.Shoot,ShootBRO);
        fightModel.UnBindEvent(FightEvent.Damage,DamageBRO);
        fightModel.UnBindEvent(FightEvent.Kill,KillBRO);
    }

    private void DamageBRO(object[] args) {
        TransModel model = (TransModel)args[0];
        DamageDTO dto = model.GetMsg<DamageDTO>();

        if (IdToObjDic.ContainsKey(dto.DstID)) {
            if (dto.DstID == currentID) {
                //受伤害的是自己时刷新血量
                fightModel.currentModel.hp -= dto.DamageValue;
                fightModel.currentModel.hp = fightModel.currentModel.hp < 0 ? 0 : fightModel.currentModel.hp;
                this.CallEventList(FightEvent.DamageSelf.ToString());
            }
        }
    }

    private void KillBRO(object[] args) {
        DamageDTO dto = (DamageDTO)args[0];

        if (IdToObjDic.ContainsKey(dto.DstID)) {
            this.CallObjDeList(IdToObjDic[dto.DstID], FightEvent.Kill.ToString());
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
                GameObject obj = PoolMgr.Ins.GetResObj(bulletPath);
                obj.transform.position = dto.BulletPos.ToVec3();

                obj.transform.eulerAngles = dto.BulletRotate.ToVec3();

                //todo
                obj.GetComponent<Bullet>().ResetData();
            }
        }
    }

    private void MoveBRO(object[] args) {
        TransModel model = (TransModel)args[0];

        switch (model.area) {
            case 0:
                MoveDataDTO dto = model.GetMsg<MoveDataDTO>();

                if (dto.modelID != currentID && IdToReceiveDic.ContainsKey(dto.modelID)) {
                    IdToReceiveDic[dto.modelID].movData = dto;
                }
                break;
            case 1:
                MouseBtnDTO dto1 = model.GetMsg<MouseBtnDTO>();
                if (dto1.modelID != currentID && IdToReceiveDic.ContainsKey(dto1.modelID)) {
                    IdToReceiveDic[dto1.modelID].RMB = dto1.btn;
                }
                break;
            case 2:
                MouseBtnDTO dto2 = model.GetMsg<MouseBtnDTO>();
                if (dto2.modelID != currentID && IdToReceiveDic.ContainsKey(dto2.modelID)) {
                    IdToReceiveDic[dto2.modelID].LMB = dto2.btn;
                }
                break;
            case 3:
                RotateYDTO dto3 = model.GetMsg<RotateYDTO>();
                if (dto3.modelID != currentID && IdToReceiveDic.ContainsKey(dto3.modelID)) {
                    IdToReceiveDic[dto3.modelID].movData.RotationY = dto3.rotateY;
                }
                break;
            case 4:
                RotateYDTO dto4 = model.GetMsg<RotateYDTO>();
                if (dto4.modelID != currentID && IdToReceiveDic.ContainsKey(dto4.modelID)) {
                    IdToReceiveDic[dto4.modelID].boneRoX = dto4.rotateY;
                }
                break;
            default:
                break;
        }

    }



    // Update is called once per frame
    void Update() {

    }

    void InitFightData() {
        //初始化模型  当是玩家自己时，加上 实时控制 脚本，其余加上 同步控制 脚本
        foreach (var item in fightModel.fightRoom.baseModelDic) {
            GameObject ga;
            if (item.Key == fightModel.fightRoom.nameToModelID[userModel.GetUserName()]) {
                //自己的
                ga = InitUser(item);
            }
            else {
                //其他人的
                ga = InitPlayer(item);

            }

            IdToObjDic.Add(item.Key, ga);
            ObjToIdDic.Add(ga, item.Key);
        }

        //发送初始化完成
        FightCtrl.Ins.InitCompletedCREQ();
    }

    GameObject InitUser(KeyValuePair<int, ServerSimple.DTO.Fight.BaseModel> item) {
        GameObject ga;
        ga = Instantiate(kuly_user.gameObject);

        Vector3 pos = item.Value.position.ToVec3();

        pos.y = height;

        RaycastHit[] hits = Physics.RaycastAll(pos, Vector3.down, 500);
        foreach (var hit in hits) {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                pos.y = hit.point.y;
                break;
            }
        }

        ga.transform.position = pos;

        normalCam.transform.position = pos + Vector3.up;

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

    GameObject InitPlayer(KeyValuePair<int, ServerSimple.DTO.Fight.BaseModel> item) {
        GameObject ga;
        ga = Instantiate(kuly_player.gameObject);

        Vector3 pos = item.Value.position.ToVec3();

        pos.y = height;

        RaycastHit[] hits = Physics.RaycastAll(pos, Vector3.down, 500);
        foreach (var hit in hits) {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                pos.y = hit.point.y;
                break;
            }
        }

        ga.transform.position = pos;

        GameObject gunObj = Instantiate(gun_player);

        KulyCtrlSync ctrl = ga.GetComponentInChildren<KulyCtrlSync>();

        ctrl.gun = gunObj;

        IdToReceiveDic.Add(item.Key, ga.GetComponentInChildren<NetCtrlReceiver>());

        IdToReceiveDic[item.Key].movData.Position = new ServerSimple.Data.Vector3Ex(pos);

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

            UserDTO dto = fightModel.enterUserDTODic[item.Key];
            this.CallObjDeList(item.Value, UserEvent.RefreshUserModel.ToString(), dto);
        }

        normalCam.enabled = true;

        normalCam.GetComponent<ProtectCam>().enabled = true;

        this.CallEventList(FightEvent.InitPosPanel.ToString(), currentID, IdToObjDic);
    }
}
