using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Security.Policy;
using UnityEngine;

public class DownloadUtil
{

    static Stopwatch sw = new Stopwatch();

    static Stack<DownLoadList> downList = new Stack<DownLoadList>();

    static bool downloading = false;

    public static void downFile(Dictionary<string, string> list)
    {
        foreach (string item in list.Keys)
        {
            DownLoadList dl = new DownLoadList(item, list[item]);
            downList.Push(dl);
        }
        if (!downloading)
        {
            downloading = true;
            onDownloadFile();
        }
        
    }

    public static void onDownloadFile()
    {
        if (downList.Count == 0) {
            downloading = false;
            return;
        }

        DownLoadList dl = downList.Pop();

        using (WebClient wc = new WebClient())
        {
            sw.Start();
            wc.DownloadProgressChanged += ProgressChanged;
            wc.DownloadFileAsync(new System.Uri(dl.remoteFile), dl.localFile);
            wc.DownloadFileCompleted += DownLoadCompleted;
        }


    }

    static void DownLoadCompleted(object sender, AsyncCompletedEventArgs e) {
        onDownloadFile();
    }

    static void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        //UnityEngine.Debug.Log(e.ProgressPercentage);
        /*
        UnityEngine.Debug.Log(string.Format("{0} MB's / {1} MB's",
            (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
            (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00")));
        */
        //float value = (float)e.ProgressPercentage / 100f;

        string value = string.Format("{0} kb/s", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00"));
        if (e.ProgressPercentage == 100 && e.BytesReceived == e.TotalBytesToReceive)
        {
            sw.Reset();
            UnityEngine.Debug.Log(value);
        }
    }
}

public class DownLoadList {
    public string localFile;
    public string remoteFile;
    public DownLoadList(string local,string remote) {
        localFile = local;
        remoteFile = remote;
    }
}
