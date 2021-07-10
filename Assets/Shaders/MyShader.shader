Shader "Custom/MyShader"
{
    Properties
    {
    	_Color ("Color", Color) = (1, 1, 1, 1)
    	_MainTex ("Main Texture", 2D) = "white" { }
        _ToonSteps ("Toon Steps",Range(2,10))=2
        _RampThreshold ("RampThreshold",Range(0,1))=0.5
        _RampSmooth ("Ramp Smooth",Range(0,1))=0.2
        _ShadowColor ("Shadow Color", Color)=(0.2,0.2,0.2,0.2)
        _SpecularColor  ("Specular Color", Color)=(0.8,0.8,0.8,0.8)
        _SpecSmooth ("SpecSmooth",Range(0,1))=0.5
        _Shininess ("Shininess",Range(0,1))=0.5
        _RimColor ("Rim Color", Color)=(0.8,0.8,0.8,0.8)
        _RimThreshold ("RimThreshold",Range(0,1))=0.5
        _RimSmooth ("Rim Smooth",Range(0,1))=0.5
    }

    SubShader
    {
	    Tags { "RenderType" = "Opaque" }

        Cull off
        
	    CGPROGRAM
        
	    #pragma surface surf Toon addshadow fullforwardshadows exclude_path:deferred exclude_path:prepass
    	#pragma target 3.0
        
    	fixed4 _Color;
    	sampler2D _MainTex;
        fixed _RampThreshold;
        fixed _RampSmooth; 
        float4 _ShadowColor;
        float4 _SpecularColor;
        fixed _SpecSmooth;
        fixed _Shininess;
        fixed _ToonSteps;
        fixed4 _RimColor;
        fixed _RimThreshold;
        fixed _RimSmooth;

    	struct Input
    	{
    		float2 uv_MainTex;
    		float3 viewDir;
    	};
        float linearstep(float min, float max, float t)
        {
        	return saturate((t - min) / (max - min));
        }
    	inline fixed4 LightingToon(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
    	{
    		half3 normalDir = normalize(s.Normal);
    		float ndl = max(0, dot(normalDir, lightDir));

    		fixed3 lightColor = _LightColor0.rgb;
            
    		fixed4 color;
            /*fixed3 ramp = smoothstep(_RampThreshold - _RampSmooth * 0.5, _RampThreshold + _RampSmooth * 0.5, ndl);
            ramp *= atten;
            //_ShadowColor=lerp(_SpecularColor,_ShadowColor,_ShadowColor.a);*/
            float diff = smoothstep(_RampThreshold - ndl, _RampThreshold + ndl, ndl);
            float ramp;
            float interval = 1 / _ToonSteps;
            float level = round(diff * _ToonSteps) / _ToonSteps;
            if (_RampSmooth == 1)
	        {
        		ramp = interval * linearstep(level - _RampSmooth * interval * 0.5, level + _RampSmooth * interval * 0.5, diff) + level - interval;
        	}
        	else
        	{
        		ramp = interval * smoothstep(level - _RampSmooth * interval * 0.5, level + _RampSmooth * interval * 0.5, diff) + level - interval;
        	}
            ramp = max(0, ramp);
            ramp *= atten;

            float3 rampColor=lerp(_ShadowColor.rgb,(1.0,1.0,1.0),ramp);
	    	fixed3 diffuse = s.Albedo * lightColor * rampColor;
            
            
            half3 halfDir = normalize(lightDir + viewDir);
            float ndh=max(0,dot(normalDir,halfDir));
            
            float spec=pow(ndh,s.Specular*512.0)*s.Gloss;
            spec*=atten;
            spec=smoothstep(0.5-_SpecSmooth*0.5,0.5+_SpecSmooth*0.5,spec);
            fixed3 specular = _SpecularColor.rgb*lightColor*spec;

            float ndv = max(0, dot(normalDir, viewDir));
            float rim = (1.0 - ndv) * ndl;
            rim *= atten;
            rim = smoothstep(_RimThreshold - _RimSmooth * 0.5, _RimThreshold + _RimSmooth * 0.5, rim);
            fixed3 rimColor = _RimColor.rgb * lightColor * _RimColor.a * rim;

    		color.rgb = diffuse + specular + rimColor;
	    	color.a = s.Alpha;
	    	return color;
    	}
        
	    void surf(Input IN, inout SurfaceOutput o)
    	{
	    	fixed4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
    		o.Albedo = mainTex.rgb * _Color.rgb;
    		o.Alpha = mainTex.a * _Color.a;
            o.Specular=_Shininess;
            o.Gloss=mainTex.a;
    	}

    	ENDCG
    }
}
