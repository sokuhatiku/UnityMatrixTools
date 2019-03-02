using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AdvancedMatrixFieldAttribute))]
public class AdvancedMatrixFieldDrawer : PropertyDrawer
{
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		int linecount = 0;

		// foldout
		linecount += 1;

		if (property.isExpanded)
		{
			// CopyButton
			linecount += 1;

			// MatrixField
			linecount += 4;
		}

		return EditorGUIUtility.singleLineHeight * linecount;
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		// foldout
		position.height = EditorGUIUtility.singleLineHeight * 1;
		property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);
		position.y += position.height;

		position.xMin += 20;
		if (property.isExpanded)
		{
			// CopyPasteButton
			position.height = EditorGUIUtility.singleLineHeight * 1;
			ControlField(position, property);
			position.y += position.height;

			// MatrixField
			position.height = EditorGUIUtility.singleLineHeight * 4;
			MatrixEditField(position, property);
			position.y += position.height;
		}
	}

	private static void MatrixEditField(Rect position, SerializedProperty matrixProperty)
	{
		MatrixEditField(position, matrixProperty, 4, 4);
	}
	private static void MatrixEditField(Rect position, SerializedProperty matrixProperty, int rowCount, int columnCount)
	{
		var singleCell = new Vector2(
			position.width / columnCount,
			position.height / rowCount);

		var iterator = matrixProperty.Copy();

		for (int i = 0; i < rowCount; i++)
		{
			for (int j = 0; j < columnCount; j++)
			{
				iterator.Next(true);

				var cellPosition = position;
				cellPosition.width = singleCell.x;
				cellPosition.height = singleCell.y;
				cellPosition.x += singleCell.x * j;
				cellPosition.y += singleCell.y * i;

				iterator.floatValue = EditorGUI.FloatField(cellPosition, iterator.floatValue);
			}
		}
	}

	private static void ControlField(Rect buttonPosition, SerializedProperty matrixProperty)
	{
		var copyButtonPosition = buttonPosition;
		copyButtonPosition.xMin = buttonPosition.xMin + buttonPosition.width * 0.4f;
		copyButtonPosition.xMax = buttonPosition.xMin + buttonPosition.width * 0.7f;
		if (GUI.Button(copyButtonPosition, "Copy"))
		{
			MatrixToClipboard(matrixProperty);
		}

		var pasteButtonPosition = buttonPosition;
		pasteButtonPosition.xMin = buttonPosition.xMin + buttonPosition.width * 0.7f;
		pasteButtonPosition.xMax = buttonPosition.xMin + buttonPosition.width * 1f;
		if (GUI.Button(pasteButtonPosition, "Paste"))
		{
			MatrixFromClipboard(matrixProperty);
		}
	}

	private static void MatrixToClipboard(SerializedProperty matrixProperty)
	{
		var matrix = matrixProperty.GetMatrixValue();
		GUIUtility.systemCopyBuffer = MatrixUtility.CreateCSV(matrix);
	}

	private static void MatrixFromClipboard(SerializedProperty matrixProperty)
	{
		Matrix4x4 matrix;
		var csv = GUIUtility.systemCopyBuffer;
		if (MatrixUtility.TryParse(csv, out matrix))
		{
			matrixProperty.SetMatrixValue(matrix);
		}
	}

}
