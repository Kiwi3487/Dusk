Shader "Custom/WhiteEmission"
{
    Properties
    {
        _EmissionColor ("Emission Color", Color) = (1, 1, 1, 1) //color
        _EmissionIntensity ("Emission Intensity", Range(0, 10)) = 1 //intesity
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc" // Includes Unity's helper functions and definitions
            // Structure for the vertex shader input, position, normal and UV coordinates
            struct appdata
            {
                float4 vertex : POSITION; 
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0; 
            };
            // Structure for passing data to the fragment shader, UV coordinates and transformed position
            struct v2f
            {
                float2 uv : TEXCOORD0; 
                float4 vertex : SV_POSITION;
            };
            //variables
            fixed4 _EmissionColor;
            float _EmissionIntensity;
            // Vertex shader: Transforms object vertices into screen space
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            // Fragment shader: Calculates the color of each pixel
            fixed4 frag (v2f i) : SV_Target
            {

                fixed4 color = _EmissionColor * _EmissionIntensity;
                return color;
            }
            ENDCG
        }
    }
    Fallback "Diffuse" 
}
