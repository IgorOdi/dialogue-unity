using UnityEditor;
using UnityEngine;

namespace Dialogues.Model.Basic.Editor
{

	[CustomPropertyDrawer (typeof (Choice))]
	public class ChoiceEditor : PropertyDrawer {

		private SerializedProperty _textProperty;
		private SerializedProperty _nextDialogueProperty;
		private SerializedProperty _variableNameProperty;
		private SerializedProperty _variableValueProperty;

		private const string TEXT_PROPERTY_NAME = "Text";
		private const string NEXTDIALOGUE_PROPERTY_NAME = "NextDialogueAsset";
		private const string VARIABLENAME_PROPERTY_NAME = "VariableName";
		private const string VARIABLEVALUE_PROPERTY_NAME = "VariableValue";

		private const string OUTPUT_VALUE = "Output Values";

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {

			return property.isExpanded ? base.GetPropertyHeight (property, label) * 4 + EditorGUIUtility.singleLineHeight :
				EditorGUIUtility.singleLineHeight;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

			EditorGUI.BeginProperty (position, label, property);

			position.height = EditorGUIUtility.singleLineHeight;
			property.isExpanded = EditorGUI.Foldout (position, property.isExpanded, label);

			if (property.isExpanded) {

				_textProperty = property.FindPropertyRelative (TEXT_PROPERTY_NAME);
				_nextDialogueProperty = property.FindPropertyRelative (NEXTDIALOGUE_PROPERTY_NAME);
				_variableNameProperty = property.FindPropertyRelative (VARIABLENAME_PROPERTY_NAME);
				_variableValueProperty = property.FindPropertyRelative (VARIABLEVALUE_PROPERTY_NAME);

				var textFieldRect = new Rect (position.x, position.y + EditorGUIUtility.singleLineHeight * 1, position.width, EditorGUIUtility.singleLineHeight);
				var nextDialogueRect = new Rect (position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width, EditorGUIUtility.singleLineHeight);
				
                var outputRect = new Rect (position.x, position.y + EditorGUIUtility.singleLineHeight * 3, position.width, EditorGUIUtility.singleLineHeight);
                var variableNameRect = new Rect (position.x, position.y + EditorGUIUtility.singleLineHeight * 4, position.width / 2, EditorGUIUtility.singleLineHeight);
				var variableValueRect = new Rect (position.x + position.width / 2, position.y + EditorGUIUtility.singleLineHeight * 4, position.width / 2, EditorGUIUtility.singleLineHeight);

				_textProperty.stringValue = EditorGUI.TextField (textFieldRect, TEXT_PROPERTY_NAME, _textProperty.stringValue);

				_nextDialogueProperty.objectReferenceValue = EditorGUI.ObjectField (nextDialogueRect, NEXTDIALOGUE_PROPERTY_NAME, _nextDialogueProperty.objectReferenceValue,
					typeof (BasicDialogueEditor), false);

                EditorGUI.LabelField(outputRect, OUTPUT_VALUE, EditorStyles.boldLabel);

				EditorGUI.indentLevel++;

				_variableNameProperty.stringValue = EditorGUI.TextField (variableNameRect, "Name", _variableNameProperty.stringValue);
				_variableValueProperty.stringValue = EditorGUI.TextField (variableValueRect, "Value", _variableValueProperty.stringValue);

				EditorGUI.indentLevel--;
			}

			EditorGUI.EndProperty ();
		}
	}
}