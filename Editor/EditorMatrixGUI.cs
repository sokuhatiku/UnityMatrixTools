using UnityEngine;
using UnityEditor;

public static class EditorMatrixGUI
{
	public static float AdvancedMatrixFieldHeight
	{
		get
		{
			return EditorGUIUtility.singleLineHeight * 4;
		}
	}

	public static void AdvancedMatrixField(Rect position, SerializedProperty matrixProperty)
	{
		var matrix = matrixProperty.GetMatrixValue();
		var newMatrix = matrix;

		if (matrixProperty.hasMultipleDifferentValues)
			EditorGUI.showMixedValue = true;
		EditorGUI.BeginChangeCheck();

		newMatrix = AdvancedMatrixField(position, matrixProperty.displayName, matrix);

		if (EditorGUI.EndChangeCheck())
			matrixProperty.SetMatrixValue(newMatrix);
		EditorGUI.showMixedValue = false;
	}

	private static Matrix4x4 AdvancedMatrixField(Rect position, string label, Matrix4x4 matrix)
	{
		var content = new GUIContent(label);
		return AdvancedMatrixField(position, content, matrix);
	}

	private static Matrix4x4 AdvancedMatrixField(Rect position, GUIContent label, Matrix4x4 matrix)
	{
		float rightSideWidth = Mathf.Max(180, position.width * 0.6f);
		float leftSideWidth = position.width - rightSideWidth;

		var leftSidePosition = position;
		leftSidePosition.width = leftSideWidth;

		var headerPosition = leftSidePosition;
		headerPosition.height = EditorGUIUtility.singleLineHeight;
		EditorGUI.LabelField(headerPosition, label);

		var tablePosition = position;
		tablePosition.xMin += leftSideWidth;
		matrix = DrawMatrixTable(tablePosition, matrix);

		var copyButtonPosition = headerPosition;
		copyButtonPosition.width = Mathf.Min(100, leftSideWidth);
		copyButtonPosition.y += copyButtonPosition.height * 2;
		matrix = DrawCopyButton(matrix, copyButtonPosition);

		var pasteButtonPosition = copyButtonPosition;
		pasteButtonPosition.y += pasteButtonPosition.height;
		matrix = DrawPasteButton(matrix, pasteButtonPosition);

		return matrix;
	}

	private static Matrix4x4 DrawCopyButton(Matrix4x4 matrix, Rect copyButtonPosition)
	{
		if (GUI.Button(copyButtonPosition, "Copy"))
		{
			MatrixToClipboard(ref matrix);
		}

		return matrix;
	}

	private static Matrix4x4 DrawPasteButton(Matrix4x4 matrix, Rect pasteButtonPosition)
	{
		if (GUI.Button(pasteButtonPosition, "Paste"))
		{
			MatrixFromClipboard(ref matrix);
		}

		return matrix;
	}

	private static Matrix4x4 DrawMatrixTable(Rect position, Matrix4x4 matrix)
	{
		var defaultLabelWidth = EditorGUIUtility.labelWidth;
		EditorGUIUtility.labelWidth = 18;
		TableLayout(position, 4, 4, (cellPosition, row, column) =>
		{
			var labelText = row + "" + column;
			matrix[column, row] = EditorGUI.FloatField(cellPosition, labelText, matrix[column, row]);
		});
		EditorGUIUtility.labelWidth = defaultLabelWidth;

		return matrix;
	}

	private delegate void TableLayoutCellAction(Rect cellPosition, int row, int column);

	private static void TableLayout(Rect position, float rowCount, float columnCount, TableLayoutCellAction action)
	{
		var singleCell = new Vector2(
			position.width / columnCount,
			position.height / rowCount);

		for (int i = 0; i < rowCount; i++)
		{
			for (int j = 0; j < columnCount; j++)
			{
				var cellPosition = position;
				cellPosition.width = singleCell.x;
				cellPosition.height = singleCell.y;
				cellPosition.x += singleCell.x * j;
				cellPosition.y += singleCell.y * i;

				action(cellPosition, i, j);
			}
		}
	}

	private static void MatrixToClipboard(ref Matrix4x4 matrix)
	{
		GUIUtility.systemCopyBuffer = MatrixUtility.CreateCSV(matrix);
	}

	private static void MatrixFromClipboard(ref Matrix4x4 matrix)
	{
		var csv = GUIUtility.systemCopyBuffer;
		MatrixUtility.TryParse(csv, out matrix);
	}
}
