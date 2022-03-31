using System.Collections.Generic;
using Dialogues.Model.Core;
using Dialogues.Model.Enum;
using UnityEditor;
using UnityEngine;

namespace Dialogues.Model.VisualNovel.Editor {

	[CustomPropertyDrawer (typeof (VisualNovelDialogue))]
	public class VisualNovelDialogueEditor : PropertyDrawer {

		private SerializedProperty characterProperty;
		private SerializedProperty expressionProperty;
		private SerializedProperty textProperty;
		private SerializedProperty sideProperty;
		private SerializedProperty commandProperty;

		private const int LINE_AMOUNT = 5;
		private const int TEXT_LINE_AMOUNT = 4;

		private const string CHARACTER_PROPERTY_NAME = "Character";
		private const string EXPRESSION_PROPERTY_NAME = "ExpressionIndex";
		private const string TEXT_PROPERTY_NAME = "Text";
		private const string SIDE_PROPERTY_NAME = "Side";
		private const string COMMAND_PROPERTY_NAME = "Command";

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {

			var commandProperty = property.FindPropertyRelative (COMMAND_PROPERTY_NAME);
			int commandArraySize = commandProperty.isExpanded ? commandProperty.arraySize * 2 : 0;
			return property.isExpanded ? base.GetPropertyHeight (property, label) * LINE_AMOUNT + EditorGUIUtility.singleLineHeight *
				(TEXT_LINE_AMOUNT + commandArraySize) :
				EditorGUIUtility.singleLineHeight;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

			EditorGUI.BeginProperty (position, label, property);

			position.height = EditorGUIUtility.singleLineHeight;
			property.isExpanded = EditorGUI.Foldout (position, property.isExpanded, label);

			if (property.isExpanded) {

				characterProperty = property.FindPropertyRelative (CHARACTER_PROPERTY_NAME);
				expressionProperty = property.FindPropertyRelative (EXPRESSION_PROPERTY_NAME);
				textProperty = property.FindPropertyRelative (TEXT_PROPERTY_NAME);
				sideProperty = property.FindPropertyRelative (SIDE_PROPERTY_NAME);
				commandProperty = property.FindPropertyRelative (COMMAND_PROPERTY_NAME);

				EditorGUI.indentLevel++;

				var characterRect = new Rect (position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
				characterProperty.objectReferenceValue = EditorGUI.ObjectField (characterRect, CHARACTER_PROPERTY_NAME,
					characterProperty.objectReferenceValue, typeof (Character), false);

				var expressionRect = new Rect (position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width, EditorGUIUtility.singleLineHeight);
				expressionProperty.intValue = EditorGUI.Popup (expressionRect, EXPRESSION_PROPERTY_NAME,
					expressionProperty.intValue, GetExpressionNames (characterProperty));

				var sideRect = new Rect (position.x, position.y + EditorGUIUtility.singleLineHeight * 3, position.width, EditorGUIUtility.singleLineHeight);
				sideProperty.enumValueIndex = (int) ((Sides) EditorGUI.EnumPopup (sideRect, SIDE_PROPERTY_NAME, (Sides) sideProperty.enumValueIndex));

				var commandBase = new Rect (position.x, position.y + EditorGUIUtility.singleLineHeight * 4, position.width / 2, EditorGUIUtility.singleLineHeight);
				var commandSize = new Rect (position.x + position.width / 2.85f, position.y + EditorGUIUtility.singleLineHeight * 4, position.width / 1.55f, EditorGUIUtility.singleLineHeight);

				commandProperty.arraySize = EditorGUI.IntField (commandSize, "Size", commandProperty.arraySize);
				commandProperty.isExpanded = EditorGUI.Foldout (commandBase, commandProperty.isExpanded, "Command");
				if (commandProperty.isExpanded) {

					EditorGUI.indentLevel++;

					var commandIndexRect = new Rect (position.x, position.y + EditorGUIUtility.singleLineHeight * 5, position.width, EditorGUIUtility.singleLineHeight);
					for (int i = 0; i < commandProperty.arraySize; i++) {

						var current = commandProperty.GetArrayElementAtIndex (i);
						commandIndexRect = new Rect (position.x, position.y + EditorGUIUtility.singleLineHeight * (5 + i * 2), position.width, EditorGUIUtility.singleLineHeight);
						EditorGUI.PropertyField (commandIndexRect, current);
					}
					EditorGUI.indentLevel--;
				}

				int textRectPrevArrayMultiplier = commandProperty.isExpanded ? commandProperty.arraySize : 0;
				var textRect = new Rect (position.x, position.y + EditorGUIUtility.singleLineHeight * (5 + textRectPrevArrayMultiplier * 2), position.width, EditorGUIUtility.singleLineHeight * TEXT_LINE_AMOUNT);
				textProperty.stringValue = EditorGUI.TextField (textRect, TEXT_PROPERTY_NAME, textProperty.stringValue);
			}
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