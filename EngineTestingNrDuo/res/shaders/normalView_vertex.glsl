#version 430

layout (location = 0) in vec3 pos;
layout (location = 2) in vec3 normal;

out vec3 Normal;

uniform mat4 transform;

void main(){
	Normal = normal;
	gl_Position = vec4(pos, 1.0) ;
}