Shader "YUtil/Normal/StandardTwoPass" {
	Properties {
		_Color("Color", Color) = (1,1,1,1)
		
        _MainTex("Albedo", 2D) = "white" {}
		

        _Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

        _Glossiness("Smoothness", Range(0.0, 1.0)) = 0.5
        _GlossMapScale("Smoothness Scale", Range(0.0, 1.0)) = 1.0
        [Enum(Metallic Alpha,0,Albedo Alpha,1)] _SmoothnessTextureChannel ("Smoothness texture channel", Float) = 0

        [Gamma] _Metallic("Metallic", Range(0.0, 1.0)) = 0.0
        _MetallicGlossMap("Metallic", 2D) = "white" {}

        [ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
        [ToggleOff] _GlossyReflections("Glossy Reflections", Float) = 1.0

        _BumpScale("Scale", Float) = 1.0
        _BumpMap("Normal Map", 2D) = "bump" {}

        _Parallax ("Height Scale", Range (0.005, 0.08)) = 0.02
        _ParallaxMap ("Height Map", 2D) = "black" {}

        _OcclusionStrength("Strength", Range(0.0, 1.0)) = 1.0
        _OcclusionMap("Occlusion", 2D) = "white" {}

        _EmissionColor("Color", Color) = (0,0,0)
        _EmissionMap("Emission", 2D) = "white" {}

        _DetailMask("Detail Mask", 2D) = "white" {}

        _DetailAlbedoMap("Detail Albedo x2", 2D) = "grey" {}
        _DetailNormalMapScale("Scale", Float) = 1.0
        _DetailNormalMap("Normal Map", 2D) = "bump" {}

        [Enum(UV0,0,UV1,1)] _UVSec ("UV Set for secondary textures", Float) = 0


        // Blending state
        [HideInInspector] _Mode ("__mode", Float) = 0.0
        [HideInInspector] _SrcBlend ("__src", Float) = 1.0
        [HideInInspector] _DstBlend ("__dst", Float) = 0.0
        [HideInInspector] _ZWrite ("__zw", Float) = 1.0

		_SColor("SColor", Color) = (1,1,1,0)
		_SecTex("SecTex", 2D) = "white" {}

		_SEmissionColor("SEColor", Color) = (0,0,0)
        _SEmissionMap("SEmission", 2D) = "white" {}

		_SecNormal("SecNormal", 2D) = "bump" {}
		_NormalScale("normalScale",float)=1
		_SMetallic("SMetallic", Range(0.0, 1.0)) = 0.0
		_SGlossiness("SSmoothness", Range(0.0, 1.0)) = 0.5
		_Cutoff("Cutoff Value",Range(0,1.1))=0.5
	}

	SubShader {
		Tags { "RenderType"="Opaque" "Queue"="Transparent" }
		LOD 200
		
		UsePass "Standard/FORWARD"
		UsePass "Standard/FORWARD_DELTA"
		UsePass "Standard/DEFERRED"

		//BLENDOP min

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		

		struct Input {
			float2 uv_MainTex;
		};

		sampler2D _SecTex;
		sampler2D _SecNormal;
		sampler2D _SEmissionMap;
		half _SGlossiness;
		half _SMetallic;
		fixed4 _SColor;
		fixed4 _SEmissionColor;
		half _Cutoff;
		half _NormalScale;


		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_SecTex, IN.uv_MainTex) * _SColor;
			fixed3 n=UnpackNormal(tex2D(_SecNormal,IN.uv_MainTex))*_NormalScale;
			fixed4 e=tex2D (_SEmissionMap, IN.uv_MainTex) * _SEmissionColor;
			o.Albedo = c;
			// Metallic and smoothness come from slider variables
			o.Alpha = c.a;
			
			o.Normal=n;

			if(o.Alpha>_Cutoff){
				o.Metallic = _SMetallic*o.Alpha;
				o.Smoothness = _SGlossiness*o.Alpha;
				o.Emission= e*o.Alpha;
			}else{
				//o.Metallic = _BMetallic*o.Alpha;
				//o.Smoothness = _BGlossiness*o.Alpha;
				o.Alpha=0;
			}
		}
		ENDCG
	}
	FallBack "Diffuse"
	CustomEditor "StandardShaderTwoPassGUI"
}
