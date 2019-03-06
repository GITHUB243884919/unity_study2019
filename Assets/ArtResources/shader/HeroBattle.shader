// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

//改变高光范围可改变梯度图方向
Shader "Hero/Battle" 
{
	Properties {
		_MainTex ("_MainTex", 2D) =  "bump" {}
		_LightTex ("_LightTex", 2D) =  "bump" {}
		_LightScale ("_LightScale", Range(0.0,10)) = 2.3

		_Color ("Main Color", Color) = (1,1,1,1)

		// Rim
		_RimColor ("Rim Color", Color) = (1,1,1,0.5)
		_RimWidth ("Rim Width", Range(0.0,1.0)) = 0

		// Outline
		_OutlineColor ("Outline Color", Color) = (1,1,1,0.5)
		_OutlineWidth ("Outline Width", Range(0.0,0.5)) = 0
	}

	SubShader {
		Tags { "RenderType"="Opaque" }

		//描边
	    Pass 
		{  
	        Name "OUTLINE"  
	        Cull Front  
	        ZWrite Off

			CGPROGRAM  
			#pragma vertex vert  
			#pragma fragment frag
			#include "UnityCG.cginc"

			fixed4 _OutlineColor;
			fixed _OutlineWidth;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				fixed4 color : COLOR;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);

				float3 norm = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
				float2 offset = TransformViewToProjection(norm.xy);

				o.pos.xy += offset * _OutlineWidth;
				o.color = _OutlineColor;
				return o;
			}

			fixed4 frag(v2f i) : COLOR 
			{  
				return i.color - 0.3;
			}  
			ENDCG  
	    }
		
		Pass {
			Cull Off

			CGPROGRAM
		    #pragma multi_compile_fwdbase  
			#pragma vertex vert
			#pragma fragment frag
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			struct a2v {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
		        half4 uv : TEXCOORD0;
			};
			
			struct v2f {
				float4 pos : SV_POSITION;
				half2 texCoord : TEXCOORD0;  
				half3 viewNormal : TEXCOORD1; // 视空间Normal 
				fixed4 rimColor: TEXCOORD2; // rim颜色
			};

			fixed4 _Color;

			fixed4 _RimColor;
			fixed _RimWidth;
			
			v2f vert(a2v v) 
			{
				v2f o;

				o.pos = UnityObjectToClipPos(v.vertex);
				o.texCoord = v.uv.xy;

				o.viewNormal = COMPUTE_VIEW_NORMAL;

				float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
				float dotProduct = max(0, 1 - dot(v.normal, viewDir));
				o.rimColor = dotProduct * sin(_RimWidth*1.57) * _RimColor;

				return o;

			}

			sampler2D _MainTex;
			sampler2D _LightTex;
			half _LightScale;

			fixed4 frag(v2f i) : COLOR 
			{
		    	fixed3 color = tex2D (_MainTex, i.texCoord);

		    	half2 lightCoord = ((normalize(i.viewNormal).xy * 0.5) + 0.5);
		    	fixed3 lightColor = tex2D (_LightTex, lightCoord);

		    	color *= lightColor * _LightScale;
				color *= _Color;
				color += i.rimColor;

				return fixed4(color, 0); 
			}
			ENDCG
		}
	} 
	//FallBack "Diffuse"
}
