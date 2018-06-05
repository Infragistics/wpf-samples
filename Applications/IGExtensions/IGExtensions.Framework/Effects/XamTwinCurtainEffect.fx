sampler2D input : register(s0);

// new HLSL shader

/// <summary>Explain the purpose of this variable.</summary>
/// <minValue>0/minValue>
/// <maxValue>1</maxValue>
/// <defaultValue>0.5</defaultValue>
float Progress : register(C0);

/// <summary>Explain the purpose of this variable.</summary>
/// <minValue>0/minValue>
/// <maxValue>1</maxValue>
/// <defaultValue>0.5</defaultValue>
float Opening: register(C1);

float4 main(float2 uv : TEXCOORD) : COLOR 
{
	float sx0=clamp(Progress/0.6, 0.0, 1.0);
	float sx1=Progress;	
	float sx=lerp(sx0, sx1, Opening<0.5? uv.y*uv.y: 1.0-(uv.y*uv.y));

	float x;
	
	if(uv.x<=0.5 /* left curtain */)
	{
		x=uv.x/sx;
			
		if(x>0.5) { return 0; }
	}
	else /* right curtain */
	{
		x=1.0-(1.0-uv.x)/sx;
		
		if(x<0.5) { return 0; }
	}

	// rippling
	float y=uv.y+0.05*lerp(sin(x*8*3.14)*(0.2+0.8*uv.y), 0, sx*sx);
	
	if(y<0.0 || y>1.0) { return 0; }

	// shading
	float Is=lerp(0.5+0.5*(cos(x*8*3.14)), 1, sx*sx);
	
	return tex2D(input, float2(x, y))*float4(Is, Is, Is, 1);
}