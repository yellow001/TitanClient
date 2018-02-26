/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMakerEditor
{
    [CustomActionEditor(typeof(HutongGames.PlayMaker.Actions.SetCameraFOV))]
    public class SetCameraFOVActionEditor : CustomActionEditor
    {
        private GameObject cachedGameObject;
        private Camera camera;

        public override bool OnGUI()
        {
            return DrawDefaultInspector();
        }

        public override void OnSceneGUI()
        {
            var setCameraFOVAction = (HutongGames.PlayMaker.Actions.SetCameraFOV)target;
            if (setCameraFOVAction.fieldOfView.IsNone)
            {
                return;
            }

            var go = setCameraFOVAction.Fsm.GetOwnerDefaultTarget(setCameraFOVAction.gameObject);
            var fov = setCameraFOVAction.fieldOfView.Value;

            if (go != null && fov > 0)
            {
                if (go != cachedGameObject || camera == null)
                {
                    camera = go.GetComponent<Camera>();
                    cachedGameObject = go;
                }

                if (camera != null)
                {
                    var originalFOV = camera.fieldOfView;
                    camera.fieldOfView = setCameraFOVAction.fieldOfView.Value;

                    Handles.color = new Color(1, 1, 0, .5f);
                    SceneGUI.DrawCameraFrustrum(camera);

                    camera.fieldOfView = originalFOV;
                }
            }
        }
    }
}
