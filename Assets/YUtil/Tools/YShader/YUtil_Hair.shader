// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "YUtil/Normal/Hair"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_MaskClipValue( "Mask Clip Value", Float ) = 0.5
		_MainTex("MainTex", 2D) = "white" {}
		_Color("Color", Color) = (0,0,0,0)
		_NormalTex("NormalTex", 2D) = "white" {}
		_NormalValue("NormalValue", Range( 0 , 10)) = 1
		_EmissionValue("EmissionValue", Range( 0 , 5)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Lambert keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _NormalTex;
		uniform float4 _NormalTex_ST;
		uniform float _NormalValue;
		uniform float4 _Color;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float _EmissionValue;
		uniform float _MaskClipValue = 0.5;

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_NormalTex = i.uv_texcoord * _NormalTex_ST.xy + _NormalTex_ST.zw;
			o.Normal = ( tex2D( _NormalTex,uv_NormalTex) * _NormalValue ).rgb;
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float4 tex2DNode2 = tex2D( _MainTex,uv_MainTex);
			o.Albedo = ( _Color * tex2DNode2 ).xyz;
			o.Emission = ( tex2DNode2 * _EmissionValue ).xyz;
			o.Alpha = 1;
			clip( tex2DNode2.a - _MaskClipValue );
		}

		ENDCG
	}
	Fallback "Diffuse"
	//CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=5105
122;307;1439;877;1386.401;386.8999;1.3;True;True
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;Lambert;YUtility/Hair;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Custom;0.5;True;True;0;True;Transparent;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-37.5,-151.5;Float;False;0;COLOR;0.0;False;1;FLOAT4;0.0,0,0,0;False
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-253.5,456.5;Float;False;0;COLOR;0.0;False;1;FLOAT;0,0,0,0;False
Node;AmplifyShaderEditor.RangedFloatNode;6;-577.5,592.5;Float;False;Property;_NormalValue;NormalValue;3;0;1;0;10
Node;AmplifyShaderEditor.ColorNode;3;-366.5,-339.5;Float;False;Property;_Color;Color;1;0;0,0,0,0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-290.8001,109.3;Float;False;0;FLOAT4;0.0;False;1;FLOAT;0.0,0,0,0;False
Node;AmplifyShaderEditor.SamplerNode;2;-769.2999,-247.7;Float;True;Property;_MainTex;MainTex;0;0;Assets/Model/Hair/Textures/hairsky57_black.png;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False
Node;AmplifyShaderEditor.RangedFloatNode;8;-720.8001,266.3;Float;False;Property;_EmissionValue;EmissionValue;4;0;0;0;5
Node;AmplifyShaderEditor.SamplerNode;5;-726.5,364.5;Float;True;Property;_NormalTex;NormalTex;2;0;Assets/Model/Hair/Textures/hairsky57_n.dds;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False
WireConnection;0;0;4;0
WireConnection;0;1;7;0
WireConnection;0;2;9;0
WireConnection;0;10;2;4
WireConnection;4;0;3;0
WireConnection;4;1;2;0
WireConnection;7;0;5;0
WireConnection;7;1;6;0
WireConnection;9;0;2;0
WireConnection;9;1;8;0
ASEEND*/
//CHKSM=4C9C088C54FD24921E959970462EE8D57EB541D4