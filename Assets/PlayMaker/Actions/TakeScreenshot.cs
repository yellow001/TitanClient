/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

#if !(UNITY_FLASH || UNITY_METRO)

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
    [Tooltip("Saves a Screenshot. NOTE: Does nothing in Web Player. On Android, the resulting screenshot is available some time later.")]
	public class TakeScreenshot : FsmStateAction
	{
	    public enum Destination
	    {
	        MyPictures,
            PersistentDataPath,
            CustomPath
	    }

        [Tooltip("Where to save the screenshot.")]
	    public Destination destination;

        [Tooltip("Path used with Custom Path Destination option.")]
	    public FsmString customPath;

		[RequiredField]
		public FsmString filename;

        [Tooltip("Add an auto-incremented number to the filename.")]
		public FsmBool autoNumber;

        [Tooltip("Factor by which to increase resolution.")]
	    public FsmInt superSize;

        [Tooltip("Log saved file info in Unity console.")]
	    public FsmBool debugLog;

		private int screenshotCount;

		public override void Reset()
		{
            destination = Destination.MyPictures;
			filename = "";
			autoNumber = null;
		    superSize = null;
		    debugLog = null;
		}

		public override void OnEnter()
		{
			if (string.IsNullOrEmpty(filename.Value)) return;

		    string screenshotPath;
		    switch (destination)
		    {
		        case Destination.MyPictures:
                    screenshotPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
		            break;
		        case Destination.PersistentDataPath:
		            screenshotPath = Application.persistentDataPath;
		            break;
                case Destination.CustomPath:
		            screenshotPath = customPath.Value;
                    break;
		        default:
		            screenshotPath = "";
                    break;
		    }

		    screenshotPath = screenshotPath.Replace("\\","/") + "/";
		    var screenshotFullPath = screenshotPath + filename.Value + ".png";

		    if (autoNumber.Value)
		    {
		        while (System.IO.File.Exists(screenshotFullPath))
		        {
		            screenshotCount++;
		            screenshotFullPath = screenshotPath + filename.Value + screenshotCount + ".png";
		        }
		    }

            if (debugLog.Value)
            {
                Debug.Log("TakeScreenshot: " + screenshotFullPath);
            }

		    ScreenCapture.CaptureScreenshot(screenshotFullPath, superSize.Value);

		    Finish();
		}
	}
}

#endif
