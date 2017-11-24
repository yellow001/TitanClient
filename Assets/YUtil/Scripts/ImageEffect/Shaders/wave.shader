// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "YUtil/ImageEffect/wave"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex,_RainTex;
			fixed speedx,speedy;
			fixed _Dispower,_Transparent;
			float4 _Color;

			fixed timeScale;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed2 speed_all=fixed2(speedx,speedy);

				fixed2 dis_x=speed_all*_Time*2;
				fixed2 dis_y=speed_all*_Time;

				fixed2 offset_x=tex2D(_RainTex,i.uv+dis_x).x;
				fixed2 offset_y=tex2D(_RainTex,i.uv+dis_y).y;

				fixed2 offset_all=lerp(offset_x,offset_y,_Transparent)*_Dispower;

				fixed dis=pow(i.uv.x-0.5,2)+pow(i.uv.y-0.5,2);
				fixed dv=tan(_Time.w*0.5*timeScale);

				if(dis>0.05*dv&&dis<0.25*dv){
					fixed value=1-abs((dis-0.15*dv)/(0.1*dv));
					i.uv+=offset_all*value*0.1;
				}

				fixed4 col = tex2D(_MainTex, i.uv);
				return col*_Color;
			}
			ENDCG
		}
	}
}
