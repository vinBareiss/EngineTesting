#version 430 core

in vec4 Color;
out vec4 fragCol;

vec4 testFunc();

void main(){

	vec4 red = vec4(1.0, 0.0, 0.0, 1.0);

	
	fragCol = testFunc();

}
