using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Rain : MonoBehaviour {
    public Shader rain_sh;
    static Material rain_mat=null;
    public Texture RainTexture;
    public Color color=Color.white;
    [Range(-0.5f,0.5f)]
    public float speedx,speedy;
    [Range(-0.2f, 0.2f)]
    public float dispower;
    [Range(0, 1)]
    public float trans;


    //save the changed value

    protected Material material{
        get {
            if (rain_mat == null)
            {
                rain_mat = new Material(rain_sh);
                rain_mat.hideFlags = HideFlags.DontSave;
            }
            return rain_mat;
        }
    }



	// Use this for initialization
	void Start () {
        if (!SystemInfo.supportsImageEffects) {
            enabled = false;
            return;
        }

        if (!rain_sh || !material.shader.isSupported)
        {
            enabled = false;
            return;
        }

	}

	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
        if (!Application.isPlaying) {
            rain_sh = Shader.Find("YUtil/ImageEffect/rain");
        }
#endif
	}



    void OnDisable() {
        if (rain_mat)
        {
            DestroyImmediate(rain_mat);
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (rain_sh != null)
        {
            material.SetFloat("speedx", speedx);
            material.SetFloat("speedy", speedy);
            material.SetFloat("_Dispower", dispower);
            material.SetFloat("_Transparent", trans);
            material.SetTexture("_RainTex", RainTexture);
            material.SetTexture("_MainTex", RainTexture);
            material.SetColor("_Color", color);
            Graphics.Blit(source, destination, material);
        }
        else {
            Graphics.Blit(source, destination);
        }
    }

}
