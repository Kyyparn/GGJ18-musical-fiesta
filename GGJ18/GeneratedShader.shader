Shader "PBR Master"
{
	Properties
	{
				[NonModifiableTextureData] [NoScaleOffset] _Texture2D_C6B39A36_Tex("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags{ "RenderPipeline" = "LightweightPipeline"}
		Tags
		{
			"RenderType"="Opaque"
			"Queue"="Geometry"
		}
		Pass
		{
			Tags{"LightMode" = "LightweightForward"}
			
					Blend One Zero
					Cull Back
					ZTest LEqual
					ZWrite On
			
			CGPROGRAM
			#pragma target 3.0
			
		    #pragma multi_compile _ _MAIN_LIGHT_COOKIE
		    #pragma multi_compile _MAIN_DIRECTIONAL_LIGHT _MAIN_SPOT_LIGHT
		    #pragma multi_compile _ _ADDITIONAL_LIGHTS
		    #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
		    #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO STEREO_INSTANCING_ON STEREO_MULTIVIEW_ON
		    #pragma multi_compile _ LIGHTMAP_ON
		    #pragma multi_compile _ DIRLIGHTMAP_COMBINED
		    #pragma multi_compile _ _HARD_SHADOWS _SOFT_SHADOWS _HARD_SHADOWS_CASCADES _SOFT_SHADOWS_CASCADES
		    #pragma multi_compile _ _VERTEX_LIGHTS
		    #pragma multi_compile_fog
		    #pragma multi_compile_instancing
		    #pragma vertex vert
			#pragma fragment frag
			
			#pragma glsl
			#pragma debug
			
						#define _NORMALMAP 1
			#include "LightweightLighting.cginc"
								void Unity_Multiply_float(float2 A, float2 B, out float2 Out)
							{
							    Out = A * B;
							}
							void Unity_Sine_float(float2 In, out float2 Out)
							{
							    Out = sin(In);
							}
							void Unity_Smoothstep_float(float2 A, float2 B, float2 T, out float2 Out)
							{
							    Out = smoothstep(A, B, T);
							}
							void Unity_Power_float(float2 A, float2 B, out float2 Out)
							{
							    Out = pow(A, B);
							}
							void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
							{
							    Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
							}
							void Unity_RadialShear_float(float2 UV, float2 Center, float2 Strength, float2 Offset, out float2 Out)
							{
							    float2 delta = UV - Center;
							    float delta2 = dot(delta.xy, delta.xy);
							    float2 delta_offset = delta2 * Strength;
							    Out = UV + float2(delta.y, -delta.x) * delta_offset + Offset;
							}
							void Unity_Twirl_float(float2 UV, float2 Center, float Strength, float2 Offset, out float2 Out)
							{
							    float2 delta = UV - Center;
							    float angle = Strength * length(delta);
							    float x = cos(angle) * delta.x - sin(angle) * delta.y;
							    float y = sin(angle) * delta.x + cos(angle) * delta.y;
							    Out = float2(x + Center.x + Offset.x, y + Center.y + Offset.y);
							}
							void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA)
							{
							    RGBA = float4(R, G, B, A);
							}
							void Unity_Normalize_float(float4 In, out float4 Out)
							{
							    Out = normalize(In);
							}
							struct GraphVertexInput
							{
								float4 vertex : POSITION;
								float3 normal : NORMAL;
								float4 tangent : TANGENT;
								float4 texcoord0 : TEXCOORD0;
								float4 texcoord1 : TEXCOORD1;
								UNITY_VERTEX_INPUT_INSTANCE_ID
							};
							struct SurfaceInputs{
								half4 uv0;
							};
							struct SurfaceDescription{
								float3 Albedo;
								float3 Normal;
								float3 Emission;
								float Metallic;
								float Smoothness;
								float Occlusion;
								float Alpha;
							};
							float4 _Smoothstep_51E1C736_A;
							float4 _Smoothstep_51E1C736_B;
							float4 _Power_CE2DE142_B;
							float4 _Remap_78352590_InMinMax;
							float4 _Remap_78352590_OutMinMax;
							float4 _RadialShear_B71614D2_Center;
							float4 _RadialShear_B71614D2_Offset;
							float4 _Remap_6B23285E_InMinMax;
							float4 _Remap_6B23285E_OutMinMax;
							float4 _Twirl_F6B80C59_Center;
							float4 _Twirl_F6B80C59_Offset;
							float _Combine_D40D5C69_B;
							float _Combine_D40D5C69_A;
							UNITY_DECLARE_TEX2D(_Texture2D_C6B39A36_Tex);
							float4 _Texture2D_C6B39A36_UV;
							float4 _PBRMaster_17496E7C_Emission;
							float _PBRMaster_17496E7C_Metallic;
							float _PBRMaster_17496E7C_Smoothness;
							float _PBRMaster_17496E7C_Occlusion;
							float _PBRMaster_17496E7C_Alpha;
							GraphVertexInput PopulateVertexData(GraphVertexInput v){
								return v;
							}
							SurfaceDescription PopulateSurfaceData(SurfaceInputs IN) {
								SurfaceDescription surface = (SurfaceDescription)0;
								half4 uv0 = IN.uv0;
								float Constant_96A84D50 = 3,141593;
								float4 _UV_6E42CE1C_Out = uv0;
								float2 _Multiply_3F8C2353_Out;
								Unity_Multiply_float((Constant_96A84D50.xx), _UV_6E42CE1C_Out, _Multiply_3F8C2353_Out);
								float2 _Sine_9765E796_Out;
								Unity_Sine_float(_Multiply_3F8C2353_Out, _Sine_9765E796_Out);
								float2 _Smoothstep_51E1C736_Out;
								Unity_Smoothstep_float(_Smoothstep_51E1C736_A, _Smoothstep_51E1C736_B, _Sine_9765E796_Out, _Smoothstep_51E1C736_Out);
								float2 _Power_CE2DE142_Out;
								Unity_Power_float(_Smoothstep_51E1C736_Out, _Power_CE2DE142_B, _Power_CE2DE142_Out);
								float _Remap_78352590_Out;
								Unity_Remap_float(_CosTime.w, _Remap_78352590_InMinMax, _Remap_78352590_OutMinMax, _Remap_78352590_Out);
								float2 _RadialShear_B71614D2_Out;
								Unity_RadialShear_float(_Power_CE2DE142_Out, _RadialShear_B71614D2_Center, (_Remap_78352590_Out.xx), _RadialShear_B71614D2_Offset, _RadialShear_B71614D2_Out);
								float _Remap_6B23285E_Out;
								Unity_Remap_float(_SinTime.w, _Remap_6B23285E_InMinMax, _Remap_6B23285E_OutMinMax, _Remap_6B23285E_Out);
								float2 _Twirl_F6B80C59_Out;
								Unity_Twirl_float(_RadialShear_B71614D2_Out, _Twirl_F6B80C59_Center, _Remap_6B23285E_Out, _Twirl_F6B80C59_Offset, _Twirl_F6B80C59_Out);
								float _Split_55EBDB3A_R = _Twirl_F6B80C59_Out[0];
								float _Split_55EBDB3A_G = _Twirl_F6B80C59_Out[1];
								float _Split_55EBDB3A_B = 0;
								float _Split_55EBDB3A_A = 0;
								float4 _Combine_D40D5C69_RGBA;
								Unity_Combine_float(_Split_55EBDB3A_R, _Split_55EBDB3A_G, _Combine_D40D5C69_B, _Combine_D40D5C69_A, _Combine_D40D5C69_RGBA);
								float4 _Normalize_28B0400C_Out;
								Unity_Normalize_float(_Combine_D40D5C69_RGBA, _Normalize_28B0400C_Out);
								float4 _Texture2D_C6B39A36_RGBA = UNITY_SAMPLE_TEX2D(_Texture2D_C6B39A36_Tex,uv0.xy);
								_Texture2D_C6B39A36_RGBA.rgb = UnpackNormal(_Texture2D_C6B39A36_RGBA);
								float _Texture2D_C6B39A36_R = _Texture2D_C6B39A36_RGBA.r;
								float _Texture2D_C6B39A36_G = _Texture2D_C6B39A36_RGBA.g;
								float _Texture2D_C6B39A36_B = _Texture2D_C6B39A36_RGBA.b;
								float _Texture2D_C6B39A36_A = _Texture2D_C6B39A36_RGBA.a;
								surface.Albedo = _Normalize_28B0400C_Out;
								surface.Normal = _Texture2D_C6B39A36_RGBA;
								surface.Emission = _PBRMaster_17496E7C_Emission;
								surface.Metallic = _PBRMaster_17496E7C_Metallic;
								surface.Smoothness = _PBRMaster_17496E7C_Smoothness;
								surface.Occlusion = _PBRMaster_17496E7C_Occlusion;
								surface.Alpha = _PBRMaster_17496E7C_Alpha;
								return surface;
							}
			
			struct GraphVertexOutput
		    {
		        float4 position : SV_POSITION;
		#ifdef LIGHTMAP_ON
		        float4 lightmapUV : TEXCOORD0;
		#else
				float4 vertexSH : TEXCOORD0;
		#endif
				half4 fogFactorAndVertexLight : TEXCOORD1; // x: fogFactor, yzw: vertex light
		        			float3 WorldSpaceNormal : TEXCOORD3;
					float3 WorldSpaceTangent : TEXCOORD4;
					float3 WorldSpaceBiTangent : TEXCOORD5;
					float3 WorldSpaceViewDirection : TEXCOORD6;
					float3 WorldSpacePosition : TEXCOORD7;
					half4 uv0 : TEXCOORD8;
					half4 uv1 : TEXCOORD9;
				UNITY_VERTEX_OUTPUT_STEREO
		    };
			
		    GraphVertexOutput vert (GraphVertexInput v)
			{
			    v = PopulateVertexData(v);
				
				UNITY_SETUP_INSTANCE_ID(v);
		        GraphVertexOutput o = (GraphVertexOutput)0;
		        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		        			o.WorldSpaceNormal = mul(v.normal,(float3x3)unity_WorldToObject);
					o.WorldSpaceTangent = mul((float3x3)unity_ObjectToWorld,v.tangent);
					o.WorldSpaceBiTangent = normalize(cross(o.WorldSpaceNormal, o.WorldSpaceTangent.xyz) * v.tangent.w);
					o.WorldSpaceViewDirection = mul((float3x3)unity_ObjectToWorld,ObjSpaceViewDir(v.vertex));
					o.WorldSpacePosition = mul(unity_ObjectToWorld,v.vertex);
					o.uv0 = v.texcoord0;
					o.uv1 = v.texcoord1;
				float3 lwWNormal = normalize(UnityObjectToWorldNormal(v.normal));
				float4 lwWorldPos = mul(unity_ObjectToWorld, v.vertex);
				float4 clipPos = mul(UNITY_MATRIX_VP, lwWorldPos);
		#ifdef LIGHTMAP_ON
				o.lightmapUV.zw = v.texcoord1 * unity_LightmapST.xy + unity_LightmapST.zw;
		#else
				o.vertexSH = half4(EvaluateSHPerVertex(lwWNormal), 0.0);
		#endif
				o.fogFactorAndVertexLight.yzw = VertexLighting(lwWorldPos.xyz, lwWNormal);
				o.fogFactorAndVertexLight.x = ComputeFogFactor(clipPos.z);
		        o.position = clipPos;
				return o;
			}
			fixed4 frag (GraphVertexOutput IN) : SV_Target
		    {
		    				float3 WorldSpaceNormal = normalize(IN.WorldSpaceNormal);
					float3 WorldSpaceTangent = IN.WorldSpaceTangent;
					float3 WorldSpaceBiTangent = IN.WorldSpaceBiTangent;
					float3 WorldSpaceViewDirection = normalize(IN.WorldSpaceViewDirection);
					float3 WorldSpacePosition = IN.WorldSpacePosition;
					float4 uv0  = IN.uv0;
					float4 uv1  = IN.uv1;
		        SurfaceInputs surfaceInput = (SurfaceInputs)0;
		        			surfaceInput.uv0 = uv0;
		        SurfaceDescription surf = PopulateSurfaceData(surfaceInput);
				float3 Albedo = float3(0.5, 0.5, 0.5);
				float3 Specular = float3(0, 0, 0);
				float Metallic = 0;
				float3 Normal = float3(0, 0, 1);
				float3 Emission = 0;
				float Smoothness = 0.5;
				float Occlusion = 1;
				float Alpha = 1;
		        			Albedo = surf.Albedo;
					Normal = surf.Normal;
					Emission = surf.Emission;
					Metallic = surf.Metallic;
					Smoothness = surf.Smoothness;
					Occlusion = surf.Occlusion;
					Alpha = surf.Alpha;
		#if defined(UNITY_COLORSPACE_GAMMA) 
		       	Albedo = Albedo * Albedo;
		       	Emission = Emission * Emission;
		#endif
		#if _NORMALMAP
		    half3 normalWS = TangentToWorldNormal(Normal, WorldSpaceTangent, WorldSpaceBiTangent, WorldSpaceNormal);
		#else
		    half3 normalWS = normalize(WorldSpaceNormal);
		#endif
		#if LIGHTMAP_ON
			half3 indirectDiffuse = SampleLightmap(IN.lightmapUV.zw, normalWS);
		#else
			half3 indirectDiffuse = EvaluateSHPerPixel(normalWS, IN.vertexSH);
		#endif
			half4 color = LightweightFragmentPBR(
					WorldSpacePosition,
					normalWS,
					WorldSpaceViewDirection,
					indirectDiffuse,
					IN.fogFactorAndVertexLight.yzw, 
					Albedo,
					Metallic,
					Specular,
					Smoothness,
					Occlusion,
					Emission,
					Alpha);
			// Computes fog factor per-vertex
		    ApplyFog(color.rgb, IN.fogFactorAndVertexLight.x);
		#if _AlphaOut
				color.a = Alpha;
		#else
				color.a = 1;
		#endif
		#if _AlphaClip
				clip(Alpha - 0.01);
		#endif
				return color;
		    }
			ENDCG
		}
		Pass
		{
		    Tags{"LightMode" = "ShadowCaster"}
		    ZWrite On ZTest LEqual
		    CGPROGRAM
		    #pragma target 2.0
		    #pragma vertex ShadowPassVertex
		    #pragma fragment ShadowPassFragment
		    #include "UnityCG.cginc"
		    #include "LightweightPassShadow.cginc"
		    ENDCG
		}
		Pass
		{
		    Tags{"LightMode" = "DepthOnly"}
		    ZWrite On
		    ColorMask 0
		    CGPROGRAM
		    #pragma target 2.0
		    #pragma vertex vert
		    #pragma fragment frag
		    #include "UnityCG.cginc"
		    float4 vert(float4 pos : POSITION) : SV_POSITION
		    {
		        return UnityObjectToClipPos(pos);
		    }
		    half4 frag() : SV_TARGET
		    {
		        return 0;
		    }
		    ENDCG
		}
	}
}
