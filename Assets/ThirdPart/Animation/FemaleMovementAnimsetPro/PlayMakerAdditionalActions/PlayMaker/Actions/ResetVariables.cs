/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

namespace HutongGames.PlayMaker.Actions{

	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets the value of listed Variables to Zero or Null.")]
	
	public sealed class ResetVariables : FsmStateAction{

        [UIHint(UIHint.Variable)] public FsmInt[] Integers;
        [UIHint(UIHint.Variable)] public FsmFloat[] Floats;
        [UIHint(UIHint.Variable)] public FsmBool[] Bools;
        public FsmGameObject[] GameObjects;

        public ResetVariables(){ Reset();}

	    public override void Reset(){
            Bools = new FsmBool[0];
            Integers = new FsmInt[0];
            Floats = new FsmFloat[0];
			GameObjects = new FsmGameObject[0];
		}

		public override void OnEnter()	{
            foreach (FsmBool fsmBool in Bools) fsmBool.Value = false;
            foreach (FsmInt fsmInt in Integers) fsmInt.Value = 0;
            foreach (FsmFloat fsmFloat in Floats) fsmFloat.Value = 0;
            foreach (FsmGameObject go in GameObjects) go.Value = null;
            Finish();		
		}
	}
}
