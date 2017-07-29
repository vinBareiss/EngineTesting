#version 450

layout (location = 1) in vec3 pos;

uniform mat4 transform;

void main(){
	gl_Position = vec4(pos, 1.0) * transform;
}