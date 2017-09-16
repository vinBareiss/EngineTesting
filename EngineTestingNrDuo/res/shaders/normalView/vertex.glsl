#version 430

#include structs.transform;

layout (location = 0) in vec3 pos;
layout (location = 2) in vec3 normal;

out vec3 Normal;

uniform Transform transform;

void main(){
	Normal = normal;
	gl_Position = transform.GetMat4() * vec4(pos, 1.0) ;
}