#version 330

in vec2 outTexCoord;
in vec3 mvVertexNormal;
in vec3 mvVertexPos;
in mat4 outModelViewMatrix;

out vec4 fragColor;

struct Material
{
    vec3 colour;
    int useColour;
    float reflectance;
    int hasNormalMap;
};

uniform vec3 ambientLight;
uniform Material material;
uniform sampler2D texture_sampler;
uniform sampler2D normalMap;



void main()
{
	vec4 baseColour; 
	vec4 tex;

	float ref = material.reflectance;
	float tmp = 0.5+ref;
    if ( material.useColour == 1 )
    {
        baseColour = vec4(material.colour, 1);
    }
    else
    {
        tex = texture(texture_sampler, outTexCoord);
    }
    if ( material.hasNormalMap == 1 )
    {
        baseColour=baseColour+vec4(0.1,0,0,0);
        baseColour=baseColour-vec4(0.1,0,0,0);
    }
    
    vec4 redTint = vec4(tmp-ref, 0.0, 0.0, 1.0);
    vec4 totalLight = vec4(ambientLight, 1.0);
    baseColour = baseColour+tex;
	baseColour = baseColour-tex;

    fragColor = (baseColour + redTint) * totalLight;
}