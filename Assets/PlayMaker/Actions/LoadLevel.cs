/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

#if (UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2)
#define UNITY_PRE_5_3
#endif

using UnityEngine;
#if !UNITY_PRE_5_3
using UnityEngine.SceneManagement;
#endif

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Level)]
    [Tooltip("Loads a Level by Name. NOTE: Before you can load a level, you have to add it to the list of levels defined in File->Build Settings...")]
    public class LoadLevel : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The name of the level to load. NOTE: Must be in the list of levels defined in File->Build Settings... ")]
        public FsmString levelName;

        [Tooltip("Load the level additively, keeping the current scene.")]
        public bool additive;

        [Tooltip("Load the level asynchronously in the background.")]
        public bool async;

        [Tooltip("Event to send when the level has loaded. NOTE: This only makes sense if the FSM is still in the scene!")]
        public FsmEvent loadedEvent;

        [Tooltip("Keep this GameObject in the new level. NOTE: The GameObject and components is disabled then enabled on load; uncheck Reset On Disable to keep the active state.")]
        public FsmBool dontDestroyOnLoad;

        [Tooltip("Event to send if the level cannot be loaded.")]
        public FsmEvent failedEvent;

        private AsyncOperation asyncOperation;

        public override void Reset()
        {
            levelName = "";
            additive = false;
            async = false;
            loadedEvent = null;
            dontDestroyOnLoad = false;
        }

        public override void OnEnter()
        {
            if (!Application.CanStreamedLevelBeLoaded(levelName.Value))
            {
                Fsm.Event(failedEvent);
                Finish();
                return;
            }

            if (dontDestroyOnLoad.Value)
            {
                // Have to get the root, since this FSM will be destroyed if a parent is destroyed.
                var root = Owner.transform.root;
                Object.DontDestroyOnLoad(root.gameObject);
            }

            if (additive)
            {
                if (async)
                {
#if UNITY_PRE_5_3
                    asyncOperation = Application.LoadLevelAdditiveAsync(levelName.Value);
#else
				    asyncOperation = SceneManager.LoadSceneAsync(levelName.Value, LoadSceneMode.Additive);
#endif

                    Debug.Log("LoadLevelAdditiveAsyc: " + levelName.Value);

                    return; // Don't Finish()
                }

#if UNITY_PRE_5_3
                Application.LoadLevelAdditive(levelName.Value);
#else
                SceneManager.LoadScene(levelName.Value, LoadSceneMode.Additive);
#endif

                Debug.Log("LoadLevelAdditive: " + levelName.Value);
            }
            else
                if (async)
                {
#if UNITY_PRE_5_3
                    asyncOperation = Application.LoadLevelAsync(levelName.Value);
#else
                    asyncOperation = SceneManager.LoadSceneAsync(levelName.Value, LoadSceneMode.Single);
#endif
                    Debug.Log("LoadLevelAsync: " + levelName.Value);

                    return; // Don't Finish()
                }
                else
                {
#if UNITY_PRE_5_3
                    Application.LoadLevel(levelName.Value);
#else
                    SceneManager.LoadScene(levelName.Value, LoadSceneMode.Single);
#endif
                    Debug.Log("LoadLevel: " + levelName.Value);
                }

            Log("LOAD COMPLETE");

            Fsm.Event(loadedEvent);
            Finish();
        }

        public override void OnUpdate()
        {
            if (asyncOperation.isDone)
            {
                Fsm.Event(loadedEvent);
                Finish();
            }
        }
    }
}
