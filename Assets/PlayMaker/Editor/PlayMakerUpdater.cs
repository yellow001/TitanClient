/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using HutongGames.PlayMakerEditor;
using UnityEditor;

[InitializeOnLoad]
public class PlayMakerUpdater 
{
    static PlayMakerUpdater()
    {
        // Delay until first update
        // Otherwise process gets stomped on by other Unity initializations
        // E.g., Unity loading last layout stomps on PlayMakerUpgradeGuide window.
        EditorApplication.update += Update;
    }

    static void Update()
    {
        EditorApplication.update -= Update;

        /*
        var showUpgradeGuide = EditorPrefs.GetBool(EditorPrefStrings.ShowUpgradeGuide, true);
        if (showUpgradeGuide)
        {
            EditorWindow.GetWindow<PlayMakerUpgradeGuide>(true);
        }*/
    }
}
