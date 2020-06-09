using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogues.Model {

    public class DialoguePreferences : ScriptableObject {

        public static DialoguePreferences Instance { get; private set; }
        [Range(0.01f, 0.1f)]
        public float BaseWriteTime = 0.03f;
        public string DialogueScene;

        public static DialoguePreferences Init () {

            Instance = Resources.Load ("Preferences") as DialoguePreferences;
            return Instance;
        }

    }
}