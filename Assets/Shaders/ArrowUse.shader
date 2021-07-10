Shader "Tools/ArrowUse"
{
    Properties
    {
        _MainTex ("Gray", 2D) = "white" {}
        _MainTex1 ("White", 2D) = "white" {}
        _MainTex2 ("Blue", 2D) = "white" {}
        _MainTex3 ("Purple", 2D) = "white" {}
        _MainTex4 ("Golden", 2D) = "white" {}
        _Lit ("lightness increase", Float) = 1.2
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

            #include "UnityCG.cginc"

            float _Lit;

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

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                clip(col.a-0.99);
                col.rgb*=_Lit;
                return col;
            }
            ENDCG
        }
    }
}
