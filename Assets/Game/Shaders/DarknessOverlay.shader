Shader "Custom/DarknessOverlay"
{
    Properties
    {
        _DarknessAlpha ("Darkness Alpha", Range(0,1)) = 0.97
    }

    SubShader
    {
        Tags
        {
            "RenderType"     = "Transparent"
            "Queue"          = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            Name  "StencilRead"
            ZWrite Off
            ZTest  Always
            Blend  SrcAlpha OneMinusSrcAlpha

            Stencil
            {
                Ref  1
                Comp NotEqual   // Solo renderiza donde stencil NO es 1
                Pass Keep
            }

            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float _DarknessAlpha;
            CBUFFER_END

            struct Attributes { float4 positionOS : POSITION; };
            struct Varyings   { float4 positionCS : SV_POSITION; };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                return half4(0.0, 0.0, 0.0, _DarknessAlpha);
            }
            ENDHLSL
        }
    }
}