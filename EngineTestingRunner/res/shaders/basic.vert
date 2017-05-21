#version 450

(layout location=0) in vec3 pos;
//in vec2 uv;
//in vec3 col;

vec4 getCol();



out vec4 Col

void main(){

	Col = getCol();
	
}


vec4 getCol(){

return vec4(1.0, 1.0, 0.0, 1.0)
}