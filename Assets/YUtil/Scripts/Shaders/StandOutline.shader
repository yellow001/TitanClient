// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "YUtil/StandOutline"
{
	Properties
	{
		_ASEOutlineColor( "Outline Color", Color ) = (0,0,0,0)
		[HideInInspector] __dirty( "", Int ) = 1
		_ASEOutlineWidth( "Outline Width", Float ) = 0
		_ColPlus("color plus", Float) = 1
		_Main("Main", 2D) = "white" {}
		//_emission("emission", 2DArray ) = "" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ }
		Cull Front
		CGPROGRAM
		#pragma target 3.0
		#pragma surface outlineSurf Standard keepalpha noshadow noambient novertexlights nolightmap nodynlightmap nodirlightmap nofog nometa noforwardadd vertex:outlineVertexDataFunc
		struct Input
		{
			fixed filler;
		};
		uniform fixed4 _ASEOutlineColor;
		uniform fixed _ASEOutlineWidth;
		void outlineVertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			v.vertex.xyz += ( v.normal * _ASEOutlineWidth );
		}
		void outlineSurf( Input i, inout SurfaceOutputStandard o ) { o.Emission = _ASEOutlineColor.rgb; o.Alpha = 1; }
		ENDCG
		

		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		//#pragma target 3.5
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Main;
		uniform float4 _Main_ST;
		//uniform UNITY_DECLARE_TEX2DARRAY( _emission );
		uniform float4 _emission_ST;
		uniform float _ColPlus;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Main = i.uv_texcoord * _Main_ST.xy + _Main_ST.zw;
			o.Albedo = tex2D( _Main,uv_Main).xyz*_ColPlus;
			float2 uv_emission = i.uv_texcoord * _emission_ST.xy + _emission_ST.zw;
			//o.Emission = UNITY_SAMPLE_TEX2DARRAY(_emission, float3(uv_emission, 0.0)  ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	//CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=5105
-1168;56;1352;732;899;549.5;1;True;True
Node;AmplifyShaderEditor.SamplerNode;4;-437.7,-229.4;Float;True;Property;_Main;Main;0;0;Assets/AmplifyShaderEditor/Examples/Assets/Textures/Sand/Sand_basecolor.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False
Node;AmplifyShaderEditor.TextureArrayNode;5;-441.7,-25.39996;Float;False;Property;_emission;emission;1;0;None;0;Object;-1;Auto;0;FLOAT2;0,0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;3;Float;ASEMaterialInspector;Standard;YUtil/StandOutline;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;True;0;0,0,0,0;VertexOffset;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;0;0;4;0
WireConnection;0;2;5;0
ASEEND*/
//CHKSM=2021C72725E623269B1E5AA8870552216405C65A