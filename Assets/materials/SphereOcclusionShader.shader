Shader "Custom/SphereOcclusionShader"
{
    Properties
    {
        _Center("Sphere Center", Vector) = (0, 0, 0, 0)
        _Radius("Sphere Radius", Float) = 1.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Tags { "Queue" = "Geometry-1" }
        ZWrite On
        ZTest LEqual
        ColorMask 0

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            float4 _Center;
            float _Radius;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_WorldToObject, v.vertex).xyz;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                float3 centerToVertex = i.worldPos - _Center.xyz;
                float distance = length(centerToVertex);

                if (distance < _Radius)
                {
                    return fixed4(0.0, 0.0, 0.0, 0.0);
                }
                else
                {
                    return fixed4(1.0, 1.0, 1.0, 1.0);
                }
            }
            ENDCG
        }
    }
}
