layout (location=0) in vec3 pos;
layout (location=1) in vec3 normal;

out vec4 Color;

uniform Transform transform;

void main(){
		
		gl_Position = transform.projection * transform.view * transform.model * vec4(pos,1.0f);

		Color = vec4(normal, 1.0);
}
