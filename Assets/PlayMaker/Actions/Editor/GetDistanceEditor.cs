/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.ComponentModel;
using System.Globalization;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMakerEditor
{
    [CustomActionEditor(typeof(HutongGames.PlayMaker.Actions.GetDistance))]
    public class GetDistanceEditor : CustomActionEditor
    {
        public override bool OnGUI()
        {
            return DrawDefaultInspector();
        }

        [Localizable(false)]
        public override void OnSceneGUI()
        {
            var action = (HutongGames.PlayMaker.Actions.GetDistance)target;

            var fromObject = action.Fsm.GetOwnerDefaultTarget(action.gameObject);
            var toObject = action.target;

            if (fromObject == null || toObject.IsNone || toObject.Value == null)
            {
                return;
            }

            var fromPos = fromObject.transform.position;
            var toPos = toObject.Value.transform.position;
            var distance = Vector3.Distance(fromPos, toPos);
            var label = string.Format("Get Distance:\n{0}", string.Format("{0:0.000}", distance));


            Handles.color = new Color(1, 1, 1, 0.5f);
            Handles.DrawLine(fromPos, toPos);
            Handles.Label((fromPos + toPos)*0.5f, label);
        }
    }
}
