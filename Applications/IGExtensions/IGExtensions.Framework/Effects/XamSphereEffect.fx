sampler2D Input : register(s0);
float Longitude : register(C0);

float4 main(float2 uv : TEXCOORD) : COLOR 
{ 
	const float pi=3.1415926535897932384626433832795;
	const float rho=0.5;
	float3 xyz={uv-0.5, 0};

	if(length(xyz)<rho)
	{
		xyz.z=sqrt(rho*rho-xyz.x*xyz.x-xyz.y*xyz.y);
		
		// rotate and what-not
		
		float latitude=asin(xyz.y/rho);
		float longitude=atan2(xyz.z, xyz.x)+Longitude;
		float2 st={ 1.0-frac(longitude/(2.0*pi)), 0.5+(latitude/pi) };
	
		float3 L={ 0.4, 0.4, -1 };
		normalize(L);
		
		float I = -dot(L, xyz/rho);
		
		return tex2D(Input, st*0.99)*float4(I, I, I, 1);
	}
	
	return 0; 
}
