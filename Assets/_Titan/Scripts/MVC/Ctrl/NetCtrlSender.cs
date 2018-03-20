using NetFrame;
using ServerSimple.Data;
using ServerSimple.DTO.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetCtrlSender : MonoBehaviour {
    public int modelID;

    float boneRoX;
	// Use this for initialization
	void Start () {
        InputMgr.Ins.InputChange += SendCtrlMessage;
        this.AddEventFun("BoneRotation", (args) => {
            boneRoX = (float)args[0];
        });

        this.AddEventFun("GunFire", (args) => GunFire(args));
    }

    #region 发送数据
    void SendCtrlMessage() {
        MoveDataDTO dto = new MoveDataDTO();

        dto.modelID = modelID;

        dto.Horizontal = InputMgr.Ins.Horizontal;
        dto.Vertical = InputMgr.Ins.Vertical;
        dto.RMB = InputMgr.Ins.RMB;
        dto.LMB = InputMgr.Ins.RMB && InputMgr.Ins.LMB;

        if (dto.RMB) {
            dto.boneRoX = boneRoX;
        }
        else {
            dto.boneRoX = 999;
        }

        dto.Rotation = new Vector3Ex(transform.eulerAngles);

        if (dto.Horizontal == 0 && dto.Vertical == 0 && !dto.RMB) {
            return;
        }

        FightCtrl.Ins.MoveCREQ(dto);

        //TransModel model = new TransModel(1003005);
        //model.SetMsg(dto);

        //MessageHandler.Send(model);
    }


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
