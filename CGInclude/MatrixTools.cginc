#define DECLARE_SERIALIZED_MATRIX(name) \
float4 name##0; \
float4 name##1; \
float4 name##2; \
float4 name##3;

#define USE_SERIALIZED_MATRIX(name) \
float4x4 name; \
name._m00_m01_m02_m03 = name##0; \
name._m10_m11_m12_m13 = name##1; \
name._m20_m21_m22_m23 = name##2; \
name._m30_m31_m32_m33 = name##3; \