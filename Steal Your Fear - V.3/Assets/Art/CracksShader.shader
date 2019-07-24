Shader "Diffuse Bump Blending" {
	Properties{
	    _Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		
		_Blend("Blend", Range(0, 1)) = 0.5
		_Texture2("Texture 2", 2D) = ""
	}
		SubShader{
		  Tags { "RenderType" = "Opaque" }
		  LOD 200
		  CGPROGRAM
		  #pragma surface surf Lambert
		  struct Input {
			float2 uv_MainTex;
		  };

		  uniform float _Blend;
		  fixed4 _Color;

		  sampler2D _MainTex;
		  sampler2D _Texture2;
		
		  void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = lerp(tex2D(_MainTex, IN.uv_MainTex).rgb * _Color, tex2D(_Texture2, IN.uv_MainTex).rgb * _Color, _Blend);

			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Alpha = c.a;
		  }

		  ENDCG
	}
		Fallback "Diffuse"
}
