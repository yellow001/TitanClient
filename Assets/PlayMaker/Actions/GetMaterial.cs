/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
// Thanks to: Giyomu
// http://hutonggames.com/playmakerforum/index.php?topic=400.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Material)]
    [Tooltip("Get a material at index on a gameObject and store it in a variable")]
    public class GetMaterial : ComponentAction<Renderer>
    {
	    [RequiredField]
	    [CheckForComponent(typeof(Renderer))]
        [Tooltip("The GameObject the Material is applied to.")]
	    public FsmOwnerDefault gameObject;

        [Tooltip("The index of the Material in the Materials array.")]
	    public FsmInt materialIndex;
	    
        [RequiredField]
	    [UIHint(UIHint.Variable)]
        [Tooltip("Store the material in a variable.")]
        public FsmMaterial material;
	
        [Tooltip("Get the shared material of this object. NOTE: Modifying the shared material will change the appearance of all objects using this material, and change material settings that are stored in the project too.")]
	    public bool getSharedMaterial;

	    public override void Reset()
	    {
		    gameObject = null;
		    material = null;
		    materialIndex = 0;
		    getSharedMaterial = false;
	    }

	    public override void OnEnter ()
	    {
		    DoGetMaterial();
		    Finish();
	    }
	
	    void DoGetMaterial()
	    {
		    var go = Fsm.GetOwnerDefaultTarget(gameObject);
		    if (!UpdateCache(go))
		    {
			    return;
		    }
		
		    if (materialIndex.Value == 0 && !getSharedMaterial)
		    {
			    material.Value = renderer.material;
		    }
		
		    else if(materialIndex.Value == 0 && getSharedMaterial)
		    {
			    material.Value = renderer.sharedMaterial;
		    }
	
		    else if (renderer.materials.Length > materialIndex.Value && !getSharedMaterial)
		    {
			    var materials = renderer.materials;
			    material.Value = materials[materialIndex.Value];
			    renderer.materials = materials;
		    }

		    else if (renderer.materials.Length > materialIndex.Value && getSharedMaterial)
		    {
			    var materials = renderer.sharedMaterials;
			    material.Value = materials[materialIndex.Value];
			    renderer.sharedMaterials = materials;
		    }
	    }
    }
}
