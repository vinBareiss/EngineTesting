#version 430 core

struct Transform {
	Mat4 view;
	Mat4 projection;
	Mat4 model;

	mat4 getTransformMatrix(){
		return model * view * projection;
	}
}

in vec3 pos;
out vec3 Col;

uniform Transform transform;

void main(){
	gl_position = pos * transform.getTransformMatrix();
	Col = vec3(1.0, 1.0, 1.0, 1.0);
}