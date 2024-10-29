Shader "Custom/LumShader"
{
    Properties
    {
        _DiffuseColor ("Diffuse Color", Color) = (1, 1, 1, 1) // Color of the diffuse reflection
        _AmbientColor ("Ambient Color", Color) = (0.2, 0.2, 0.2, 1) // Ambient color to simulate indirect lighting
        _SpecularColor ("Specular Color", Color) = (1, 1, 1, 1) // Color of specular reflection
        _Shininess ("Shininess", Range(1, 128)) = 16 // Shininess factor for specular highlights
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            // Properties
            fixed4 _DiffuseColor;
            fixed4 _AmbientColor;
            fixed4 _SpecularColor;
            float _Shininess;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = normalize(mul(v.normal, (float3x3)unity_WorldToObject)); // Transform normal to world space
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz; // Get world position
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Calculate normal and light direction
                float3 normal = normalize(i.worldNormal);
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz); // Direction of main light
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos); // Direction towards the camera

                // Diffuse lighting
                float NdotL = max(0, dot(normal, lightDir));
                fixed4 diffuse = _DiffuseColor * NdotL;

                // Ambient lighting
                fixed4 ambient = _AmbientColor;

                // Specular lighting
                float3 reflectDir = reflect(-lightDir, normal);
                float spec = pow(max(0, dot(viewDir, reflectDir)), _Shininess);
                fixed4 specular = _SpecularColor * spec;

                // Combine all components
                fixed4 color = ambient + diffuse + specular;
                color.a = 1.0; // Set alpha to fully opaque

                return color;
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
