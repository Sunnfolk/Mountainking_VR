Shader "Custom/Effects/Wave"{

	Properties{
		_MainTexture("MainTexture", 2D) = "white" {}
		_Color("Color", Color) = (0, 0, 0, 1)
		_Strength("Strength", Range(0, 2)) = 1.0
		_Speed("Speed", Range(-200, 200)) = 100
		_Transparency("Transparency", Range(0.0, 1.0)) = 0.25
	}

		SubShader{
			Tags{"Queue" = "Transparent" "RenderType" = "Transparent"
			}

			Blend SrcAlpha OneMinusSrcAlpha

			Pass{
			Cull off

			CGPROGRAM

			#pragma vertex vertexFunc
			#pragma fragment fragmentFunc

			#include "Unitycg.cginc"

			float4 _Color;
			float _Strength;
			float _Speed;
			float _Transparency;

			sampler2D _MainTexture;

			struct vertexInput {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct vertexOutput {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			vertexOutput vertexFunc(vertexInput IN) {
				vertexOutput o;

				float4 worldPos = mul(unity_ObjectToWorld, IN.vertex);

				float displacement = (cos(worldPos.y) + cos(worldPos.x + (_Speed * _Time)));
				worldPos.y = worldPos.y + (displacement * _Strength);

				o.pos = mul(UNITY_MATRIX_VP, worldPos);
				o.uv = IN.uv;

				return o;
			}

			float4 fragmentFunc(vertexOutput IN) : COLOR {
				fixed4 textureColor = tex2D(_MainTexture, IN.uv);
				textureColor.a = _Transparency;

				return textureColor * _Color;
			}

			ENDCG
			}
		}
}
