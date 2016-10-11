Shader "MistralTest/TestVolumeLight"
{
    Properties
    {
    	//This is the position relative to the camera concerned. 
    	_LightPos ("Position of the light", Vector) = (0, 0, 0, 0)
    	exL ("Extension of hte volume light", float) = 3
    	kP ("sdfsdf", float) = 3
    	BaseC ("Base Color", color) = (1, 1, 1, 1)
    }

    SubShader
    {
    	Tags {"Queue" = "Transparent+10"}
    	ZWrite OFF
    	Offset 1,1
    	Pass
    {
    	Blend SrcAlpha OneMinusSrcAlpha
    	Cull OFF
    	CGPROGRAM

    	#pragma vertex vert
    	#pragma fragment frag
    	#include "UnityCG.cginc"
    	uniform float4 _LightPos;
    	uniform float exL;
    	uniform float kP;
    	uniform float4 BaseC;

    	struct vertexInput
    	{
    	    float4 vertex : POSITION;
    	    float4 normal : NORMAL;
    	    float4 texcoord : TEXCOORD;
    	};

    	struct vertexOutput
    	{
    	    float4 pos : SV_POSITION;
    	    float3 op : TEXCOORD0;
    	    float exDist : TEXCOORD1;
    	    float4 oLitP : TEXCOORD2;
    	    float2 uv : TEXCOORD3;
    	};

    	vertexOutput vert (vertexInput v) : POSITION
    	{
    	    vertexOutput o;
    	    o.oLitP = mul (_LightPos, UNITY_MATRIX_IT_MV);
    	    float3 toLit = o.oLitP - v.vertex.xyz * o.oLitP.w;
    	    float3 toLight = normalize (toLit);
    	    float backFactor = dot (toLight, v.normal);

    	    float extrude = (backFactor < 0.0) ? 1.0 : 0.0;
    	    v.vertex.xyz += v.normal * 0.05;
    	    v.vertex.xyz -= toLight * (extrude * exL);
    	    o.pos = (UNITY_MATRIX_MVP, v.vertex);
    	    o.exDist = extrude * exL;
    	    o.op = v.vertex.xyz / v.vertex.w;
    	    o.uv = v.texcoord.xy;
    	    return o;
    	}

    	float4 frag (vertexOutput i) : COLOR
    	{
    	    float alp = 0.7;
    	    float toL = distance (i.oLitP.xyz, i.op);
    	    float dist = toL- exL;
    	    float att = dist / exL;
    	    att = 1 - att;
    	    float4 c = BaseC * att;
    	    c.a = pow (att, kP) * BaseC * att;
    	    return c;
    	}

    	ENDCG
    }
    }

    FallBack "Diffuse"
}