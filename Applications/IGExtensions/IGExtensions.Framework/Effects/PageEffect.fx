sampler2D input : register(s0);
sampler2D Table : register(s1);

float2 Origin : register(C0);			// fold origin
float2 Direction : register(C1);		// normalised fold direction vector	
float Curve: register(C2);				// curve size (0 is flat, 0.1 is about right and 1 is way too much)
float4 Background: register(C3);		// back color
float4 Fresnel: register(C4);			// fresnel highlight color
float Opacity: register(C5);

bool inside(float2 p)
{
	return p.x >= 0 && p.x <= 1 && p.y >= 0 && p.y <=1;
	//return saturate(p.x)==v.x && saturate(p.y)==v.y;
}

float4 main(float2 p : TEXCOORD) : COLOR 
{
	float	pi = 3.1415926535897932384626433832795;
	float	s = Curve * pi / 2.0;
	float2	d = { Direction.y, -Direction.x };										// direction p to fold
	float	ld = Direction.x * (p.y - Origin.y) - Direction.y * (p.x - Origin.x);	// signed distance p to fold

	float2	R = p + d * (ld + ld);
	float4	table = { 1, 1,	0, 1 };

	if(ld > s - Curve)
	{
		if(ld < s /* on the curl */)
		{
			table = tex2D(Table, float2((s - ld) / Curve, 0));

			float  k = Curve * table.w * 0.5 * pi;

			R = p + d * (ld + s - k);
			p = p + d * (ld - s + k);
		}

		if(inside(p))
		{
			float4 front=tex2D(input, p);
			front.rgb *= table.x;

			if(inside(R))
			{
				float4 back = tex2D(input, R);

				back.rgb = lerp(back.rgb, Background.rgb, Background.a);	// blend with page backface color
				back.rgb *= table.y;										// matte illumination
				back=lerp(front, back, Opacity);							// blend with occluded front
				back=saturate(back+Fresnel*table.z);						// fresnel highlight

				return back;
			}

			return front;
		}
	}
	
	discard;
	return float4(0, 0, 0, saturate(2*ld/s));		
}
