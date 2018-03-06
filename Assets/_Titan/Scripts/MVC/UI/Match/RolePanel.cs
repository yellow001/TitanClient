using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ServerSimple.DTO.Login;

public class RolePanel : BaseUI {
    [HideInInspector]
    public Image headImg;
    [HideInInspector]
    public Text nameTx;
    [HideInInspector]
    public RawImage renderImg;
    [HideInInspector]
    public Button changeBtn;
    [HideInInspector]
    public Transform tra;

    UserModel userModel;

    public string headImgPath = "head";

    string headFullPath;

    public GameObject KulyPrefab;

    public Transform renderPos;

    public string renderLayer="render";

    GameObject kulyObj;
    new void OnEnable() {
        base.OnEnable();
    }

    public override void Init() {

        tra = transform;
        headImg = tra.Find("name/head/headImg").GetComponent<Image>();
        nameTx = tra.Find("name/nameTx").GetComponent<Text>();
        renderImg = tra.Find("renderImg").GetComponent<RawImage>();
        changeBtn = tra.Find("renderImg/changeBtn").GetComponent<Button>();

        //todo 生成角色 rendertexture
        CreateRoleRenderTexture();

        base.Init();
    }

    public override void AddEvent() {
        
        //changeBtn.onClick.AddListener();
        userModel = LoginCtrl.Ins.model;

        userModel.BindEvent(UserEvent.RefreshUserModel, (args) => { UpdateView(); });
    }

    public override void UpdateView() {
        base.UpdateView();

        nameTx.text=userModel.GetUserName();

        UserData data = userModel.GetUserData();

        string p = headImgPath + "/" + data.headID;

        if (!p.Equals(headFullPath)) {
            headFullPath = p;
            headImg.sprite = ResMgr.Ins.GetResAsset<Sprite>(headFullPath);
        }
    }

    public override void CloseAni() {
        base.CloseAni();
        //@CloseAni
    }

    public override void OpenAni() {
        base.OpenAni();
        //@OpenAni
    }

    void CreateRoleRenderTexture() {
        kulyObj = Instantiate(KulyPrefab) as GameObject;
        kulyObj.transform.SetParent(renderPos, false);
        kulyObj.GetComponentInChildren<Rigidbody>().useGravity = false;

        kulyObj.GetComponentInChildren<KulyAssetLoader>().SetCallBack(() => {
            SetObjLayer(kulyObj, LayerMask.NameToLayer(renderLayer));
        });

        kulyObj.GetComponentInChildren<KulyAssetLoader>().RefreshKulyData();
    }

    void SetObjLayer(GameObject obj,int layer) {
        obj.layer = layer;
        for (int i = 0; i < obj.transform.childCount; i++) {
            int index = i;
            SetObjLayer(obj.transform.GetChild(i).gameObject, layer);
        }
    }
}
