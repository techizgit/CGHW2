#version 330

in vec2 outTexCoord;
in vec3 mvVertexNormal;
in vec3 mvVertexPos;
in vec4 mvVertexLight;

out vec4 fragColor;

uniform sampler2D texture_sampler;

struct Material
{
    vec3 colour;
    int useColour;
    float reflectance;
    int hasNormalMap;
};

uniform Material material;

void main()
{
    vec4 baseColour;
 
    if ( material.useColour == 1 )
    {
        baseColour = vec4(material.colour, 1);
    }
    else
    {
        baseColour = texture(texture_sampler, outTexCoord);
    }
    if ( material.hasNormalMap == 1 )
    {
        baseColour=baseColour+vec4(0.1,0,0,0);
        baseColour=baseColour-vec4(0.1,0,0,0);
    }
    fragColor = mvVertexLight * baseColour;
}