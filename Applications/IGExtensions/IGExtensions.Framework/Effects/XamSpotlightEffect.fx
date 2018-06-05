sampler2D input : register(s0);

float2 Mouse: register(C0);
float2 Hotspot: register(C1);

float4 main(float2 uv : TEXCOORD) : COLOR 
{ 
	float SpotExp=128.0;
	float shininess=1.0;	
	float Amount=20;
	float Width=0.001;
	
	float3 lightPosition=float3(Hotspot, 0.5);
	float3 spotDirection=normalize(float3(Mouse, 0.0)-lightPosition);
	float3 eyePosition=float3(0.5, 0.5, 1.0);

	float3 L=normalize(float3(uv, 0.0)-lightPosition);		// vector from light to pixel
	float spotEffect=pow(dot(spotDirection, L), SpotExp);	// spot illumination intensity

	if(spotEffect>0.01)
	{
		float scale=Amount*Width;

		float3 c0=tex2D(input, float2(uv.x-Width, uv.y));
		float3 c1=tex2D(input, float2(uv.x+Width, uv.y));
		float3 ea=float3(2.0*Width, 0, (c1.r-c0.r+c1.g-c0.g+c1.b-c0.b)*scale);
		
		float3 c2=tex2D(input, float2(uv.x, uv.y-Width));
		float3 c3=tex2D(input, float2(uv.x, uv.y+Width));
		float3 eb=float3(0, 2.0*Width, (c3.r-c2.r+c3.g-c2.g+c3.b-c2.b)*scale);
		
		float3 N=normalize(cross(ea, eb));					// normal at pixel
		float3 Eye=normalize(eyePosition-float3(uv, 0));	// vector from pixel to viewer
		float3 H = normalize(Eye-L);						// illumination half-vector		

		float Is=pow(dot(N, H), shininess);					// specular intensity
		float I=0.5*Is*spotEffect;							// overall intensity

		return tex2D(input, uv)+float4(I, I, I, 0.0);
	}
	else
	{
		return tex2D(input, uv);
	}
}

