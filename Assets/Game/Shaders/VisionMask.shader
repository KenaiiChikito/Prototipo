Shader "Custom/VisionMask"
{
    SubShader
    {
        Tags
        {
            "RenderType"     = "Opaque"
            "Queue"          = "Transparent-1"
            "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            Name    "StencilWrite"
            ZWrite  Off
            ZTest   Always     // Siempre escribe, sin importar profundidad
            ColorMask 0        // No escribe ningún color (completamente invisible)

            Stencil
            {
                Ref  1
                Comp Always
                Pass Replace   // Escribe 1 en cada pixel del mesh
            }

            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes { float4 positionOS : POSITION; };
            struct Varyings   { float4 positionCS : SV_POSITION; };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target { return 0; }
            ENDHLSL
        }
    }
}