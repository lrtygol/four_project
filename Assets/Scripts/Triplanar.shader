Shader "Custom/TriplanarStandard"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _TileSize ("Meters per tile", Float) = 4
        _Sharpness ("Blend sharpness", Range(1,8)) = 4
        _Glossiness ("Smoothness", Range(0,1)) = 0.2
        _Metallic ("Metallic", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;
        float _TileSize;
        float _Sharpness;
        half _Glossiness;
        half _Metallic;

        struct Input
        {
            float3 worldPos;
            float3 worldNormal;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // ¬ес каждой проекции зависит от направлени€ грани
            float3 blend = pow(abs(IN.worldNormal), _Sharpness);
            blend /= (blend.x + blend.y + blend.z);

            float3 p = IN.worldPos / _TileSize;

            fixed4 cx = tex2D(_MainTex, p.zy); // боковые грани (ось X)
            fixed4 cy = tex2D(_MainTex, p.xz); // верх и низ
            fixed4 cz = tex2D(_MainTex, p.xy); // боковые грани (ось Z)

            fixed4 c = cx * blend.x + cy * blend.y + cz * blend.z;

            o.Albedo = c.rgb * _Color.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}