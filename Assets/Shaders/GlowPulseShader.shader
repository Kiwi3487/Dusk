Shader "Custom/GlowShader"
{
    Properties
    {
        _GlowColor ("Glow Color", Color) = (1, 1, 1, 1)
        _GlowIntensity ("Glow Intensity", Range(0, 10)) = 5
        _AuraThickness ("Aura Thickness", Range(0, 1)) = 0.1
        _EmissionColor ("Emission Color", Color) = (1, 1, 1, 1)
        _EmissionIntensity ("Emission Intensity", Range(0, 10)) = 5 
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

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : SV_Target;
            };

            fixed4 _GlowColor;
            float _GlowIntensity;
            float _AuraThickness;
            fixed4 _EmissionColor; 
            float _EmissionIntensity;


            // Vertex shader: Transforms object vertices into screen space
            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // Convert object coordinates to clip-space for rendering
                o.uv = v.uv; // Pass UV coordinates through
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float glow = (sin(_Time.y * 1.0) * 0.5 + 0.5); //sin wave to control the glow effect (Sin moves intesnity back and fourth,
                float aura = smoothstep(0.0, _AuraThickness, glow); // Use smoothstep to soften the aura edges
                
                float4 color = (_GlowColor * aura * _GlowIntensity) + (_EmissionColor * _EmissionIntensity); //combines everything with intensity
                return color;
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
