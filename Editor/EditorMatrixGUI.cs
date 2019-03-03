using UnityEngine;
using UnityEditor;

namespace Sokuhatiku.MatrixTools
{
	public static class EditorMatrixGUI
	{
		public static float MatrixFieldHeight
		{
			get
			{
				return EditorGUIUtility.singleLineHeight * 4;
			}
		}

		public static void MatrixField(Rect position, SerializedProperty matrixProperty)
		{
			MatrixField(position, matrixProperty.displayName, matrixProperty);
		}

		public static void MatrixField(Rect position, string label, SerializedProperty matrixProperty)
		{
			var content = new GUIContent(label);
			MatrixField(position, content, matrixProperty);
		}

		public static void MatrixField(Rect position, GUIContent label, SerializedProperty matrixProperty)
		{
			var matrix = matrixProperty.GetMatrixValue();
			var newMatrix = matrix;

			if (matrixProperty.hasMultipleDifferentValues)
				EditorGUI.showMixedValue = true;
			EditorGUI.BeginChangeCheck();

			newMatrix = MatrixField(position, label, matrix);

			if (EditorGUI.EndChangeCheck())
				matrixProperty.SetMatrixValue(newMatrix);
			EditorGUI.showMixedValue = false;
		}

		public static Matrix4x4 MatrixField(Rect position, string label, Matrix4x4 matrix)
		{
			var content = new GUIContent(label);
			return MatrixField(position, content, matrix);
		}

		public static Matrix4x4 MatrixField(Rect position, GUIContent label, Matrix4x4 matrix)
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
			matrix = DrawCopyButton(copyButtonPosition, matrix);

			var pasteButtonPosition = copyButtonPosition;
			pasteButtonPosition.y += pasteButtonPosition.height;
			matrix = DrawPasteButton(pasteButtonPosition, matrix);

			return matrix;
		}


		private static Matrix4x4 DrawCopyButton(Rect position, Matrix4x4 matrix)
		{
			EditorGUI.BeginDisabledGroup(EditorGUI.showMixedValue);
			if (GUI.Button(position, "Copy"))
			{
				EditorMatrixUtility.MatrixToClipboard(ref matrix);
			}
			EditorGUI.EndDisabledGroup();

			return matrix;
		}

		private static Matrix4x4 DrawPasteButton(Rect position, Matrix4x4 matrix)
		{
			if (GUI.Button(position, "Paste"))
			{
				EditorMatrixUtility.MatrixFromClipboard(ref matrix);
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

		private delegate void TableLayoutCellAction(Rect position, int row, int column);

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
	}
}