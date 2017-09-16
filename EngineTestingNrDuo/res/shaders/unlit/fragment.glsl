#version 430

uniform vec3 color;

out vec4 outCol;

void main(){
	outCol = vec4(color, 1.0);
}