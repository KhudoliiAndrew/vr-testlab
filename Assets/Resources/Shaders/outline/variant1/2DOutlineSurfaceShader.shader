Shader "Custom/2DOutlineSurfaceShader"
{
    Properties
    {
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 0

        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent+1"
            "RenderType" = "Transparent"
        }

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
            #pragma multi_compile_fog

            fixed4 _OutlineColor;

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata_base input)
            {
                v2f o;

                const float3 world_scale = float3(
                    // scale x axis
                    length(float3(unity_ObjectToWorld[0].x, unity_ObjectToWorld[1].x, unity_ObjectToWorld[2].x)),
                    // scale y axis
                    length(float3(unity_ObjectToWorld[0].y, unity_ObjectToWorld[1].y, unity_ObjectToWorld[2].y)),
                    // scale z axis
                    length(float3(unity_ObjectToWorld[0].z, unity_ObjectToWorld[1].z, unity_ObjectToWorld[2].z))
                );

                input.vertex.xyz += input.normal * 3 * .005f / world_scale;
                
                o.pos = UnityObjectToClipPos(input.vertex);

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