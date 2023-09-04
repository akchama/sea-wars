Shader "Custom/WaterShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                float time = _Time.y * 0.1 + 23.0;
                float2 uv = i.uv;
                float2 p = fmod(uv * 6.28318530718, 6.28318530718) - 250.0;
                float2 i_value = p;
                float c = 1.0;
                float inten = 0.005;
                
                for (int n = 0; n < 5; n++) 
                {
                    float t = time * (1.0 - (3.5 / (n + 1.0)));
                    i_value = p + float2(cos(t - i_value.x) + sin(t + i_value.y), sin(t - i_value.y) + cos(t + i_value.x));
                    c += 1.0 / length(float2(p.x / (sin(i_value.x + t) / inten), p.y / (cos(i_value.y + t) / inten)));
                }
                
                c /= 5.0;
                c = 1.17 - pow(c, 1.4);
                half3 colour = half3(pow(abs(c), 8.0), pow(abs(c), 8.0), pow(abs(c), 8.0));
                
                // Making the water darker by multiplying the final color
                colour *= 0.01; // 0.5 is the darkening factor, adjust as needed
                
                colour = clamp(colour + half3(0.0, 0.05, 0.08), 0.0, 0.3);
                
                return half4(colour, 1.0);
            }
            ENDCG
        }
    }
}
