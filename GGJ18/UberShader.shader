Shader "hidden/preview"
{
	Properties
	{
				[NonModifiableTextureData] [NoScaleOffset] _Texture2D_C6B39A36_Tex("Texture", 2D) = "white" {}
	}
	CGINCLUDE
	#include "UnityCG.cginc"
			void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
			{
			    Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
			}
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
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			struct SurfaceInputs{
				half4 uv0;
			};
			struct SurfaceDescription{
				float4 PreviewOutput;
			};
			float Float_36674656;
			float4 _Remap_6B23285E_InMinMax;
			float4 _Remap_6B23285E_OutMinMax;
			float4 _Remap_78352590_InMinMax;
			float4 _Remap_78352590_OutMinMax;
			float4 _Smoothstep_51E1C736_A;
			float4 _Smoothstep_51E1C736_B;
			float4 _Power_CE2DE142_B;
			float4 _RadialShear_B71614D2_Center;
			float4 _RadialShear_B71614D2_Offset;
			float4 _Twirl_F6B80C59_Center;
			float4 _Twirl_F6B80C59_Offset;
			float _Combine_D40D5C69_B;
			float _Combine_D40D5C69_A;
			UNITY_DECLARE_TEX2D(_Texture2D_C6B39A36_Tex);
			float4 _Texture2D_C6B39A36_UV;
			GraphVertexInput PopulateVertexData(GraphVertexInput v){
				return v;
			}
			SurfaceDescription PopulateSurfaceData(SurfaceInputs IN) {
				SurfaceDescription surface = (SurfaceDescription)0;
				half4 uv0 = IN.uv0;
				float _Remap_6B23285E_Out;
				Unity_Remap_float(_SinTime.w, _Remap_6B23285E_InMinMax, _Remap_6B23285E_OutMinMax, _Remap_6B23285E_Out);
				if (Float_36674656 == 0) { surface.PreviewOutput = half4(_Remap_6B23285E_Out, _Remap_6B23285E_Out, _Remap_6B23285E_Out, 1.0); return surface; }
				float _Remap_78352590_Out;
				Unity_Remap_float(_CosTime.w, _Remap_78352590_InMinMax, _Remap_78352590_OutMinMax, _Remap_78352590_Out);
				if (Float_36674656 == 1) { surface.PreviewOutput = half4(_Remap_78352590_Out, _Remap_78352590_Out, _Remap_78352590_Out, 1.0); return surface; }
				float Constant_96A84D50 = 3,141593;
				float4 _UV_6E42CE1C_Out = uv0;
				if (Float_36674656 == 2) { surface.PreviewOutput = half4(_UV_6E42CE1C_Out.x, _UV_6E42CE1C_Out.y, 0.0, 1.0); return surface; }
				float2 _Multiply_3F8C2353_Out;
				Unity_Multiply_float((Constant_96A84D50.xx), _UV_6E42CE1C_Out, _Multiply_3F8C2353_Out);
				if (Float_36674656 == 3) { surface.PreviewOutput = half4(_Multiply_3F8C2353_Out.x, _Multiply_3F8C2353_Out.y, 0.0, 1.0); return surface; }
				float2 _Sine_9765E796_Out;
				Unity_Sine_float(_Multiply_3F8C2353_Out, _Sine_9765E796_Out);
				if (Float_36674656 == 4) { surface.PreviewOutput = half4(_Sine_9765E796_Out.x, _Sine_9765E796_Out.y, 0.0, 1.0); return surface; }
				float2 _Smoothstep_51E1C736_Out;
				Unity_Smoothstep_float(_Smoothstep_51E1C736_A, _Smoothstep_51E1C736_B, _Sine_9765E796_Out, _Smoothstep_51E1C736_Out);
				if (Float_36674656 == 5) { surface.PreviewOutput = half4(_Smoothstep_51E1C736_Out.x, _Smoothstep_51E1C736_Out.y, 0.0, 1.0); return surface; }
				float2 _Power_CE2DE142_Out;
				Unity_Power_float(_Smoothstep_51E1C736_Out, _Power_CE2DE142_B, _Power_CE2DE142_Out);
				if (Float_36674656 == 6) { surface.PreviewOutput = half4(_Power_CE2DE142_Out.x, _Power_CE2DE142_Out.y, 0.0, 1.0); return surface; }
				float2 _RadialShear_B71614D2_Out;
				Unity_RadialShear_float(_Power_CE2DE142_Out, _RadialShear_B71614D2_Center, (_Remap_78352590_Out.xx), _RadialShear_B71614D2_Offset, _RadialShear_B71614D2_Out);
				if (Float_36674656 == 7) { surface.PreviewOutput = half4(_RadialShear_B71614D2_Out.x, _RadialShear_B71614D2_Out.y, 0.0, 1.0); return surface; }
				float2 _Twirl_F6B80C59_Out;
				Unity_Twirl_float(_RadialShear_B71614D2_Out, _Twirl_F6B80C59_Center, _Remap_6B23285E_Out, _Twirl_F6B80C59_Offset, _Twirl_F6B80C59_Out);
				if (Float_36674656 == 8) { surface.PreviewOutput = half4(_Twirl_F6B80C59_Out.x, _Twirl_F6B80C59_Out.y, 0.0, 1.0); return surface; }
				float _Split_55EBDB3A_R = _Twirl_F6B80C59_Out[0];
				float _Split_55EBDB3A_G = _Twirl_F6B80C59_Out[1];
				float _Split_55EBDB3A_B = 0;
				float _Split_55EBDB3A_A = 0;
				float4 _Combine_D40D5C69_RGBA;
				Unity_Combine_float(_Split_55EBDB3A_R, _Split_55EBDB3A_G, _Combine_D40D5C69_B, _Combine_D40D5C69_A, _Combine_D40D5C69_RGBA);
				if (Float_36674656 == 9) { surface.PreviewOutput = half4(_Combine_D40D5C69_RGBA.x, _Combine_D40D5C69_RGBA.y, _Combine_D40D5C69_RGBA.z, 1.0); return surface; }
				float4 _Normalize_28B0400C_Out;
				Unity_Normalize_float(_Combine_D40D5C69_RGBA, _Normalize_28B0400C_Out);
				if (Float_36674656 == 10) { surface.PreviewOutput = half4(_Normalize_28B0400C_Out.x, _Normalize_28B0400C_Out.y, _Normalize_28B0400C_Out.z, 1.0); return surface; }
				float4 _Texture2D_C6B39A36_RGBA = UNITY_SAMPLE_TEX2D(_Texture2D_C6B39A36_Tex,uv0.xy);
				_Texture2D_C6B39A36_RGBA.rgb = UnpackNormal(_Texture2D_C6B39A36_RGBA);
				float _Texture2D_C6B39A36_R = _Texture2D_C6B39A36_RGBA.r;
				float _Texture2D_C6B39A36_G = _Texture2D_C6B39A36_RGBA.g;
				float _Texture2D_C6B39A36_B = _Texture2D_C6B39A36_RGBA.b;
				float _Texture2D_C6B39A36_A = _Texture2D_C6B39A36_RGBA.a;
				if (Float_36674656 == 11) { surface.PreviewOutput = half4(_Texture2D_C6B39A36_RGBA.x, _Texture2D_C6B39A36_RGBA.y, _Texture2D_C6B39A36_RGBA.z, 1.0); return surface; }
				return surface;
			}
	ENDCG
	SubShader
	{
	    Tags { "RenderType"="Opaque" }
	    LOD 100
	    Pass
	    {
	        CGPROGRAM
	        #pragma vertex vert
	        #pragma fragment frag
	        #include "UnityCG.cginc"
	        struct GraphVertexOutput
	        {
	            float4 position : POSITION;
	            half4 uv0 : TEXCOORD;
	        };
	        GraphVertexOutput vert (GraphVertexInput v)
	        {
	            v = PopulateVertexData(v);
	            GraphVertexOutput o;
	            o.position = UnityObjectToClipPos(v.vertex);
	            o.uv0 = v.texcoord0;
	            return o;
	        }
	        fixed4 frag (GraphVertexOutput IN) : SV_Target
	        {
	            float4 uv0  = IN.uv0;
	            SurfaceInputs surfaceInput = (SurfaceInputs)0;;
	            surfaceInput.uv0 = uv0;
	            SurfaceDescription surf = PopulateSurfaceData(surfaceInput);
	            return surf.PreviewOutput;
	        }
	        ENDCG
	    }
	}
}
