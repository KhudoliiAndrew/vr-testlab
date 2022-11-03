Shader "Custom/SurfaceOutlineShader"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineThickness ("Outline Thickness", Range(0,1)) = 0.1
    }
    SubShader
    {
        //the material is completely non-transparent and is rendered at the same time as the other opaque geometry
        Tags
        {
            // change queue value to enable fog
            "Queue" = "Transparent+1"

            "RenderType"="Opaque"
        }

        //The second pass where we render the outlines
        Pass
        {
            Cull Front

            CGPROGRAM
            //include useful shader functions
            #include "UnityCG.cginc"

            //define vertex and fragment shader
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            //tint of the texture
            fixed4 _OutlineColor;
            float _OutlineThickness;

            //the object data that's put into the vertex shader
            struct appdata
            {
                float4 vertex : POSITION;
                float4 normal : NORMAL;

                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            //the data that's used to generate fragments and can be read by the fragment shader
            struct v2f
            {
                float4 position : SV_POSITION;

                UNITY_VERTEX_OUTPUT_STEREO
            };

            //the vertex shader
            v2f vert(appdata v)
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v); //Insert
                UNITY_INITIALIZE_OUTPUT(v2f, o); //Insert
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); //Insert
                UNITY_TRANSFER_FOG(o, o.vertex);

                //convert the vertex positions from object space to clip space so they can be rendered
                o.position = UnityObjectToClipPos(v.vertex + normalize(v.normal) * _OutlineThickness);
                return o;
            }

            //the fragment shader
            fixed4 frag(v2f i) : SV_TARGET
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }
    FallBack "Standard"
}