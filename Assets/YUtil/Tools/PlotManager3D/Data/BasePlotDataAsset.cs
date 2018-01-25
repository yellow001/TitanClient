using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlotDataAsset",menuName ="YUtil/Plot Asset/Base Plot Data Asset",order =1)]
[System.Serializable]
public class BasePlotDataAsset: ScriptableObject{
    public List<BasePlotData> datas=new List<BasePlotData>();
    public AudioClip BGM;
    public Dictionary<string, BasePlotDataAsset> childAssetDic;
    public BasePlotDataAsset parentAsset;
    public List<BasePlotDataAsset> childAssets=new List<BasePlotDataAsset>();
    public List<string> selString = new List<string>();
    /// <summary>
    /// 当前对话下标
    /// </summary>
    public int currentIndex { get; set; } = 0;

    public BasePlotData GetPlotData() {
        if (datas.Count > currentIndex) {
            return datas[currentIndex++];
        }
        else {
            return null;
        }
    }

    public BasePlotDataAsset GetChildData(string key) {

        if (childAssetDic == null) {
            Init();
        }

        if (childAssetDic.ContainsKey(key)) {
            return childAssetDic[key];
        }

        return null;
    }

    void AddChildAsset(string key,BasePlotDataAsset asset) {
        if (childAssetDic.ContainsKey(key)) {
            Debug.Log("分支名重复，将忽略 "+key);
            return;
        }
        childAssetDic.Add(key, asset);
        asset.parentAsset = this;
    }

    void Init() {
        childAssetDic = new Dictionary<string, BasePlotDataAsset>();
        for (int i = 0; i < childAssets.Count; i++) {
            AddChildAsset(selString[i], childAssets[i]);
        }
    }
}
