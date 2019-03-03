using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MatrixEditor))]
public class MatrixEditorInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		var matrixProperty = serializedObject.FindProperty("matrix");
		EditorGUI.BeginDisabledGroup(matrixProperty.hasMultipleDifferentValues);
		if (GUILayout.Button("Copy Invert Matrix"))
		{
			var matrix = matrixProperty.GetMatrixValue().inverse;
			EditorMatrixUtility.MatrixToClipboard(ref matrix);
		}
		EditorGUI.EndDisabledGroup();
	}
}
