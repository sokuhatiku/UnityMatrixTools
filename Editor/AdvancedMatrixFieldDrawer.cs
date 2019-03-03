using UnityEngine;
using UnityEditor;

namespace Sokuhatiku.MatrixTools
{
	[CustomPropertyDrawer(typeof(AdvancedMatrixFieldAttribute))]
	public class AdvancedMatrixFieldDrawer : PropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorMatrixGUI.AdvancedMatrixFieldHeight;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorMatrixGUI.AdvancedMatrixField(position, property);
		}
	}
}