using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KulyAssetLoadTest : MonoBehaviour {

    public KulyClothCtrl clothCtrl;

    public KulyHairCtrl hairCtrl;

	// Use this for initialization
	void Start () {
        if (hairCtrl != null)
            hairCtrl.RefreshHair("hair0");

        if (clothCtrl!=null)
            clothCtrl.RefreshCloth("cloth0", new string[] { "#000000", "#ffffff" });
	}
	
}
