using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

	private static Rect GetFieldRect()
	{
		return GUILayoutUtility.GetRect(Screen.width, EditorMatrixGUI.AdvancedMatrixFieldHeight);
	}

}
