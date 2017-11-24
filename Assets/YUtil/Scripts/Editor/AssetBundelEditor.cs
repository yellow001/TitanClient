using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class AssetBundelEditor{

    static List<AssetBundleBuild> map = new List<AssetBundleBuild>();
    static List<string> md5Files = new List<string>();
    //static List<string> luaTempFiles = new List<string>();

    static string AssetPath;

    [MenuItem("Tool/Build AssetBundle(win)")]
    public static void BuildAssetbundleWin() {
        BuildAssetbundle(BuildTarget.StandaloneWindows);
    }

    [MenuItem("Tool/Build AssetBundle(android)")]
    public static void BuildAssetbundleAndroid() {
        BuildAssetbundle(BuildTarget.Android);
    }
    
    public static void BuildAssetbundle(BuildTarget target) {
        AssetPath = EditorUtility.OpenFolderPanel("选择打包路径", "", "");
        if (string.IsNullOrEmpty(AssetPath)) {
            return;
        }
        BuildMap(AssetPath, true);

        if (Directory.Exists(Application.streamingAssetsPath)) {
            Directory.Delete(Application.streamingAssetsPath, true);
        }

        Directory.CreateDirectory(Application.streamingAssetsPath);

        BuildAssetBundleOptions bop = BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.DeterministicAssetBundle;

        BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, map.ToArray(), bop, BuildTarget.StandaloneWindows);

        CreateMD5File();

        Clear();
        AssetDatabase.Refresh();
    }

    public static void BuildMap(string path, bool isRoot = false) {

        //文件路径最后的分隔符是 '\' ,要替换成 '/'
        path = path.Replace('\\', '/');

        string[] files = Directory.GetFiles(path);

        for (int i = 0; i < files.Length; i++) {
            //文件路径最后的分隔符是 '\' ,要替换成 '/'
            files[i] = files[i].Replace('\\', '/');
        }


        if (files.Length != 0) {
            string abName;
            if (isRoot) {
                string[] temp = path.Split(new string[] { "/" }, System.StringSplitOptions.RemoveEmptyEntries);
                abName = temp[temp.Length - 1];
                Debug.Log(abName);
            }
            else {
                abName = GetRelativePath(path, AssetPath);
            }

            AddAssetBundleBuild(abName, files);
        }


        foreach (var item in Directory.GetDirectories(path)) {
            BuildMap(item);
        }
    }

    public static void AddAssetBundleBuild(string abName, string[] files) {
        if (string.IsNullOrEmpty(abName) || files == null || files.Length == 0) { return; }

        List<string> packFiles = new List<string>();

        for (int i = 0; i < files.Length; i++) {
            if (canPack(files[i])) {

                ////替换其中的 lua 文件为 txt 文件
                //if (files[i].EndsWith(".lua")) {
                //    files[i] = getLuaFile(files[i]);
                //}

                //获取相对于项目的路径（完整的路径是不会打包的）
                files[i] = FileUtil.GetProjectRelativePath(files[i]);

                packFiles.Add(files[i]);
            }
        }

        //AssetDatabase.Refresh();

        AssetBundleBuild abb = new AssetBundleBuild();
        abb.assetBundleName = abName + AppConst.AssetEx;
        abb.assetNames = packFiles.ToArray();

        map.Add(abb);
    }

    /// <summary>
    /// 生成 MD5 总文件
    /// </summary>
    public static void CreateMD5File() {
        GetAllFilePath(Application.streamingAssetsPath);

        FileStream fs = new FileStream(Application.streamingAssetsPath + "/" + "files.txt", FileMode.CreateNew);

        StreamWriter sw = new StreamWriter(fs);

        foreach (string item in md5Files) {
            string name = GetRelativePath(item, Application.streamingAssetsPath);
            string md5 = YUtil.md5file(item);

            sw.WriteLine(name + "|" + md5);
        }

        sw.Close();
        fs.Close();
    }

    /// <summary>
    /// 获取目录下所有文件并保存在 md5Files 中
    /// </summary>
    /// <param name="path"></param>
    public static void GetAllFilePath(string path) {
        string[] temp = Directory.GetFiles(path);

        //List<string> files = new List<string>();

        for (int i = 0; i < temp.Length; i++) {
            if (temp[i].Contains(".meta")) {
                //忽略 .meta 文件
                continue;
            }
            //替换文件路径分隔符中的 '\'为'/'
            temp[i] = temp[i].Replace('\\', '/');
            //files.Add(temp[i]);
            md5Files.Add(temp[i]);
        }
        
        //md5Files.AddRange(temp);

        foreach (var item in Directory.GetDirectories(path)) {
            GetAllFilePath(item);
        }
    }

    /// <summary>
    /// 获取相对于给定父路径的相对路径
    /// </summary>
    /// <param name="childPath"></param>
    /// <param name="parentPath"></param>
    /// <returns></returns>
    public static string GetRelativePath(string childPath, string parentPath) {
        //Debug.Log("childParh: " + childPath + " parentPath: " + parentPath);
        if (!childPath.Contains(parentPath)) {
            Debug.Log("the path you given is not a chid of the parent path");
            Debug.Log("childParh: " + childPath + " parentPath: " + parentPath);
            return string.Empty;
        }

        string[] temp = childPath.Split(new string[] { parentPath + "/" }, System.StringSplitOptions.RemoveEmptyEntries);
        childPath = temp[temp.Length - 1];
        //Debug.Log("relative childParh : " + childPath);
        return childPath;
    }

    /// <summary>
    /// 判断文件是否打包
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static bool canPack(string file) {
        foreach (var item in AppConst.dontPack) {
            if (file.Contains(item)) {
                return false;
            }
        }
        return true;
    }

    //static string getLuaFile(string file) {
    //    string temp = file.Replace(".lua", ".txt");

    //    string content = File.ReadAllText(file);

    //    StreamWriter sw = new StreamWriter(File.Open(temp, FileMode.OpenOrCreate));
    //    sw.Write(content);
    //    sw.Flush();
    //    sw.Close();

    //    luaTempFiles.Add(temp);
    //    return temp;
    //}

    //static void DeleteTempLua() {
    //    foreach (var item in luaTempFiles) {
    //        //Debug.Log(item);
    //        string meta = item + ".meta";
    //        File.Delete(meta);
    //        File.Delete(item);
    //    }
    //    luaTempFiles.Clear();
    //}

    public static void Clear() {
        map.Clear();
        md5Files.Clear();
        //DeleteTempLua();
    }

    [MenuItem("YUtility/addCoin")]
    static void AddCoin() {
        //PlayerInfo.Ins.coin += 5000;
        //PlayerInfo.Ins.Save();
    }
}
