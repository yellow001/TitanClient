using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResMgr : BaseManager<ResMgr> {

    public LoadMode loadmode = LoadMode.Resources;

    /// <summary>
    /// 已经加载的AB包
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

    /// <summary>
    /// 通过资源枚举查找资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <returns></returns>
    public T GetAsset<T>(string t) where T : Object {
        string path;
        switch (loadmode) {
            case LoadMode.Resources:

                path = ResPath.Ins.GetResPath(t);
                if (!string.IsNullOrEmpty(path)) {
                    return GetResAsset<T>(path);
                }
                return default(T);

            case LoadMode.AssetBundle:

                path = ResPath.Ins.GetABPath(t);
                if (!string.IsNullOrEmpty(path)) {
                    return GetABAsset<T>(path, t.ToString());
                }
                return default(T);

            default:
                return null;
        }
    }

    /// <summary>
    /// 通过resources获取资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 加载assetbundle包
    /// </summary>
    /// <param name="abName"></param>
    /// <returns></returns>
    public AssetBundle loadAssetBundle(string abName) {

        if (!isInit) { Init(); }

        //判断该资源是否存在
        string path = GetAbRealPath(abName);
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

    /// <summary>
    /// 获取ab包下的具体资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="abName"></param>
    /// <param name="assetName"></param>
    /// <returns></returns>
    public T GetABAsset<T>(string abName, string assetName) where T : Object {
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

    /// <summary>
    /// 通过资源枚举查找资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <returns></returns>
    public void GetAssetAsync<T>(string t,System.Action<T> callBack) where T : Object {
        string path;
        switch (loadmode) {
            case LoadMode.Resources:

                path = ResPath.Ins.GetResPath(t);
                if (!string.IsNullOrEmpty(path)) {
                    StartCoroutine(GetResAssetAsync(path,callBack));
                    return;
                }
                callBack(default(T));

                break;
            case LoadMode.AssetBundle:

                path = ResPath.Ins.GetABPath(t);
                if (!string.IsNullOrEmpty(path)) {
                    StartCoroutine(GetABAssetAsync<T>(path, t.ToString(),callBack));
                    return;
                }
                callBack(default(T));

                break;
            default:
                callBack(default(T));
                break;
        }
    }

    /// <summary>
    /// 通过resources异步获取资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public IEnumerator GetResAssetAsync<T>(string path,System.Action<T> callBack) where T : Object {
        if (resAssets == null) {
            resAssets = new Dictionary<string, Object>();
        }

        if (resAssets.ContainsKey(path)) {
            callBack((T)resAssets[path]);
            yield break;
        }

        ResourceRequest req = Resources.LoadAsync<T>(path);
        yield return req;

        if (req.asset != null) {
            resAssets.Add(path, req.asset);
            callBack((T)req.asset);
            yield break;
        }

        callBack(default(T));
        
    }

    /// <summary>
    /// 异步获取ab包下的具体资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="abName"></param>
    /// <param name="assetName"></param>
    /// <returns></returns>
    public IEnumerator GetABAssetAsync<T>(string abName, string assetName,System.Action<T> callBack) where T : Object {
        AssetBundle ab = loadAssetBundle(abName);

        if (ab != null && ab.Contains(assetName)) {
            AssetBundleRequest req = ab.LoadAssetAsync<T>(assetName);
            yield return req;

            if (req.asset != null) {
                callBack((T)req.asset);
                yield break;
            }
            callBack(default(T));
            yield break;
        }

        callBack(default(T));
    }

    /// <summary>
    /// 获取ab包的真正路径
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public string GetAbRealPath(string path) {

        path = path.ToLower();

        foreach (string item in assetDepends.Keys) {
            if (item.Contains(path)) {
                return item;
            }
        }

        return string.Empty;
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
            return GetABAsset<TextAsset>(abName, sname).text;
        }
    }

    /// <summary>
    /// 卸载资源
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="unLoadAllObjs"></param>
    public void UnLoadAssetBundle(string abName, bool unLoadAllObjs = false) {
    }

    public enum LoadMode {
        Resources,
        AssetBundle
    }
}
