using UnityEditor;
using UnityEngine;

public static class SerializedPropertyExtensions
{
	public static Matrix4x4 GetMatrixValue(this SerializedProperty property)
	{
		var matrix = default(Matrix4x4);
		var iterator = property.Copy();

		for (int i = 0; i < 16; i++)
		{
			iterator.Next(true);
			matrix[i] = iterator.floatValue;
		}

		return matrix;
	}

	public static void SetMatrixValue(this SerializedProperty property, Matrix4x4 matrix)
	{
		var iterator = property.Copy();

		for (int i = 0; i < 16; i++)
		{
			iterator.Next(true);
			iterator.floatValue = matrix[i];
		}
	}
}
