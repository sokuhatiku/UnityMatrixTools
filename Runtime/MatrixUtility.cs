using System;
using System.Text;
using UnityEngine;

public static class MatrixUtility
{
	public static string CreateCSV(Matrix4x4 matrix)
	{
		var builder = new StringBuilder();
		string separate = ", ";
		string newBreak = Environment.NewLine;
		for (int i = 0; i < 16; i++)
		{
			builder.Append(matrix[i]);

			var lineEnd = (i + 1) % 4 == 0;
			if (lineEnd)
			{
				builder.Append(newBreak);
			}
			else
			{
				builder.Append(separate);
			}
		}

		return builder.ToString();
	}

	public static bool TryParse(string csv, out Matrix4x4 matrix)
	{
		matrix = default(Matrix4x4);

		if (string.IsNullOrEmpty(csv))
			return false;

		var strings = csv.Split(',', '\n');
		if (strings.Length < 16)
			return false;

		for (int i = 0; i < 16; i++)
		{
			float f;
			if (!float.TryParse(strings[i], out f))
				return false;
			matrix[i] = f;
		}

		return true;
	}
}
