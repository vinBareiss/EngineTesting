#version 430 core

layout (location=0) in vec3 pos;

out vec4 Color;

void main(){

		
		gl_Position = vec4(pos,1.0f);

		Color = vec4(pos, 1.0);
}
