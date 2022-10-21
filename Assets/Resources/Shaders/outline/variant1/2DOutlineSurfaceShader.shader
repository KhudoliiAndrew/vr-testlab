Shader "Custom/2DOutlineSurfaceShader"
{
    Properties
    {
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 0

        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Outline Thickness", Range(0,10)) = .5
    }
    SubShader
    {
        Tags
        {
            "Queue" = "1"
            "RenderType" = "Transparent"
            // "DisableBatching" = "True"
        }
        //LOD 200

        // render outline

        Pass
        {
            Cull Back
            ZWrite On
            ZTest [_ZTest]

            Stencil
            {
                Ref 1
                Comp NotEqual
            }

            CGPROGRAM
            //include useful shader functions
            #include "UnityCG.cginc"

            //define vertex and fragment shader
            #pragma vertex vert
            #pragma fragment frag

            half _OutlineThickness;
            fixed4 _OutlineColor;

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata_base input)
            {
                v2f o;
                // input.vertex.xyz += input.normal * 10 * .005f;
                input.vertex.xyz += 1 * 10 * .005f;
                o.pos = UnityObjectToClipPos(input.vertex);

                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }
    FallBack "Standard"
}