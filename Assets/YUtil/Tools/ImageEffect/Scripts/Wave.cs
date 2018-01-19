using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Wave : MonoBehaviour {
    public Shader wa_sh;
    static Material wa_mat=null;
    public Texture RainTexture;
    public Color color=Color.white;
    [Range(-0.5f,0.5f)]
    public float speedx,speedy;
    [Range(-1f, 1f)]
    public float dispower;
    [Range(0, 1)]
    public float trans;
    [Range(1, 10)]
    public float timeScale;


    //save the changed value

    protected Material material{
        get {
            if (wa_mat == null)
            {
                wa_mat = new Material(wa_sh);
                wa_mat.hideFlags = HideFlags.DontSave;
            }
            return wa_mat;
        }
    }



	// Use this for initialization
	void Start () {
        if (!SystemInfo.supportsImageEffects) {
            enabled = false;
            return;
        }

        if (!wa_sh || !material.shader.isSupported)
        {
            enabled = false;
            return;
        }

	}

	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
        if (wa_sh==null) {
            wa_sh = Shader.Find("YUtil/ImageEffect/wave");
        }
#endif
	}



    void OnDisable() {
        if (wa_mat)
        {
            DestroyImmediate(wa_mat);
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (wa_sh != null)
        {
            material.SetFloat("speedx", speedx);
            material.SetFloat("speedy", speedy);
            material.SetFloat("_Dispower", dispower);
            material.SetFloat("_Transparent", trans);
            material.SetTexture("_RainTex", RainTexture);
            material.SetTexture("_MainTex", RainTexture);
            material.SetColor("_Color", color);
            material.SetFloat("timeScale", timeScale);
            Graphics.Blit(source, destination, material);
        }
        else {
            Graphics.Blit(source, destination);
        }
    }

}
