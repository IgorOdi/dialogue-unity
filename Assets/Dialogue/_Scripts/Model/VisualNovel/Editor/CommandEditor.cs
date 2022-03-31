using Dialogues.Model.Core;
using Dialogues.Model.Enum;
using UnityEditor;
using UnityEngine;

namespace Dialogues.Model.VisualNovel.Editor {

	[CustomPropertyDrawer (typeof (Command))]
	public class CommandEditor : PropertyDrawer {

		private const int maxIndex = 4;

		private SerializedProperty commandTypeProperty;
		private SerializedProperty sideProperty;
		private SerializedProperty characterProperty;
		private SerializedProperty expressionProperty;

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {

			var _commandTypeProperty = property.FindPropertyRelative ("CommandType");
			int commandMultiplier = _commandTypeProperty.enumValueIndex.Equals (1) ? 2 : 1;
			return EditorGUIUtility.singleLineHeight * commandMultiplier;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

			EditorGUI.BeginProperty (position, label, property);

			commandTypeProperty = property.FindPropertyRelative ("CommandType");
			sideProperty = property.FindPropertyRelative ("Side");
			characterProperty = property.FindPropertyRelative ("Character");
			expressionProperty = property.FindPropertyRelative ("Expression");

			Rect commandTypeRect = new Rect (position.x, position.y, position.width / 1.5f, EditorGUIUtility.singleLineHeight);
			Rect characterRect = new Rect (position.x + position.width / 1.65f, position.y, position.width / 2.55f, EditorGUIUtility.singleLineHeight);

			EditorGUI.PropertyField (commandTypeRect, commandTypeProperty);
			EditorGUI.ObjectField (characterRect, characterProperty, new GUIContent (""));

			if (commandTypeProperty.enumValueIndex.Equals (1)) return;

			Rect sideRect = new Rect (position.x, position.y + EditorGUIUtility.singleLineHeight, position.width / 1.5f, EditorGUIUtility.singleLineHeight);
			Rect expressionRect = new Rect (position.x + position.width / 1.65f, position.y + EditorGUIUtility.singleLineHeight, position.width / 2.55f, EditorGUIUtility.singleLineHeight);

			sideProperty.enumValueIndex = EditorGUI.Popup (sideRect, "Side", sideProperty.enumValueIndex, sideProperty.enumDisplayNames);
			expressionProperty.intValue = EditorGUI.Popup (expressionRect, expressionProperty.intValue, GetExpressionNames (characterProperty));

			EditorGUI.EndProperty ();
		}

		private string[] GetExpressionNames(SerializedProperty characterProperty) {

			Character character = (Character) characterProperty.objectReferenceValue;
			if (character == null) return new string[0];
			string[] names = new string[character.Expressions.Count];
			for (int i = 0; i < names.Length; i++) {

				names[i] = character.Expressions[i].Name;
			}
			return names;
		}
	}
}