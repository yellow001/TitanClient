/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    // Base class for actions that access a Component on a GameObject.
    // Caches the component for performance
    public abstract class ComponentAction<T> : FsmStateAction where T : Component
    {
        private GameObject cachedGameObject;
        private T component;

        protected Rigidbody rigidbody
        {
            get { return component as Rigidbody; }
        }

        protected Rigidbody2D rigidbody2d
        {
            get { return component as Rigidbody2D; }
        }

        protected Renderer renderer
        {
            get { return component as Renderer; }
        }

        protected Animation animation
        {
            get { return component as Animation; }
        }

        protected AudioSource audio
        {
            get { return component as AudioSource; }
        }

        protected Camera camera
        {
            get { return component as Camera; }
        }

        protected GUIText guiText
        {
            get { return component as GUIText; }
        }

        protected GUITexture guiTexture
        {
            get { return component as GUITexture; }
        }

        protected Light light
        {
            get { return component as Light; }
        }

#if !(UNITY_FLASH || UNITY_NACL || UNITY_METRO || UNITY_WP8 || UNITY_WIIU || UNITY_PSM || UNITY_WEBGL || UNITY_PS3 || UNITY_PS4 || UNITY_XBOXONE)
        protected NetworkView networkView
        {
            get { return component as NetworkView; }
        }
#endif
        protected bool UpdateCache(GameObject go)
        {
            if (go == null)
            {
                return false;
            }

            if (component == null || cachedGameObject != go)
            {
                component = go.GetComponent<T>();
                cachedGameObject = go;

                if (component == null)
                {
                    LogWarning("Missing component: " + typeof(T).FullName + " on: " + go.name);
                }
            }

            return component != null;
        }
    }
}
