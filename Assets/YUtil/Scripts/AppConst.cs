using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class AppConst{

    public static readonly string UIScriptPath = Application.dataPath + "/";
    public static readonly string UITemplePath = Application.dataPath + "/YUtil/Scripts/Template/BaseUITemple.cs";
    //public static readonly string AbPath = Application.streamingAssetsPath;

#if UNITY_ANDROID && !UNITY_EDITOR
    public static string ResPath = Application.persistentDataPath;
#elif UNITY_STANDALONE || UNITY_EDITOR
    public static string ResPath = Application.streamingAssetsPath;
#endif

    public static readonly bool UpdateMode = false;         //是否更新资源
    public static readonly bool LuaAssetMode = true;       //是否从资源中加载lua脚本
    public static readonly string LuaPath = Application.dataPath + "/RideClient/Res/lua/";  //调试模式下lua脚本存放路径
    public static readonly string connKey = "rideClient";

    public static readonly string gateIP = "127.0.0.1";
    //public static readonly string gateIP = "127.0.0.1";
    public static readonly int gatePort = 12345;

    public static string regionIP = string.Empty;
    public static int regionPort = -1;

    public static readonly string[] dontPack = new string[] { ".cs", ".meta" };

    public static readonly string httpUrl = "http://localhost:7777/";

    public static int regionID;
    public static string accName;
    public static string roleKey;
    public static string roleName;

    public static readonly int lvExp = 150;

    public static string AssetEx = ".bundle";

}
