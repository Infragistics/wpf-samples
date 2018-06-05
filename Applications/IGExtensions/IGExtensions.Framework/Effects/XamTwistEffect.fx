/// <summary>The amount of twist for the Swirl. </summary>
/// <minValue>0</minValue>
/// <maxValue>1</maxValue>
/// <defaultValue>0</defaultValue>
float Amount : register(C0);

/// <summary>The amount of twist for the Swirl. </summary>
/// <minValue>0</minValue>
/// <maxValue>1</maxValue>
/// <defaultValue>0.5</defaultValue>
float Frequency : register(C1);

/// <summary>The amount of twist for the Swirl. </summary>
/// <minValue>0,0</minValue>
/// <maxValue>1,1</maxValue>
/// <defaultValue>.5,.5</defaultValue>
float2 Center : register(C2);

sampler2D Input : register(s0);

float4 BandedSwirl(float2 uv)
{

  float2 toUV = uv - Center;
  float distanceFromCenter = length(toUV);
  float2 normToUV = toUV / distanceFromCenter;
  float angle = atan2(normToUV.y, normToUV.x);

  angle += sin(distanceFromCenter * Frequency) * Amount * 100.0;
  
  float2 newUV;
  sincos(angle, newUV.y, newUV.x);
  
  newUV = newUV * distanceFromCenter + Center;

	return tex2D(Input, frac(newUV));
}

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(float2 uv : TEXCOORD) : COLOR
{
  return BandedSwirl(uv);
}
