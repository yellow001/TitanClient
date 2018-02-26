/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.

/* NOTE: Wrapper no longer needed in Unity 4.x
 * BUT: changing it breaks saved layouts
 * SO: wrap in namespace instead (supported in 4.x)
 */

// EditorWindow classes can't be called from a dll in Unity 3.5
// so create a thin wrapper class as a workaround

namespace HutongGames.PlayMakerEditor
{
    class FsmLogWindow : FsmLogger
    {
    }
}

