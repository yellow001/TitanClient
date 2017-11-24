using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoolMgr : BaseManager<PoolMgr> {
    public Dictionary<string, Queue<GameObject>> nameToPool = new Dictionary<string, Queue<GameObject>>();
    // Use this for initialization
    public override void Init() {
        base.Init();
        SceneManager.sceneLoaded += OnLoadScene;
    }

    private void OnLoadScene(Scene arg0, LoadSceneMode arg1) {
        Clear();
    }

    public void Push<T>(T obj) where T : MonoBehaviour, IPoolReset {
        obj.gameObject.SetActive(false);
        if (!nameToPool.ContainsKey(obj.GetPoolName())) {
            nameToPool.Add(obj.GetPoolName(), new Queue<GameObject>());
        }
        nameToPool[obj.GetPoolName()].Enqueue(obj.gameObject);
        //Debug.Log(obj.GetPoolName());
    }

    public GameObject GetResObj(string path) {

        string[] names = path.Split('/');
        string name = names[names.Length - 1];
        if (nameToPool.ContainsKey(name) && nameToPool[name].Count > 0) {
            //Debug.Log("get "+name+"  "+nameToPool[name].Count);
            GameObject obj = nameToPool[name].Dequeue();
            obj.GetComponent<IPoolReset>().Reset();
            return obj;
        }
        else {
            GameObject ga = ResMgr.Ins.GetResAsset<GameObject>(path);
            ga = Instantiate(ga);
            ga.SetActive(false);
            ga.GetComponent<IPoolReset>().SetPoolName(name);
            if (!nameToPool.ContainsKey(name)) {
                nameToPool.Add(name, new Queue<GameObject>());
            }
            //Debug.Log("reload    " + name + "  " + nameToPool[name].Count);
            return ga;
        }

    }

    public GameObject GetAssetObj(string abName, string assetName) {

        if (nameToPool.ContainsKey(assetName) && nameToPool[assetName].Count > 0) {
            //Debug.Log("get "+name+"  "+nameToPool[name].Count);
            GameObject obj = nameToPool[assetName].Dequeue();
            obj.GetComponent<IPoolReset>().Reset();
            return obj;
        }
        else {
            GameObject ga = ResMgr.Ins.getAsset<GameObject>(abName, assetName);
            if (ga == null) {
                return null;
            }
            ga = Instantiate(ga);
            ga.SetActive(false);
            ga.GetComponent<IPoolReset>().SetPoolName(assetName);
            if (!nameToPool.ContainsKey(assetName)) {
                nameToPool.Add(assetName, new Queue<GameObject>());
            }
            //Debug.Log("reload    " + name + "  " + nameToPool[name].Count);
            return ga;
        }

    }

    public void Clear() {
        foreach (var item in nameToPool.Keys) {
            nameToPool[item].Clear();
        }
        nameToPool.Clear();
    }
}

public interface IPoolReset {
    void Reset();
    string GetPoolName();
    void SetPoolName(string n);
}
