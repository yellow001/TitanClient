/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.Linq;
using HutongGames.PlayMaker;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace HutongGames.PlayMakerEditor
{
    public class PlayMakerBuildCallbacks
    {
        [PostProcessSceneAttribute(2)]
        public static void OnPostprocessScene()
        {
            /* TODO: Figure out if we need to do this!
            // OnPostprocessScene is called when loading a scene in the editor 
            // Might not want to post process in that case...?
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }*/

            //Debug.Log("OnPostprocessScene");

            PlayMakerGlobals.IsBuilding = true;
            PlayMakerGlobals.InitApplicationFlags();

            var fsmList = Resources.FindObjectsOfTypeAll<PlayMakerFSM>();
            foreach (var playMakerFSM in fsmList)
            {
                //Debug.Log(FsmEditorUtility.GetFullFsmLabel(playMakerFSM));
                
                if (!Application.isPlaying) // actually making a build vs playing in editor
                {
                    playMakerFSM.Preprocess();
                }
            }

            PlayMakerGlobals.IsBuilding = false;

            //Debug.Log("EndPostProcessScene");
        }
    }
}
