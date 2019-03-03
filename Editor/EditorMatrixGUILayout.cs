using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Sokuhatiku.MatrixTools
{
	public static class EditorMatrixGUILayout
	{
		public static void AdvancedMatrixField(SerializedProperty matrixProperty)
		{
			AdvancedMatrixField(matrixProperty.displayName, matrixProperty);
		}

		public static void AdvancedMatrixField(string label, SerializedProperty matrixProperty)
		{
			var content = new GUIContent(label);
			AdvancedMatrixField(content, matrixProperty);
		}

		public static void AdvancedMatrixField(GUIContent label, SerializedProperty matrixProperty)
		{
			Rect rect = GetFieldRect();
			EditorMatrixGUI.AdvancedMatrixField(rect, matrixProperty);
		}

		public static Matrix4x4 AdvancedMatrixField(string label, Matrix4x4 matrix)
		{
			Rect rect = GetFieldRect();
			return EditorMatrixGUI.AdvancedMatrixField(rect, label, matrix);
		}

		public static Matrix4x4 AdvancedMatrixField(GUIContent label, Matrix4x4 matrix)
		{
			Rect rect = GetFieldRect();
			return EditorMatrixGUI.AdvancedMatrixField(rect, label, matrix);
		}


		public static void MaterialSerializedMatrixField(string propertyName, Material material, string namePrefix)
		{
			MaterialSerializedMatrixField(propertyName, material,
				namePrefix + "0", namePrefix + "1", namePrefix + "2", namePrefix + "3");
		}

		public static void MaterialSerializedMatrixField(string propertyName, Material material, string name0, string name1, string name2, string name3)
		{
			EditorGUI.BeginChangeCheck();
			var field0 = material.GetVector(name0);
			var field1 = material.GetVector(name1);
			var field2 = material.GetVector(name2);
			var field3 = material.GetVector(name3);
			var matrix = new Matrix4x4(field0, field1, field2, field3);

			matrix = AdvancedMatrixField(propertyName, matrix);

			var changed = EditorGUI.EndChangeCheck();
			if (changed)
			{
				material.SetVector(name0, matrix.GetColumn(0));
				material.SetVector(name1, matrix.GetColumn(1));
				material.SetVector(name2, matrix.GetColumn(2));
				material.SetVector(name3, matrix.GetColumn(3));
			}
		}


		private static Rect GetFieldRect()
		{
			return GUILayoutUtility.GetRect(Screen.width, EditorMatrixGUI.AdvancedMatrixFieldHeight);
		}

	}
}
