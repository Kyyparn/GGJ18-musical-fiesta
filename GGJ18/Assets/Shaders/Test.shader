﻿Shader "Custom/Test" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_RingColor("Ring Color", Color) = (1,1,1,1)
		_RingColorIntensity("Ring Color Intensity", float) = 2
		_RingSpeed("Ring Speed", float) = 1
		_RingWidth("Ring Width", float) = 0.1
		_RingIntensityScale("Ring Range", float) = 1
		_RingTex("Ring Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 200

		Pass  
		{
			ZWrite On
			ColorMask 0
		}

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha	

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _RingTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half4 _hitPts[20];
		half _StartTime;
		half _Intensity[20];

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _RingColor;
		half _RingColorIntensity;
		half _RingSpeed;
		half _RingWidth;
		half _RingIntensityScale;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;

			half DiffFromRingCol = abs(o.Albedo.r - _RingColor.r) + abs(o.Albedo.b - _RingColor.b) + abs(o.Albedo.g - _RingColor.g);

			// Check every point in the array
			// The goal is to set RGB to highest possible values based on current sonar rings
			for (int i = 0; i < 20; i++) {

				half d = distance(_hitPts[i], IN.worldPos);
				half intensity = _Intensity[i] * _RingIntensityScale;
				half val = (1 - (d / intensity));

				if (d < (_Time.y - _hitPts[i].w) * _RingSpeed && d >(_Time.y - _hitPts[i].w) * _RingSpeed - _RingWidth && val > 0) {
					half posInRing = (d - ((_Time.y - _hitPts[i].w) * _RingSpeed - _RingWidth)) / _RingWidth;

					// Calculate predicted RGB values sampling the texture radially
					float angle = acos(dot(normalize(IN.worldPos - _hitPts[i]), float3(1,0,0)));
					val *= tex2D(_RingTex, half2(1 - posInRing, angle));
					half3 tmp = _RingColor * val + c * (1 - val);

					// Determine if predicted values will be closer to the Ring color
					half tempDiffFromRingCol = abs(tmp.r - _RingColor.r) + abs(tmp.b - _RingColor.b) + abs(tmp.g - _RingColor.g);
					if (tempDiffFromRingCol < DiffFromRingCol)
					{
						// Update values using our predicted ones.
						DiffFromRingCol = tempDiffFromRingCol;
						o.Albedo.r = tmp.r;
						o.Albedo.g = tmp.g;
						o.Albedo.b = tmp.b;
						o.Albedo.rgb *= _RingColorIntensity;
					}
				}
			}

			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
