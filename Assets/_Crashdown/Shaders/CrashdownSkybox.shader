// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Kobold's Keep/GradientSkyboxShader"
{
	Properties
	{
		_SkyColor("Sky Color", Color) = (1,1,1,1)
		_HorizonColor("Horizon Color", Color) = (1,1,1,1)
		_GridLineSpacing("Grid Line Spacing", float) = 200
		_GridLineWidth("Grid Line Width", Range(0, 1)) = .8
		_CameraOffsetStrength("Camera Offset Strength", Vector) = (.1, .1, .1)
		_CurrentCameraOffset("Current Camera Offset", Vector) = (0, 0, 0)
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 position : POSITION;
				float3 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 position : SV_POSITION;
				float3 texcoord : TEXCOORD0;
			};

			fixed4 _SkyColor;
			fixed4 _HorizonColor;
			float _GridLineSpacing;
			float _GridLineWidth;
			float3 _CameraOffsetStrength;
			float3 _CurrentCameraOffset;

			v2f vert(appdata v)
			{
				v2f o;
				o.position = UnityObjectToClipPos(v.position);
				o.texcoord = v.texcoord;
				
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float power = .35;

				float y = normalize(i.texcoord).y;
				float skyAmount = saturate(pow(abs(y), power));
				float horizonAmount = saturate(1 - skyAmount);
				
				float yOffset = _CameraOffsetStrength.y * _CurrentCameraOffset.y;

				// Solid lines, no aliasing
				float lineAmount =
					sign(saturate(
						sin((i.texcoord.x + _CameraOffsetStrength.x * _CurrentCameraOffset.x + yOffset) * _GridLineSpacing) - _GridLineWidth)
						+ saturate(sin((i.texcoord.z + _CameraOffsetStrength.z * _CurrentCameraOffset.z + yOffset) * _GridLineSpacing) - _GridLineWidth
					));
				//float lineAmount =
				//	1 - min(
				//		(sin(
				//			(i.texcoord.x + _CameraOffsetStrength.x * _CurrentCameraOffset.x + yOffset) * _GridLineSpacing) - _GridLineWidth),
				//		(sin(
				//			(i.texcoord.z + _CameraOffsetStrength.z * _CurrentCameraOffset.z + yOffset) * _GridLineSpacing) - _GridLineWidth));
				horizonAmount = max(horizonAmount, lineAmount);
				skyAmount = max(horizonAmount, 1.0f - lineAmount);
				fixed4 col =
					skyAmount * _SkyColor
					+ horizonAmount * _HorizonColor;
				return col;
			}
			ENDCG
		}
	}
}
