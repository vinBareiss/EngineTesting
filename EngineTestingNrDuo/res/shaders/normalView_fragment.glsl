#version 430 core

in vec3 Normal;
out vec4 color;

void main(){
	color = vec4((Normal / 2) + 0.5,1.0);
}