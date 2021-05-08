Shader "Crashdown/EndScreenGlitchShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_GlitchOne("Glitch One", Vector) = (.5, .5, .1, .1)
		_GlitchTwo("Glitch Two", Vector) = (.6, .6, .1, .1)
		_GlitchThree("Glitch Three", Vector) = (.4, .4, .1, .1)
		_GlitchFour("Glitch Four", Vector) = (.7, .7, .1, .1)
		_GlitchFive("Glitch Five", Vector) = (.3, .3, .1, .1)
		_GlitchSix("Glitch Six", Vector) = (.8, .8, .1, .1)
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

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;

			float4 _GlitchOne;
			float4 _GlitchTwo;
			float4 _GlitchThree;
			float4 _GlitchFour;
			float4 _GlitchFive;
			float4 _GlitchSix;

			fixed4 swap(fixed4 originalColor, float2 currentPosition, float4 rectangle1, float4 rectangle2)
			{
				fixed4 newColor = originalColor;
				// Branching logic isn't the best on the GPU, but let's just make something fast that works.
				if (abs(currentPosition.x - rectangle1.x) < rectangle1.z
					&& abs(currentPosition.y - rectangle1.y) < rectangle1.w)
				{
					newColor = tex2D(_MainTex, currentPosition.xy - rectangle1.xy + rectangle2.xy);
				}
				else if (abs(currentPosition.x - rectangle2.x) < rectangle2.z
					&& abs(currentPosition.y - rectangle2.y) < rectangle2.w)
				{
					newColor = tex2D(_MainTex, currentPosition.xy - rectangle2.xy + rectangle1.xy);
				}
				return newColor;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				col = swap(col, i.uv, _GlitchOne, _GlitchTwo);
				col = swap(col, i.uv, _GlitchThree, _GlitchFour);
				col = swap(col, i.uv, _GlitchFive, _GlitchSix);
				return col;
			}

			ENDCG
		}
	}
}
