using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResMgr : BaseManager<ResMgr> {
    /// <summary>
    /// 已经加载的资源
    /// </summary>
    public static Dictionary<string, AssetBundle> loadedAssets;

    /// <summary>
    /// 某资源对应的依赖项
    /// </summary>
    public static Dictionary<string, string[]> assetDepends;

    public static Dictionary<string, Object> resAssets;

    static bool isInit = false;

    public new void Init() {

        if (isInit) { return; }

        loadedAssets = new Dictionary<string, AssetBundle>();
        assetDepends = new Dictionary<string, string[]>();

        //load manifest
        AssetBundle manifestFile = AssetBundle.LoadFromFile(AppConst.ResPath + "/StreamingAssets");
        AssetBundleManifest manifest = manifestFile.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        string[] abs = manifest.GetAllAssetBundles();

        foreach (string item in abs) {
            assetDepends.Add(item, manifest.GetAllDependencies(item));
        }
        isInit = true;
    }

    public AssetBundle loadAssetBundle(string abName) {

        if (!isInit) { Init(); }

        //判断该资源是否存在
        string path = getRealPath(abName + AppConst.AssetEx);
        if (path == string.Empty) {
            Debug.Log("the assetbundle (" + abName + ") you want to load is not exit");
            return null;
        }

        //判断资源是否已经加载
        if (loadedAssets.ContainsKey(path)) { return loadedAssets[path]; }

        //加载资源并返回 ab
        //先加载依赖项
        foreach (string item in assetDepends[path]) {
            if (loadedAssets.ContainsKey(item)) continue;

            //todo 应该检查该依赖项是否还有依赖项（额。。。不知道要不要）
            AssetBundle ab = AssetBundle.LoadFromFile(AppConst.ResPath + "/" + item);
            loadedAssets.Add(item, ab);
        }

        //加载该 ab
        AssetBundle asset = AssetBundle.LoadFromFile(AppConst.ResPath + "/" + path);
        loadedAssets.Add(path, asset);
        return asset;

    }

    public T getAsset<T>(string abName, string assetName) where T : Object {
        AssetBundle ab = loadAssetBundle(abName);

        //foreach (string item in ab.GetAllAssetNames()) {
        //    Debug.Log(item);
        //}

        if (ab != null && ab.Contains(assetName)) {
            return ab.LoadAsset<T>(assetName);
        }

        //Debug.Log("the asset you want to load is null");
        return null;
    }

    public string getRealPath(string path) {

        path = path.ToLower();

        foreach (string item in assetDepends.Keys) {
            if (item.Contains(path)) {
                return item;
            }
        }

        return string.Empty;
    }

    public T GetResAsset<T>(string path) where T : Object {
        if (resAssets == null) {
            resAssets = new Dictionary<string, Object>();
        }

        if (resAssets.ContainsKey(path)) {
            return (T)resAssets[path];
        }

        T value = Resources.Load<T>(path);

        resAssets.Add(path, value);

        return value;
    }


    public string getLua(string abName, string sname) {
        if (!AppConst.LuaAssetMode) {
            Debug.Log("当前加载lua为调试模式，将从文件夹路径加载lua脚本，发布时请修改 AppConst.cs 中的 LuaAssetMode 为 true,并打包资源");
            StreamReader sr = File.OpenText(AppConst.LuaPath + sname + ".lua");
            string code = sr.ReadToEnd();
            sr.Close();
            return code;
        }
        else {
            Debug.Log("当前加载lua为资源模式，将从打包资源中加载lua脚本，找不到路径文件时请先打包文件，或修改 AppConst.cs 中的 LuaAssetMode 为 false");
            return getAsset<TextAsset>(abName, sname).text;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="unLoadAllObjs"></param>
    public void UnLoadAssetBundle(string abName,bool unLoadAllObjs=false) {
    }
}
