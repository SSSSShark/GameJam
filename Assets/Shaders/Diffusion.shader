Shader "Custom/Diffusion"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PictureWidth ("Picture Size", Float) = 1920
        _PictureHeight ("Picture Height", Float) = 1080
        _Range ("Diffuse Range", Float) = 10
        _Ratio ("Diffuse Range", Range(0,1))=0.5
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

            float _PictureWidth;
            float _PictureHeight;
            float _Range;
            float _Ratio;

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 diffuse(float2 uv)
            {
                float4 maxC=tex2D(_MainTex, uv);
                float u=0.0;
                float v=0.0;
                for(int i =1;i<=_Range;i++)
                {
                    for(int j =1;j<=_Range;j++)
                    {
                        if(length(tex2D(_MainTex,uv+float2(i/_PictureWidth,j/_PictureHeight)))>length(maxC)){
                            maxC=tex2D(_MainTex,uv+float2(i/_PictureWidth,j/_PictureHeight));
                            u=i;
                            v=j;
                        }
                        if(length(tex2D(_MainTex,uv+float2(-i/_PictureWidth,-j/_PictureHeight)))>length(maxC)){
                            maxC=tex2D(_MainTex,uv+float2(-i/_PictureWidth,-j/_PictureHeight));
                            u=i;
                            v=j;
                        }
                    }
                }
                return lerp(maxC,tex2D(_MainTex, uv),(v+u)/(2*_Range,2));
            }

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
                fixed4 col = (1-_Ratio)*tex2D(_MainTex, i.uv)+_Ratio*diffuse(i.uv);
                return col;
            }
            ENDCG
        }
    }
}
