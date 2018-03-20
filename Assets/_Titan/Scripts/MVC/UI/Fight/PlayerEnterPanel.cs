using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using ServerSimple.DTO.Login;
using System.Collections.Generic;
using System.Linq;

public class PlayerEnterPanel : BaseUI {

	[HideInInspector]
	public Image root;
	[HideInInspector]
	public Transform tra;

    public GameObject grid;

    public UserEnterItem enterItem;

    List<UserEnterItem> itemList=new List<UserEnterItem>();

    FightModel fightModel;
    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init()
    {

		tra=transform;
		root=GetComponent <Image>();

        base.Init();
    }

    public override void AddEvent() {
        fightModel = FightCtrl.Ins.model;

        fightModel.BindEvent(FightEvent.InitCompleted, (args) => { UpdateCompleteList((Dictionary<int, UserDTO>)args[1]); });
    }

    void UpdateCompleteList(Dictionary<int, UserDTO> dtoDic) {

        List<UserDTO> dtos = dtoDic.Values.ToList();

        //throw new NotImplementedException();
        //刷新完成列表

        //更新列表项数目（多减少补）
        int count = dtos.Count - itemList.Count;
        if (count > 0) {
            while (count > 0) {
                UserEnterItem info = Instantiate(enterItem.gameObject, grid.transform, false).GetComponent<UserEnterItem>();
                itemList.Add(info);
                count--;
            }
        }
        else if (count < 0) {
            while (count < 0) {
                UserEnterItem info = itemList[itemList.Count - 1];
                itemList.Remove(info);
                info.CloseAni();
                count++;
            }
        }


        //更新信息
        for (int i = 0; i < dtos.Count; i++) {
            itemList[i].currentDTO = dtos[i];
            itemList[i].UpdateView();
        }
    }

    public override void UpdateView()
    {
        base.UpdateView();
        
		//root.sprite=null;

    }

    public override void CloseAni()
    {
        base.CloseAni();
//@CloseAni
    }

    public override void OpenAni()
    {
        base.OpenAni();
//@OpenAni
    }
}
