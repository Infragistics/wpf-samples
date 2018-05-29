sampler2D Input : register(s0);
sampler2D Noise : register(s1);

float Progress : register(C0);

float4 main(float2 uv : TEXCOORD) : COLOR 
{ 
	float nr=tex2D(Noise, float2(frac(uv.x-0.1*Progress), frac(uv.y-0.3*Progress))).r; 
	float ng=tex2D(Noise, float2(frac(uv.x+0.1*Progress), frac(uv.y-0.2*Progress))).g; 
	float nb=tex2D(Noise, float2(frac(uv.x), frac(uv.y-0.4*Progress))).b; 
	float px=1.0-65536.0*pow(uv.x-0.5, 16);
	float py=clamp(uv.y*2, 0, 1);

	float d=(nr+ng+nb)*0.75*(1-py)*px;

	return lerp(tex2D( Input , uv.xy), float4(1, 1, 1, 1), d); 
}