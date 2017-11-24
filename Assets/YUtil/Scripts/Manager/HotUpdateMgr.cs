using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HotUpdateMgr : BaseManager<HotUpdateMgr> {
    //下载列表
    public Dictionary<string, string> downList = new Dictionary<string, string>();

    // Use this for initialization
    void Start() {
        StartCoroutine(Init());
    }

    /// <summary>
    /// 初始化
    /// </summary>
    new IEnumerator Init() {
        //检查&转移资源
        yield return StartCoroutine(CheckExtractResources());
        //更新资源
        yield return StartCoroutine(UpdateResources());
    }

    public IEnumerator CheckExtractResources() {
        //简单判断一下，以后可以增加逻辑
        if (!File.Exists(AppConst.ResPath + "/files.txt")) {
            yield return StartCoroutine(ExtractResources());
        }
        else {
            //tx.text += AppConst.ResPath + "/files.txt " + "资源文件已解压" + "   ";
            Debug.Log("资源文件已解压");
            yield break;
        }
    }

    IEnumerator ExtractResources() {
        if (Application.isMobilePlatform) {
            //移动平台则把文件从 streamingPath 转移到 persistentDataPath
            string dstPath = Application.persistentDataPath;

            if (!Directory.Exists(dstPath)) {
                Directory.CreateDirectory(dstPath);
            }

            DeleteDirctory(dstPath);
            string srcPath = Application.streamingAssetsPath;

            string infile = srcPath + "/files.txt";
            string outfile = dstPath + "/files.txt";
            if (File.Exists(outfile)) File.Delete(outfile);

            //tx.text += infile + "\n";

            //string message = "正在解包文件:>files.txt";
            Debug.Log(infile);
            Debug.Log(outfile);
            if (Application.platform == RuntimePlatform.Android) {
                WWW www = new WWW(infile);
                yield return www;
                if (www.isDone) {
                    //tx.text += "infile www done" + www.bytes.Length + "\n";
                    File.WriteAllBytes(outfile, www.bytes);
                }
                yield return 0;
            }
            else {
                File.Copy(infile, outfile, true);
            }

            yield return new WaitForEndOfFrame();

            //释放其他文件
            string[] files = File.ReadAllLines(outfile);

            foreach (var item in files) {
                string[] fs = item.Split('|');
                infile = srcPath + "/" + fs[0];
                outfile = dstPath + "/" + fs[0];

                string dir = Path.GetDirectoryName(outfile);
                if (!Directory.Exists(dir)) {
                    Directory.CreateDirectory(dir);
                }

                if (Application.platform == RuntimePlatform.Android) {
                    WWW www = new WWW(infile);
                    yield return www;

                    if (www.isDone) {
                        //tx.text += infile + " www done" + www.bytes.Length + "\n";
                        File.WriteAllBytes(outfile, www.bytes);
                    }
                    yield return 0;
                }
                else {

                    File.Copy(infile, outfile, true);
                }
                yield return new WaitForEndOfFrame();
            }

        }
        else {
            //其他平台（目前是win）直接使用 streamingPath 不用复制资源文件
            Debug.Log("win 平台直接使用streamingPath 不用复制资源文件");
            yield break;
        }
    }

    public IEnumerator UpdateResources() {

#if UNITY_EDITOR
        Debug.Log("当前为编辑器模式，不更新资源");
        yield break;
#endif

        //如果不是更新模式，返回
        if (!AppConst.UpdateMode) {
            //tx.text += "当前不是更新模式\n";
            Debug.Log("当前不是更新模式");
            yield break;
        }

        WWW www = new WWW(AppConst.httpUrl + "files.txt");
        yield return www;
        //tx.text += www.bytes.Length + "\n";
        if (!Directory.Exists(AppConst.ResPath)) {
            Directory.CreateDirectory(AppConst.ResPath);
        }
        //更新 files.txt 文件
        FileStream fs = new FileStream(AppConst.ResPath + "/files.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        sw.Write(www.text);
        sw.Close();
        fs.Close();

        AddDownLoadList(www.text);

        DownloadUtil.downFile(downList);
    }

    public void AddDownLoadList(string files) {
        downList.Clear();

        string[] allFs = files.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        foreach (var item in allFs) {
            string fileName = item.Split('|')[0];
            string remoteMD5 = item.Split('|')[1];

            //获取本地文件路径
            string localFile = AppConst.ResPath + "/" + fileName;

            string dir = Path.GetDirectoryName(localFile);
            if (!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }

            //若存在该文件
            if (File.Exists(localFile)) {
                //检查MD5是否相等
                if (remoteMD5.Equals(YUtil.md5file(localFile))) {
                    //相等，忽略该文件
                    continue;
                }
                else {
                    //删除该文件
                    File.Delete(localFile);
                }
            }

            //把该文件加入到下载列表
            string fileUrl = AppConst.httpUrl + fileName;
            downList.Add(localFile, fileUrl);
        }

        foreach (var item in downList.Keys) {
            Debug.Log(item + "|" + downList[item]);
        }
    }

    public void DeleteDirctory(string path) {
        foreach (var item in Directory.GetFiles(path)) {
            File.Delete(item);
        }

        foreach (var item in Directory.GetDirectories(path)) {
            Directory.Delete(item, true);
        }
    }
}
