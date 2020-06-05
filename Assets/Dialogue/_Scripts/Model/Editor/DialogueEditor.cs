﻿using UnityEditor;
using UnityEngine;

namespace Dialogues.Model.Editor {

    [CustomPropertyDrawer (typeof (Dialogue))]
    public class DialogueEditor : PropertyDrawer {

        private SerializedProperty _characterProperty;
        private SerializedProperty _expressionProperty;
        private SerializedProperty _textProperty;

        private const int LINE_AMOUNT = 3;
        private const int TEXT_LINE_AMOUNT = 4;

        private const string CHARACTER_PROPERTY_NAME = "Character";
        private const string EXPRESSION_PROPERTY_NAME = "ExpressionIndex";
        private const string TEXT_PROPERTY_NAME = "Text";

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {

            return property.isExpanded ? base.GetPropertyHeight (property, label) * LINE_AMOUNT + EditorGUIUtility.singleLineHeight * TEXT_LINE_AMOUNT :
                EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {

            EditorGUI.BeginProperty (position, label, property);

            position.height = EditorGUIUtility.singleLineHeight;
            property.isExpanded = EditorGUI.Foldout (position, property.isExpanded, label);

            if (property.isExpanded) {

                _characterProperty = property.FindPropertyRelative (CHARACTER_PROPERTY_NAME);
                _expressionProperty = property.FindPropertyRelative (EXPRESSION_PROPERTY_NAME);
                _textProperty = property.FindPropertyRelative (TEXT_PROPERTY_NAME);

                EditorGUI.indentLevel++;

                var characterRect = new Rect (position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
                var expressionRect = new Rect (position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width, EditorGUIUtility.singleLineHeight);
                var textRect = new Rect (position.x, position.y + EditorGUIUtility.singleLineHeight * 3, position.width, EditorGUIUtility.singleLineHeight * TEXT_LINE_AMOUNT);

                _characterProperty.objectReferenceValue = EditorGUI.ObjectField (characterRect, CHARACTER_PROPERTY_NAME,
                    _characterProperty.objectReferenceValue, typeof (Character), false);

                _expressionProperty.intValue = EditorGUI.Popup (expressionRect, EXPRESSION_PROPERTY_NAME,
                    _expressionProperty.intValue, GetExpressionNames (_characterProperty));

                _textProperty.stringValue = EditorGUI.TextField (textRect, TEXT_PROPERTY_NAME, _textProperty.stringValue);

            }
            EditorGUI.EndProperty ();
        }

        private string[] GetExpressionNames (SerializedProperty characterProperty) {

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