sampler2D Input : register(s0);

float4 Spot0Position: register(C1);
float4 Spot0Direction: register(C2);

float4 Spot1Position: register(C3);
float4 Spot1Direction: register(C4);

float3 normal(float2 uv : TEXCOORD): COLOR
{
	float Width=0.001;
	float scale=20*Width;

	float c0=tex2D(Input, float2(uv.x-Width, uv.y)).r;
	float c1=tex2D(Input, float2(uv.x+Width, uv.y)).r;
	float3 ea=float3(2.0*Width, 0, (c0-c1)*scale);
		
	float c2=tex2D(Input, float2(uv.x, uv.y-Width)).r;
	float c3=tex2D(Input, float2(uv.x, uv.y+Width)).r;
	float3 eb=float3(0, 2.0*Width, (c3-c2)*scale);
			
	return normalize(cross(ea, eb));
}

float Spot(float2 uv, float3 Eye, float3 N, float3 _position, float3 _direction)
{
	float SpotExp=128;
	float shininess=1.0;
	
	float3 L=normalize(float3(uv, 0.0)-_position);		// vector from light to pixel
	float spotEffect=pow(dot(_direction, L), SpotExp);	// spot illumination intensity

	return pow(dot(N, normalize(Eye-L)), shininess)*spotEffect;
}

float4 main(float2 uv : TEXCOORD) : COLOR 
{ 
	float3 N=normal(uv);
	float3 xy=float3(uv, 0);
	float3 Eye=normalize(float3(0.5, 0.5, 1.0)-xy);	// vector from pixel to viewer

	float Is0=Spot(uv, Eye, N, (Spot0Position.rgb*3)-1, normalize((Spot0Direction.rgb*2)-1));
	float Is1=Spot(uv, Eye, N, (Spot1Position.rgb*3)-1, normalize((Spot1Direction.rgb*2)-1));

	return tex2D(Input, uv)+float4(1, 0.5, 0, 0)*Is0+float4(0, 0.5, 1, 0)*Is1;
}