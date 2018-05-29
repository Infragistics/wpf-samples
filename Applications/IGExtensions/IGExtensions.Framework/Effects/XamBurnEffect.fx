sampler2D Input : register(s0);

sampler2D Noise : register(s1);

/// <minValue>0/minValue>
/// <maxValue>1</maxValue>
/// <defaultValue>0.5</defaultValue>
float Progress : register(C0);

float4 main(float2 uv : TEXCOORD) : COLOR 
{
	float k=0.5;
	
	float n0=tex2D(Noise, float2(uv.x, 0));
	float4 v1=min(1, (1+k)-Progress*(1+k))-0.2*Progress*n0;
	float4 v0=1.0-Progress*(1+k);
	float h=v1-v0;
	
	float p=(uv.y-v0)/(v1-v0);
	float4 color=tex2D(Input, uv);
	
	if(p<0)
	{
		return color;
	}
	
	if(p<1)
	{
		if(p>0.9)
		{
			return float4(0, 0,0,1);
		}
		
		p=p/0.9;
		float ns=p*tex2D(Noise, float2(frac(3*uv.x), frac(p+1.2*Progress)));
		float4 flame=lerp(float4(1,0,0,1), float4(1,1,0,1), ns);

		ns=min(2*ns, 1);
		
		return lerp(color, flame, ns);
	}
	
	return 0;
}


