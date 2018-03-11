// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Enviro/EnviroSurfaceShader/Metallic" {
    Properties {
        _Albedo ("Albedo (RGB)", 2D) = "white" {}
        _AlbedoColor ("Albedo Color", Color) = (1,1,1,1)
        _Metallic ("Metallic (R) and Smoothness (A)", 2D) = "black" {}
        [Normal]_NormalMap ("Normal Map", 2D) = "bump" {}
        _Occlusion ("Occlusion", 2D) = "white" {}
        _OcclusionStrenght ("Occlusion Strenght", Range(0, 1)) = 0.07361673
        _DetailIntensity ("Detail Intensity", Range(0, 1)) = 0
        _DetailAlbedo ("Detail Albedo (RGB)", 2D) = "white" {}
        _DetailNormal ("Detail Normal", 2D) = "bump" {}
        _Raining ("Raining", Range(0, 1)) = 0
        _Smoothness_dry ("Smoothness Dry", Range(-1, 1)) = 0
        _Smoothness_wet ("Smoothness Wet", Range(-1, 1)) = 0.5811966
        [Normal]_WetNormal ("Wet Normal Map", 2D) = "bump" {}
        [HideInInspector]_Ripple ("Ripple", 2D) = "bump" {}
        _SnowStrenght ("Snow Strenght", Range(0, 1)) = 0
        _SnowAmount ("Snow Amount", Float ) = 0.1
        _SnowAlbedo ("Snow Albedo (RGB)", 2D) = "white" {}
        [Normal]_SnowNormal ("Snow Normal", 2D) = "bump" {}
        [NoScaleOffset]_Mask ("Mask (R) Detail,(G) Snow, (B) Wet", 2D) = "white" {}
        [HideInInspector]_NormalClear ("NormalClear", 2D) = "bump" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma target 3.0
            #pragma glsl
            
            uniform sampler2D _WetNormal; uniform float4 _WetNormal_ST;
            uniform float _Smoothness_dry;
            uniform sampler2D _Ripple; uniform float4 _Ripple_ST;
            uniform sampler2D _Albedo; uniform float4 _Albedo_ST;
            uniform float4 _AlbedoColor;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform sampler2D _SnowAlbedo; uniform float4 _SnowAlbedo_ST;
            uniform sampler2D _SnowNormal; uniform float4 _SnowNormal_ST;
            uniform float _SnowStrenght;
            uniform sampler2D _Mask;
            uniform sampler2D _NormalClear; uniform float4 _NormalClear_ST;
            uniform float _Raining;
            uniform float _SnowAmount;
            uniform sampler2D _Metallic; uniform float4 _Metallic_ST;
            uniform float _Smoothness_wet;
            uniform sampler2D _DetailNormal; uniform float4 _DetailNormal_ST;
            uniform sampler2D _DetailAlbedo; uniform float4 _DetailAlbedo_ST;
            uniform sampler2D _Occlusion; uniform float4 _Occlusion_ST;
            uniform float _OcclusionStrenght;
            uniform float _DetailIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 _Mask_var = tex2Dlod(_Mask,float4(o.uv0,0.0,0));
                v.vertex.xyz += (lerp(float3(0,0,0),float3(_Mask_var.g,_Mask_var.g,_Mask_var.g),(_SnowStrenght*_SnowAmount))*normalize(v.normal));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
                float3 _NormalClear_var = UnpackNormal(tex2D(_NormalClear,TRANSFORM_TEX(i.uv0, _NormalClear)));
                float3 _DetailNormal_var = UnpackNormal(tex2D(_DetailNormal,TRANSFORM_TEX(i.uv0, _DetailNormal)));
                float4 _Mask_var = tex2D(_Mask,i.uv0);
                float node_7641 = (_DetailIntensity*_Mask_var.r);
                float3 node_498_nrm_base = _NormalMap_var.rgb + float3(0,0,1);
                float3 node_498_nrm_detail = lerp(_NormalClear_var.rgb,_DetailNormal_var.rgb,node_7641) * float3(-1,-1,1);
                float3 node_498_nrm_combined = node_498_nrm_base*dot(node_498_nrm_base, node_498_nrm_detail)/node_498_nrm_base.z - node_498_nrm_detail;
                float3 node_498 = node_498_nrm_combined;
                float3 _WetNormal_var = UnpackNormal(tex2D(_WetNormal,TRANSFORM_TEX(i.uv0, _WetNormal)));
                float3 _Ripple_var = UnpackNormal(tex2D(_Ripple,TRANSFORM_TEX(i.uv0, _Ripple)));
                float3 node_4233_nrm_base = _WetNormal_var.rgb + float3(0,0,1);
                float3 node_4233_nrm_detail = _Ripple_var.rgb * float3(-1,-1,1);
                float3 node_4233_nrm_combined = node_4233_nrm_base*dot(node_4233_nrm_base, node_4233_nrm_detail)/node_4233_nrm_base.z - node_4233_nrm_detail;
                float3 node_4233 = node_4233_nrm_combined;
                float3 node_980_nrm_base = node_498 + float3(0,0,1);
                float3 node_980_nrm_detail = lerp(_NormalClear_var.rgb,node_4233,(_Raining*_Mask_var.b)) * float3(-1,-1,1);
                float3 node_980_nrm_combined = node_980_nrm_base*dot(node_980_nrm_base, node_980_nrm_detail)/node_980_nrm_base.z - node_980_nrm_detail;
                float3 node_980 = node_980_nrm_combined;
                float3 _SnowNormal_var = UnpackNormal(tex2D(_SnowNormal,TRANSFORM_TEX(i.uv0, _SnowNormal)));
                float3 normalLocal = lerp(node_980,lerp(node_980,_SnowNormal_var.rgb,_Mask_var.g),_SnowStrenght);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); 
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
                float4 _Metallic_var = tex2D(_Metallic,TRANSFORM_TEX(i.uv0, _Metallic));
                float node_1938 = (-1.0);
                float gloss = (_Metallic_var.a*lerp(lerp(_Smoothness_dry,_Smoothness_wet,(_Raining*_Mask_var.b)),node_1938,_SnowStrenght));
                float specPow = exp2( gloss * 10.0+1.0);
                
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                d.boxMax[0] = unity_SpecCube0_BoxMax;
                d.boxMin[0] = unity_SpecCube0_BoxMin;
                d.probePosition[0] = unity_SpecCube0_ProbePosition;
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.boxMax[1] = unity_SpecCube1_BoxMax;
                d.boxMin[1] = unity_SpecCube1_BoxMin;
                d.probePosition[1] = unity_SpecCube1_ProbePosition;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
                
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float4 _Albedo_var = tex2D(_Albedo,TRANSFORM_TEX(i.uv0, _Albedo));
                float3 node_6517 = (_Albedo_var.rgb*_AlbedoColor.rgb);
                float4 _DetailAlbedo_var = tex2D(_DetailAlbedo,TRANSFORM_TEX(i.uv0, _DetailAlbedo));
                float3 node_8328 = lerp(node_6517,_DetailAlbedo_var.rgb,node_7641);
                float4 _SnowAlbedo_var = tex2D(_SnowAlbedo,TRANSFORM_TEX(i.uv0, _SnowAlbedo));
                float3 diffuseColor = lerp(node_8328,lerp(node_8328,_SnowAlbedo_var.rgb,_SnowStrenght),_Mask_var.g); 
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, lerp(_Metallic_var.rgb,float3(_SnowAlbedo_var.a,_SnowAlbedo_var.a,_SnowAlbedo_var.a),_SnowStrenght).r, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * UNITY_PI / 4.0);
                float3 directSpecular = 1 * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                float3 specular = (directSpecular + indirectSpecular);

                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; 
                indirectDiffuse += gi.indirect.diffuse;
                float4 _Occlusion_var = tex2D(_Occlusion,TRANSFORM_TEX(i.uv0, _Occlusion));
                indirectDiffuse *= pow(_Occlusion_var.r,saturate((_OcclusionStrenght+(_SnowStrenght*node_1938)))); 
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;

                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma target 3.0
            #pragma glsl
            uniform sampler2D _WetNormal; uniform float4 _WetNormal_ST;
            uniform float _Smoothness_dry;
            uniform sampler2D _Ripple; uniform float4 _Ripple_ST;
            uniform sampler2D _Albedo; uniform float4 _Albedo_ST;
            uniform float4 _AlbedoColor;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform sampler2D _SnowAlbedo; uniform float4 _SnowAlbedo_ST;
            uniform sampler2D _SnowNormal; uniform float4 _SnowNormal_ST;
            uniform float _SnowStrenght;
            uniform sampler2D _Mask;
            uniform sampler2D _NormalClear; uniform float4 _NormalClear_ST;
            uniform float _Raining;
            uniform float _SnowAmount;
            uniform sampler2D _Metallic; uniform float4 _Metallic_ST;
            uniform float _Smoothness_wet;
            uniform sampler2D _DetailNormal; uniform float4 _DetailNormal_ST;
            uniform sampler2D _DetailAlbedo; uniform float4 _DetailAlbedo_ST;
            uniform float _DetailIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                float4 _Mask_var = tex2Dlod(_Mask,float4(o.uv0,0.0,0));
                v.vertex.xyz += (lerp(float3(0,0,0),float3(_Mask_var.g,_Mask_var.g,_Mask_var.g),(_SnowStrenght*_SnowAmount))*normalize(v.normal));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
                float3 _NormalClear_var = UnpackNormal(tex2D(_NormalClear,TRANSFORM_TEX(i.uv0, _NormalClear)));
                float3 _DetailNormal_var = UnpackNormal(tex2D(_DetailNormal,TRANSFORM_TEX(i.uv0, _DetailNormal)));
                float4 _Mask_var = tex2D(_Mask,i.uv0);
                float node_7641 = (_DetailIntensity*_Mask_var.r);
                float3 node_498_nrm_base = _NormalMap_var.rgb + float3(0,0,1);
                float3 node_498_nrm_detail = lerp(_NormalClear_var.rgb,_DetailNormal_var.rgb,node_7641) * float3(-1,-1,1);
                float3 node_498_nrm_combined = node_498_nrm_base*dot(node_498_nrm_base, node_498_nrm_detail)/node_498_nrm_base.z - node_498_nrm_detail;
                float3 node_498 = node_498_nrm_combined;
                float3 _WetNormal_var = UnpackNormal(tex2D(_WetNormal,TRANSFORM_TEX(i.uv0, _WetNormal)));
                float3 _Ripple_var = UnpackNormal(tex2D(_Ripple,TRANSFORM_TEX(i.uv0, _Ripple)));
                float3 node_4233_nrm_base = _WetNormal_var.rgb + float3(0,0,1);
                float3 node_4233_nrm_detail = _Ripple_var.rgb * float3(-1,-1,1);
                float3 node_4233_nrm_combined = node_4233_nrm_base*dot(node_4233_nrm_base, node_4233_nrm_detail)/node_4233_nrm_base.z - node_4233_nrm_detail;
                float3 node_4233 = node_4233_nrm_combined;
                float3 node_980_nrm_base = node_498 + float3(0,0,1);
                float3 node_980_nrm_detail = lerp(_NormalClear_var.rgb,node_4233,(_Raining*_Mask_var.b)) * float3(-1,-1,1);
                float3 node_980_nrm_combined = node_980_nrm_base*dot(node_980_nrm_base, node_980_nrm_detail)/node_980_nrm_base.z - node_980_nrm_detail;
                float3 node_980 = node_980_nrm_combined;
                float3 _SnowNormal_var = UnpackNormal(tex2D(_SnowNormal,TRANSFORM_TEX(i.uv0, _SnowNormal)));
                float3 normalLocal = lerp(node_980,lerp(node_980,_SnowNormal_var.rgb,_Mask_var.g),_SnowStrenght);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); 
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
                float4 _Metallic_var = tex2D(_Metallic,TRANSFORM_TEX(i.uv0, _Metallic));
                float node_1938 = (-1.0);
                float gloss = (_Metallic_var.a*lerp(lerp(_Smoothness_dry,_Smoothness_wet,(_Raining*_Mask_var.b)),node_1938,_SnowStrenght));
                float specPow = exp2( gloss * 10.0+1.0);
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float4 _Albedo_var = tex2D(_Albedo,TRANSFORM_TEX(i.uv0, _Albedo));
                float3 node_6517 = (_Albedo_var.rgb*_AlbedoColor.rgb);
                float4 _DetailAlbedo_var = tex2D(_DetailAlbedo,TRANSFORM_TEX(i.uv0, _DetailAlbedo));
                float3 node_8328 = lerp(node_6517,_DetailAlbedo_var.rgb,node_7641);
                float4 _SnowAlbedo_var = tex2D(_SnowAlbedo,TRANSFORM_TEX(i.uv0, _SnowAlbedo));
                float3 diffuseColor = lerp(node_8328,lerp(node_8328,_SnowAlbedo_var.rgb,_SnowStrenght),_Mask_var.g); // Need this for specular when using metallic
                float specularMonochrome;
                float3 specularColor;
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, lerp(_Metallic_var.rgb,float3(_SnowAlbedo_var.a,_SnowAlbedo_var.a,_SnowAlbedo_var.a),_SnowStrenght).r, specularColor, specularMonochrome );
                specularMonochrome = 1-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * UNITY_PI / 4.0 );
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
                
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
                
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma target 3.0
            #pragma glsl
            uniform float _SnowStrenght;
            uniform sampler2D _Mask;
            uniform float _SnowAmount;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float2 uv1 : TEXCOORD2;
                float2 uv2 : TEXCOORD3;
                float4 posWorld : TEXCOORD4;
                float3 normalDir : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 _Mask_var = tex2Dlod(_Mask,float4(o.uv0,0.0,0));
                v.vertex.xyz += (lerp(float3(0,0,0),float3(_Mask_var.g,_Mask_var.g,_Mask_var.g),(_SnowStrenght*_SnowAmount))*normalize(v.normal));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma target 3.0
            #pragma glsl
            uniform float _Smoothness_dry;
            uniform sampler2D _Albedo; uniform float4 _Albedo_ST;
            uniform float4 _AlbedoColor;
            uniform sampler2D _SnowAlbedo; uniform float4 _SnowAlbedo_ST;
            uniform float _SnowStrenght;
            uniform sampler2D _Mask;
            uniform float _Raining;
            uniform float _SnowAmount;
            uniform sampler2D _Metallic; uniform float4 _Metallic_ST;
            uniform float _Smoothness_wet;
            uniform sampler2D _DetailAlbedo; uniform float4 _DetailAlbedo_ST;
            uniform float _DetailIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 _Mask_var = tex2Dlod(_Mask,float4(o.uv0,0.0,0));
                v.vertex.xyz += (lerp(float3(0,0,0),float3(_Mask_var.g,_Mask_var.g,_Mask_var.g),(_SnowStrenght*_SnowAmount))*normalize(v.normal));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = 0;
                
                float4 _Albedo_var = tex2D(_Albedo,TRANSFORM_TEX(i.uv0, _Albedo));
                float3 node_6517 = (_Albedo_var.rgb*_AlbedoColor.rgb);
                float4 _DetailAlbedo_var = tex2D(_DetailAlbedo,TRANSFORM_TEX(i.uv0, _DetailAlbedo));
                float4 _Mask_var = tex2D(_Mask,i.uv0);
                float node_7641 = (_DetailIntensity*_Mask_var.r);
                float3 node_8328 = lerp(node_6517,_DetailAlbedo_var.rgb,node_7641);
                float4 _SnowAlbedo_var = tex2D(_SnowAlbedo,TRANSFORM_TEX(i.uv0, _SnowAlbedo));
                float3 diffColor = lerp(node_8328,lerp(node_8328,_SnowAlbedo_var.rgb,_SnowStrenght),_Mask_var.g);
                float specularMonochrome;
                float3 specColor;
                float4 _Metallic_var = tex2D(_Metallic,TRANSFORM_TEX(i.uv0, _Metallic));
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, lerp(_Metallic_var.rgb,float3(_SnowAlbedo_var.a,_SnowAlbedo_var.a,_SnowAlbedo_var.a),_SnowStrenght).r, specColor, specularMonochrome );
                float node_1938 = (-1.0);
                float roughness = 1.0 - (_Metallic_var.a*lerp(lerp(_Smoothness_dry,_Smoothness_wet,(_Raining*_Mask_var.b)),node_1938,_SnowStrenght));
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}