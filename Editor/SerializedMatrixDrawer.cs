using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SerializedMatrixDrawer : MaterialPropertyDrawer
{
	public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
	{
		return EditorMatrixGUI.AdvancedMatrixFieldHeight;
	}

	public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor)
	{
		var serializedObject = editor.serializedObject;
		var name = prop.name;
		var baseName = name.Substring(0, name.Length - 1);

		EditorGUI.BeginChangeCheck();

		var prop0 = MaterialEditor.GetMaterialProperty(editor.targets, baseName + "0");
		var prop1 = MaterialEditor.GetMaterialProperty(editor.targets, baseName + "1");
		var prop2 = MaterialEditor.GetMaterialProperty(editor.targets, baseName + "2");
		var prop3 = MaterialEditor.GetMaterialProperty(editor.targets, baseName + "3");

		var matrix = new Matrix4x4(
			prop0.vectorValue,
			prop1.vectorValue,
			prop2.vectorValue,
			prop3.vectorValue);

		EditorGUI.showMixedValue =
			prop0.hasMixedValue ||
			prop1.hasMixedValue ||
			prop2.hasMixedValue ||
			prop3.hasMixedValue;

		matrix = EditorMatrixGUI.AdvancedMatrixField(position, label, matrix);

		EditorGUI.showMixedValue = false;

		var changed = EditorGUI.EndChangeCheck();
		if (changed)
		{
			prop0.vectorValue = matrix.GetColumn(0);
			prop1.vectorValue = matrix.GetColumn(1);
			prop2.vectorValue = matrix.GetColumn(2);
			prop3.vectorValue = matrix.GetColumn(3);
		}
	}
}
