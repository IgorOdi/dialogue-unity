using UnityEngine;

namespace Dialogues.Model {

    internal class DialoguePreferences : ScriptableObject {

        internal static DialoguePreferences Instance { get; private set; }

        [SerializeField, Range (0.01f, 0.1f)]
        internal float BaseWriteTime = 0.03f;
        [SerializeField]
        internal string DialogueScene;

        internal static DialoguePreferences Init () {

            Instance = Resources.Load ("Preferences") as DialoguePreferences;
            return Instance;
        }

    }
}