Shader "Hidden/SobelOutline"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
//        _CameraGBufferTexture2 ("Camera GBuffer Texture 2", 2D) = "white" {}

        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineThickness ("Outline thickness", float) = 1.0
        _OutlineDensity ("Outline Density", float) = 1.0

        _OutlineDepthMultiplier ("Depth Multiplier", float) = 1.0
        _OutlineDepthBias ("Depth Bias", float) = 1.0

        _OutlineNormalMultiplier ("Normal Multiplier", float) = 1.0
        _OutlineNormalBias ("Normal Bias", float) = 1.0

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
            sampler2D _CameraDepthTexture;
            // sampler2D _CameraGBufferTexture2;
            float4 _OutlineColor;

            float _OutlineThickness;
            float _OutlineDepthMultiplier;
            float _OutlineDepthBias;
            float _OutlineNormalMultiplier;
            float _OutlineNormalBias;
            float _OutlineDensity;

            float SobelSample(sampler2D t, float2 uv, float3 offset)
            {
                return 0.0;
            }

            float SobelDepth(float ldc, float ldl, float ldr, float ldu, float ldd)
            {
                return abs(ldl - ldc) +
                    abs(ldr - ldc) +
                    abs(ldu - ldc) +
                    abs(ldd - ldc);
            }

            float SobelSampleDepth(sampler2D t, float2 uv, float3 offset)
            {
                float pixelCenter = LinearEyeDepth(tex2D(t, uv).r);
                float pixelLeft = LinearEyeDepth(tex2D(t, uv - offset.xz).r);
                float pixelRight = LinearEyeDepth(tex2D(t, uv + offset.xz).r);
                float pixelUp = LinearEyeDepth(tex2D(t, uv + offset.zy).r);
                float pixelDown = LinearEyeDepth(tex2D(t, uv - offset.zy).r);

                // return 0;
                return SobelDepth(pixelCenter, pixelLeft, pixelRight, pixelUp, pixelDown);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 SceneColor = tex2D(_MainTex, i.uv).rgb;
                float3 color = SceneColor;

                float3 offset = float3(
                    1.0 / _ScreenParams.x,
                    1.0 / _ScreenParams.y,
                    0.0) * _OutlineThickness;

                float sobelDepth = SobelSampleDepth(_CameraDepthTexture, i.uv, offset);

                return fixed4(sobelDepth, sobelDepth, sobelDepth, 1);
            }
            ENDCG
        }
    }
}