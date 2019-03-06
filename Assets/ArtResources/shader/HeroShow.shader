// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hero/Show" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "black" {}
		_MaskTex ("Mask贴图（R通道为高光系数，B通道为反射比例）", 2D) = "black" {}
    
		_LightTex ("光照贴图", 2D) = "black" {}
		_LightPower("光照贴图系数", Float) = 0.3

		_ReflectTex ("反射贴图", 2D) = "black" {}
		_ReflectColor("反射颜色", Color) = (0.9558824,0.9473512,0.9418253,1)
		_ReflectionMultiplier("反射衰减系数", float) = 2.21
		_ReflectPower("反射强度", Float) = 1.6125

		_NoiseTex ("_NoiseTex", 2D) = "black" {}
		_NoiseColor ("_NoiseColor", Color)  = (0.7205882,0.5381756,0.3073097,1)
		_Scroll2X ("_Scroll2X", float) = -5
		_Scroll2Y ("_Scroll2Y", float) = 0
		_MMultiplier ("噪点指数", float) = 5

		_RampMap ("_RampMap", 2D) = "black" {}

		_ShadowColor ("_ShadowColor", Color) = (0.2051897,0.2030911,0.2784314,1)

		_SpecColor("镜面高光颜色", Color) = (0.6672794,0.7332404,0.8897059,1)
		_SpecMultiplier("镜面高光衰减系数", Float) = 0.49
		_SpecPower("镜面高光强度", Float) = 21.41072
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass {
			Tags{"LightMode"="ForwardBase"}
			Cull Off

			CGPROGRAM
			#pragma multi_compile_fwdbase
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			//#include "Lighting.cginc"
			#include "AutoLight.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				half4 texCoord : TEXCOORD0;
				half3 viewNormal : TEXCOORD1; // 视空间Normal
				half3 viewDir : TEXCOORD2; // 世界空间视线
				float4 worldPos : TEXCOORD3; // 世界空间坐标
				float3 worldNormal : TEXCOORD4; // 世界空间法线
				SHADOW_COORDS(5)
			};

			float  _Scroll2X;
			float  _Scroll2Y;

			float  _LightPower;

			sampler2D _MainTex;
			uniform float4 _MainTex_ST;

			sampler2D _NoiseTex;
			uniform float4 _NoiseTex_ST;

			sampler2D    _RampMap;

			sampler2D _MaskTex;
			sampler2D _LightTex;

			sampler2D _ReflectTex;
			half3 _ReflectColor;
			float  _ReflectPower;
            float  _SpecPower;
			half  _ReflectionMultiplier;
			half3   _SpecColor;
            half3  _ShadowColor;
			half _SpecMultiplier;
			float4 _NoiseColor;
			float _MMultiplier;

			v2f vert (appdata v)
			{
				v2f o;

				o.pos = UnityObjectToClipPos(v.vertex);

				o.viewNormal = COMPUTE_VIEW_NORMAL;
				o.texCoord.xy = TRANSFORM_TEX(v.uv, _MainTex);
				o.texCoord.zw = TRANSFORM_TEX(v.uv, _NoiseTex) + frac(float2(_Scroll2X, _Scroll2Y) * _Time.x);

				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
  
				o.viewDir = normalize(UnityWorldSpaceViewDir(o.worldPos.xyz));

				TRANSFER_SHADOW(o);
				return o;
			}

      
			fixed4 frag (v2f i) : COLOR
			{
				// 计算光照图uv
				half3 viewNormal = normalize(i.viewNormal);
				half2 lightCoord = (viewNormal.xy * 0.5) + 0.5;
				// lightCoord=half2(lightCoord.x,-lightCoord.y);//光取反

				half3 worldNormal = normalize(i.worldNormal);
				half3 viewDir = normalize(i.viewDir);
				half3 lightDir = normalize(UnityWorldSpaceLightDir(i.worldPos.xyz));

				float3 diffuseColor = tex2D (_MainTex, i.texCoord.xy);
				float3 maskColor = tex2D (_MaskTex, i.texCoord.xy);

				// diffuse和光照图混合
				float3 color = (diffuseColor + 0.15) * (tex2D (_LightTex, lightCoord) * _LightPower).xyz;

				// 流光混合
				float3 noiseColor = tex2D (_NoiseTex, i.texCoord.zw).xyz;
				noiseColor = (noiseColor * (diffuseColor * _NoiseColor)) * (maskColor.y * _MMultiplier);
				color = (color + diffuseColor) + noiseColor;

				// 反射图混合
				float3 reflectColor = tex2D (_ReflectTex, lightCoord);
				color = lerp (color, color * pow (reflectColor * _ReflectColor, _ReflectPower) * _ReflectionMultiplier, maskColor.z);

				// 阴影衰减
				half atten = SHADOW_ATTENUATION(i);
				half  tmpvar_17 = ((atten * 0.5) + 0.5);
				
				// 高光
				half specParam = max(0, dot(worldNormal, normalize(lightDir + viewDir)));

				// diffuse
				half diffuseParam = dot(worldNormal, lightDir) * 0.5 + 0.5;
				float3 rampColor  = tex2D (_RampMap, diffuseParam).xyz;
					
				float3 finalColor = _SpecColor * (
					pow (specParam, _SpecPower) * maskColor.x * _SpecMultiplier * tmpvar_17 * 2.0) 
					+ (rampColor + 0.5) * color;
   

				finalColor = lerp ((color * _ShadowColor.xyz), finalColor, tmpvar_17);

				return fixed4(finalColor, 1);
			}

			ENDCG
		}

		Pass {  
			Tags { "LightMode"="ShadowCaster" }  

			CGPROGRAM  
			#pragma vertex vert  
			#pragma fragment frag  
			#pragma multi_compile_shadowcaster  
			#include "UnityCG.cginc"  
  
			sampler2D _Shadow;  
  
			struct v2f{  
				V2F_SHADOW_CASTER;   
			};  
  
			v2f vert(appdata_base v){  
				v2f o;  
				TRANSFER_SHADOW_CASTER_NORMALOFFSET(o);  
				return o;  
			}  
  
			float4 frag( v2f i ) : SV_Target  
			{
				SHADOW_CASTER_FRAGMENT(i)  
			}  
  
			ENDCG  
		}
	} 
}