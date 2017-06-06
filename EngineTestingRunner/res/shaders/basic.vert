#version 430 core

layout (location=0) in vec3 pos;
//layout (location=1) in vec3 normal;

out vec4 Color;

uniform mat4 trans;

void main(){

		
		gl_Position = trans * vec4(pos,1.0f);

		Color = vec4(pos, 1.0);
}
