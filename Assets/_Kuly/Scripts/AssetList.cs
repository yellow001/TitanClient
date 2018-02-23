using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetList", menuName = "YUtil/AssetList", order = 1)]
[System.Serializable]
public class AssetList : ScriptableObject {
    public List<string> list;
}
