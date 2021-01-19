Shader "WebcamVisualizer"
{
    Properties
    {
        _CameraFeed("", 2D) = ""{}
    }

    CGINCLUDE

    #include "UnityCG.cginc"

    sampler2D _CameraFeed;

    void VertexBlit(uint vid : SV_VertexID,
                    out float4 position : SV_Position,
                    out float2 uv : TEXCOORD0)
    {
        float x = vid >> 1;
        float y = (vid & 1) ^ (vid >> 1);

        position = float4(float2(x, y) * 2 - 1, 1, 1);
        uv = float2(x, y);
    }

    float4 FragmentBlit(float4 position : SV_Position,
                        float2 uv : TEXCOORD0) : SV_Target
    {
        return tex2D(_CameraFeed, uv);
    }

    ENDCG

    SubShader
    {
        // Draw the camera output
        Pass
        {
            ZTest Always ZWrite Off Cull Off
            CGPROGRAM
            #pragma vertex VertexBlit
            #pragma fragment FragmentBlit
            ENDCG
        }
    }
}
