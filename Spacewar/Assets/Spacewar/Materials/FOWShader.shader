Shader "Custom/FOWShader"
{
    Properties
    {
        _Color ("Color", Color) = (0,0,0,1)
        _MainTex ("AlphaZero (RGB) Trans (A)", 2D) = "white" {}
        _MainTex2 ("AlphaMid (RGB) Trans (A)", 2D) = "white" {}
    }
    SubShader
    {
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert alpha:blend


        sampler2D _MainTex;
        sampler2D _MainTex2;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 colorZero = tex2D(_MainTex, IN.uv_MainTex);
            fixed4 colorMid = tex2D(_MainTex, IN.uv_MainTex);
    
            o.Albedo = _Color;
            // Metallic and smoothness come from slider variables
            float alpha = 1.0f - colorZero.g - colorMid.g / 4;
            o.Alpha = alpha;
        }
        ENDCG
    }
}
