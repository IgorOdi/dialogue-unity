using UnityEngine;
using UnityEditor;

namespace Dialogues.View.Core.Editor {

	[CustomEditor (typeof (DialogueBoxLayoutData))]
	[CanEditMultipleObjects]
	public class DialogueBoxLayoutDataEditor : UnityEditor.Editor {

		private DialogueBoxLayoutData convertedTarget;
		private SerializedProperty rectTransforms;
		private SerializedProperty layoutDatas;
		private bool layoutFold = true;

		void OnEnable() {

			rectTransforms = serializedObject.FindProperty ("rectTransforms");
			layoutDatas = serializedObject.FindProperty ("layoutDatas");
			convertedTarget = (DialogueBoxLayoutData) target;
		}

		public override void OnInspectorGUI() {

			serializedObject.Update ();
			EditorGUILayout.PropertyField (rectTransforms);

			layoutFold = EditorGUILayout.Foldout (layoutFold, "Layout Datas");
			if (layoutFold) {

				EditorGUI.indentLevel++;
				layoutDatas.arraySize = EditorGUILayout.IntField ("Size", layoutDatas.arraySize);
				for (int i = 0; i < layoutDatas.arraySize; i++) {

					EditorGUILayout.PropertyField (layoutDatas.GetArrayElementAtIndex (i));
					EditorGUILayout.BeginHorizontal ();
					if (GUILayout.Button ("Configure this Set to Dialogue Box")) convertedTarget.ConfigureSet (i);
					if (GUILayout.Button ("Copy Current Configuration")) convertedTarget.SetPosition (i);
					EditorGUILayout.EndHorizontal ();
				}
			}

			EditorGUILayout.Space ();
			if (GUILayout.Button ("Add Current as Preset")) {

				convertedTarget.AddPosition ();
			}
			serializedObject.ApplyModifiedProperties ();
		}

	}
}