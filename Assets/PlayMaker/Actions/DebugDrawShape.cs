/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Debug)]
    [Tooltip("Draw Gizmos in the Scene View.")]
    public class DebugDrawShape : FsmStateAction
    {
        public enum ShapeType { Sphere, Cube, WireSphere, WireCube }

        [RequiredField]
        [Tooltip("Draw the Gizmo at a GameObject's position.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("The type of Gizmo to draw:\nSphere, Cube, WireSphere, or WireCube.")]
        public ShapeType shape;
        
        [Tooltip("The color to use.")]
        public FsmColor color;
        
        [Tooltip("Use this for sphere gizmos")]
        public FsmFloat radius;
        
        [Tooltip("Use this for cube gizmos")]
        public FsmVector3 size;

        public override void Reset()
        {
            gameObject = null;
            shape = ShapeType.Sphere;
            color = Color.grey;
            radius = 1f;
            size = new Vector3(1f, 1f, 1f);
        }

        public override void OnDrawActionGizmos()
        {
            var ownerTransform = Fsm.GetOwnerDefaultTarget(gameObject).transform;
            if (ownerTransform == null)
                return;

            Gizmos.color = color.Value;

            switch (shape)
            {
                case ShapeType.Sphere:
                    Gizmos.DrawSphere(ownerTransform.position, radius.Value);
                    break;
                case ShapeType.WireSphere:
                    Gizmos.DrawWireSphere(ownerTransform.position, radius.Value);
                    break;
                case ShapeType.Cube:
                    Gizmos.DrawCube(ownerTransform.position, size.Value);
                    break;
                case ShapeType.WireCube:
                    Gizmos.DrawWireCube(ownerTransform.position, size.Value);
                    break;
                default:
                    break;
            }
        }
    }
}