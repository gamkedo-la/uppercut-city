Shader "Hidden/SobelOutline"
{
    Properties
    {
        [HideInInspector]
        _MainTex ("Main Texture", 2D) = "white" {}

        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineThickness ("Outline thickness", float) = 1.0

        _OutlineDepthMultiplier ("Depth Multiplier", float) = 1.0
        _OutlineDepthBias ("Depth Bias", float) = 1.0
        
        _OutlineMinDepth ("Minimum depth", Range(0, 1)) = 0.01
        _OutlineDepthSpan ("Depth span", Range(0, 1)) = 0.01

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
            sampler2D _CameraDepthNormalsTexture;
            
            float4 _OutlineColor;

            float _OutlineMinDepth;
            float _OutlineDepthSpan;
            float _OutlineThickness;
            float _OutlineDepthMultiplier;
            float _OutlineDepthBias;
            float _OutlineNormalMultiplier;
            float _OutlineNormalBias;

            float3 DepthNormal(sampler2D t, float2 uv)
            {
                float depth;
                float3 normal;
                float4 depth_texture = tex2D(_CameraDepthNormalsTexture, uv);
                DecodeDepthNormal(depth_texture, depth, normal);
                return normal;
            }

            float3 SobelSample(sampler2D t, float2 uv, float3 offset)
            {
                float3 pixelCenter = DepthNormal(t, uv);
                float3 pixelLeft = DepthNormal(t, uv - offset.xz);
                float3 pixelRight = DepthNormal(t, uv + offset.xz);
                float3 pixelUp = DepthNormal(t, uv + offset.zy);
                float3 pixelDown = DepthNormal(t, uv - offset.zy);

                return abs(pixelLeft - pixelCenter) +
                    abs(pixelRight - pixelCenter) +
                    abs(pixelUp - pixelCenter) +
                    abs(pixelDown - pixelCenter);
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
                float3 scene_color = tex2D(_MainTex, i.uv).rgb;
                float3 color = scene_color;
                
                // Modulate the outline color based on its transparency.
                float3 outline_color = lerp(color, _OutlineColor.rgb, _OutlineColor.a);
                
                // Generate an alpha value based on scene depth.
                //     >= MinDepth + DepthSpan = No outline
                //      > MinDepth             = Partial outline
                //     <= MinDepth             = Full outline
                float depth01   = Linear01Depth(tex2D(_CameraDepthTexture, i.uv).r);
                float minDepth  = _OutlineMinDepth;
                float depthSpan = _OutlineDepthSpan;
                float alpha     = lerp(1.0, 0.0, (clamp(depth01, minDepth, minDepth + depthSpan) - minDepth) / depthSpan);

                // If object is past outline distance, don't add outline.
                if (alpha <= 0.0)
                {
                    return float4(color, 1.0);
                }

                // Calculate depth-based outline.
                float3 offset = float3(
                    1.0 / _ScreenParams.x,
                    1.0 / _ScreenParams.y,
                    0.0) * _OutlineThickness;
                float sobel_depth = SobelSampleDepth(_CameraDepthTexture, i.uv, offset);
                sobel_depth = pow(sobel_depth * _OutlineDepthMultiplier, _OutlineDepthBias);

                // Calculate normal-based outline.
                float3 sobel_normal_vec = SobelSample(_CameraDepthNormalsTexture, i.uv, offset).rgb;
                float sobel_normal = sobel_normal_vec.x + sobel_normal_vec.y + sobel_normal_vec.z;
                sobel_normal = pow(sobel_normal * _OutlineNormalMultiplier, _OutlineNormalBias);

                // Combine outlines.
                float sobel_outline = saturate(max(sobel_depth, sobel_normal));
                sobel_outline = sobel_outline * alpha;
                
                // Calculate final outline color.
                color = lerp(color, outline_color, sobel_outline);

                return float4(color, 1.0);
            }
            ENDCG
        }
    }
}