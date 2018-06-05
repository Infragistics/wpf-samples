float Saturation: register(C0);
float4 Tint: register(C1);

sampler2D Input : register(S0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
   float4 saturated = tex2D(Input, uv);
   float4 desaturated = float4(Tint.rgb*dot(saturated.rgb, float3(0.3, 0.59, 0.1)), Tint.a*saturated.a);
   
   return lerp(desaturated, saturated, Saturation);
}
