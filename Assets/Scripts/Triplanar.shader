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
        #pragma surface surf Standard fullforwardshadows vertex:vert
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;
        float _TileSize;
        float _Sharpness;
        half _Glossiness;
        half _Metallic;

        struct Input
        {
            float3 localPos;
            float3 localNormal;
        };

        void vert (inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);

            // Масштаб объекта, чтобы плотность текстуры не зависела от Scale
            float3 scale = float3(
                length(unity_ObjectToWorld._m00_m10_m20),
                length(unity_ObjectToWorld._m01_m11_m21),
                length(unity_ObjectToWorld._m02_m12_m22));

            o.localPos = v.vertex.xyz * scale; // позиция в локальных осях, но в метрах
            o.localNormal = v.normal;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 blend = pow(abs(IN.localNormal), _Sharpness);
            blend /= (blend.x + blend.y + blend.z);

            float3 p = IN.localPos / _TileSize;

            fixed4 cx = tex2D(_MainTex, p.zy);
            fixed4 cy = tex2D(_MainTex, p.xz);
            fixed4 cz = tex2D(_MainTex, p.xy);

            fixed4 c = cx * blend.x + cy * blend.y + cz * blend.z;

            o.Albedo = c.rgb * _Color.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}