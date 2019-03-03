using UnityEngine;

namespace Sokuhatiku.MatrixTools
{
	public static class EditorMatrixUtility
	{
		public static void MatrixToClipboard(ref Matrix4x4 matrix)
		{
			GUIUtility.systemCopyBuffer = MatrixUtility.CreateCSV(matrix);
		}

		public static void MatrixFromClipboard(ref Matrix4x4 matrix)
		{
			var csv = GUIUtility.systemCopyBuffer;
			MatrixUtility.TryParse(csv, out matrix);
		}
	}
}