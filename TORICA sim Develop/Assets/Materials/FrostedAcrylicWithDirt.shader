Shader "Unlit/FrostedGlass"
{
    Properties
    {
        [MainTexture] _MainTex ("Texture (Albedo)", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _BlurAmount ("Blur Amount", Range(0, 10)) = 1.0
        _Alpha ("Alpha", Range(0, 1)) = 0.5
        
        // For PBR lighting model (optional, but good practice)
        _Glossiness ("Smoothness", Range(0.0, 1.0)) = 0.5
        _Metallic ("Metallic", Range(0.0, 1.0)) = 0.0
    }
    SubShader
    {
        Tags 
        { 
            "RenderPipeline" = "UniversalPipeline"
            "Queue"="Transparent" 
            "RenderType"="Transparent" 
        }

        // ポリゴンの裏表両方を描画する設定
        Cull Off

        Pass
        {
            // レンダリング設定
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // URPのコアライブラリをインクルード
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            // シェーダープロパティの宣言
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            half4 _MainTex_ST;
            
            half4 _Color;
            float _BlurAmount;
            half _Alpha;

            // URPが提供する背景テクスチャ
            TEXTURE2D(_CameraColorTexture);
            SAMPLER(sampler_CameraColorTexture);

            // 頂点シェーダーの入力
            struct Attributes
            {
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0;
            };

            // フラグメントシェーダーへの入力
            struct Varyings
            {
                float4 positionCS   : SV_POSITION;
                float2 uv           : TEXCOORD0;
                float4 screenPos    : TEXCOORD1; // 画面上の座標
            };

            // 頂点シェーダー
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                OUT.screenPos = ComputeScreenPos(OUT.positionCS);
                return OUT;
            }

            // フラグメントシェーダー
            half4 frag(Varyings IN) : SV_Target
            {
                float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
                float blur = _BlurAmount * 0.001; // ループで使うため、係数は少し小さめに戻す

                half4 finalColor = 0;
                int sampleCount = 0;

                // 5x5の範囲をループでサンプリング
                for (int x = -2; x <= 2; x++)
                {
                    for (int y = -2; y <= 2; y++)
                    {
                        float2 offset = float2(x, y) * blur;
                        finalColor += SAMPLE_TEXTURE2D(_CameraColorTexture, sampler_CameraColorTexture, screenUV + offset);
                        sampleCount++;
                    }
                }
                half4 blurredBackground = finalColor / sampleCount;

                // ガラス本体の色の計算
                half4 glassTexture = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
                half4 glassColor = glassTexture * _Color;

                // 背景とガラスの合成
                half3 blendedColor = lerp(blurredBackground.rgb, glassColor.rgb, glassColor.a);

                return half4(blendedColor, _Alpha);
            }            
            ENDHLSL
        }
    }
}