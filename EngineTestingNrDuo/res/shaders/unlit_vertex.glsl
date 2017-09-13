#version 430

layout (location = 0) in vec3 pos;

out vec3 Col;
uniform mat4 transform;

void main(){

	Col = pos;
	gl_Position = transform * vec4(pos, 1.0) ;
}