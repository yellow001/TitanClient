using NetFrame;
using ServerSimple.Data;
using ServerSimple.DTO.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetCtrlSender : MonoBehaviour {
    public int modelID;

    float boneRoX;

    //float mHorizontal, mVertical;

    //bool RMB, LMB;

    float deltaTime = 0;

    float intervalTime = 0.25f;
    // Use this for initialization
    void Start() {
        //InputMgr.Ins.InputChange += SendMoveMessage;
        InputMgr.Ins.PositionChange += SendMoveMessage;

        InputMgr.Ins.LMBChange += SendLMBMessage;

        InputMgr.Ins.RMBChange += SendRMBMessage;

        InputMgr.Ins.RotateY += SendRotateYMessage;

        this.AddEventFun("BoneRotation", (args) => {
            boneRoX = (float)args[0];
        });

        this.AddEventFun("GunFire",GunFire);

        this.AddObjEventFun(gameObject, FightEvent.Kill.ToString(), (args) => {
            Clear();
            this.enabled = false;
        });

        this.AddEventFun(FightEvent.Over.ToString(),(args)=> {
            Clear();
            this.enabled = false;
        });
    }

    private void OnDestroy() {
        Clear();
    }

    void Clear() {
        InputMgr.Ins.PositionChange -= SendMoveMessage;

        InputMgr.Ins.LMBChange -= SendLMBMessage;

        InputMgr.Ins.RMBChange -= SendRMBMessage;

        InputMgr.Ins.RotateY -= SendRotateYMessage;

        this.RemoveEventFun("GunFire", GunFire);
    }

    #region 发送数据

    /// <summary>
    /// 移动数据
    /// </summary>
    void SendMoveMessage() {

        if (deltaTime < intervalTime) {
            deltaTime += Time.deltaTime;
            return;
        }

        deltaTime = 0;

        MoveDataDTO dto = new MoveDataDTO();

        dto.modelID = modelID;

        //dto.Horizontal = InputMgr.Ins.Horizontal;
        //dto.Vertical = InputMgr.Ins.Vertical;
        //dto.RMB = InputMgr.Ins.RMB;
        //dto.LMB = InputMgr.Ins.RMB && InputMgr.Ins.LMB;

        //if (dto.RMB) {
        //    boneRoX = boneRoX;
        //}
        //else {
        //    dto.boneRoX = 999;
        //}

        dto.RotationY = transform.eulerAngles.y;

        dto.Position = new Vector3Ex(transform.position);

        //bool move = true;
        //if ((dto.Horizontal == 0 && mHorizontal == 0) && (dto.Vertical == 0 && mVertical == 0)) {
        //    move = false;
        //}

        //mHorizontal = dto.Horizontal;
        //mVertical = dto.Vertical;

        //if (!move && !RMB) {
        //    //如果移动数据为0且右键不按下，就不发送数据
        //    return;
        //}

        //if (dto.Horizontal == 0 && dto.Vertical == 0 && !dto.RMB) {
        //    return;
        //}

        FightCtrl.Ins.MoveCREQ(dto);

        //TransModel model = new TransModel(1003005);
        //model.SetMsg(dto);

        //MessageHandler.Send(model);
    }

    void SendLMBMessage() {
        MouseBtnDTO dto = new MouseBtnDTO();
        dto.modelID = modelID;
        dto.btn = InputMgr.Ins.LMB;
        FightCtrl.Ins.LMBCREQ(dto);
    }

    void SendRMBMessage() {
        MouseBtnDTO dto = new MouseBtnDTO();
        dto.modelID = modelID;
        dto.btn = InputMgr.Ins.RMB;
        FightCtrl.Ins.RMBCREQ(dto);
    }

    void SendRotateYMessage() {
        if (InputMgr.Ins.Horizontal == 0 && InputMgr.Ins.Vertical == 0) {
            RotateYDTO dto2 = new RotateYDTO();
            dto2.modelID = modelID;
            dto2.rotateY = transform.eulerAngles.y;
            FightCtrl.Ins.RotateYCREQ(dto2);
        }
    }

    /// <summary>
    /// 开火
    /// </summary>
    /// <param name="args"></param>
    void GunFire(object[] args) {
        ShootDTO dto = new ShootDTO();

        dto.modelID = modelID;

        Vector3 v = (Vector3)args[0];

        dto.BulletPos = new Vector3Ex(v.x, v.y, v.z);

        Vector3 v2 = (Vector3)args[1];

        dto.BulletRotate = new Vector3Ex(v2.x, v2.y, v2.z);

        FightCtrl.Ins.ShootCREQ(dto);

        //TransModel model = new TransModel(1003007);
        //model.SetMsg(dto);

        //MessageHandler.Send(model);
    }
    #endregion
}
