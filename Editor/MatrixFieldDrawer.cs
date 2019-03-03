using UnityEngine;
using UnityEditor;

namespace Sokuhatiku.MatrixTools
{
	[CustomPropertyDrawer(typeof(MatrixFieldAttribute))]
	public class MatrixFieldDrawer : PropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorMatrixGUI.MatrixFieldHeight;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorMatrixGUI.MatrixField(position, property);
		}
	}
}