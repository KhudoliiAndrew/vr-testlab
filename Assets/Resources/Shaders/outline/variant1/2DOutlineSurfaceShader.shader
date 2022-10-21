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
            "Queue" = "Transparent+1"
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

            v2f vert(appdata_full input)
            {
                v2f o;

                float aspect = _ScreenParams.x * (_ScreenParams.w - 1); //width times 1/height

                float length = input.normal[0];
                // input.vertex
                // input.vertex.xyz += input.normal * 10 * .005f;
                input.vertex.xyz += normalize(input.normal.xyz) * (2 / aspect) * .001f;

                float4 worldPos = mul(unity_ObjectToWorld, float4(input.vertex.xyz, 1.0));
                float3 worldNormal = UnityObjectToWorldNormal(input.normal);
                worldPos.xyz += worldNormal * .001;
                o.pos = mul(UNITY_MATRIX_VP, worldPos);
                
                //o.pos = UnityObjectToClipPos(input.vertex);

                UNITY_TRANSFER_FOG(o, o.vertex);
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