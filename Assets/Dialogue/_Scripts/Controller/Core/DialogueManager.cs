using System;
using Dialogues.View;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dialogues.Controller.Core {

    public class DialogueManager : MonoBehaviour {

        public static DialogueManager Instance {
            get { return _instance; }
            private set { }
        }
        private static DialogueManager _instance;
        private const string DIALOGUE_SCENE_NAME = "BasicDialogue";

        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InstantiateManager () {

            GameObject manager = new GameObject ("DialogueManager");
            manager.AddComponent (typeof (DialogueManager));
            _instance = manager.GetComponent<DialogueManager> ();
            Model.DialoguePreferences.Init ();
            DontDestroyOnLoad (manager);
        }

        public void RunDialogue (string dialogueSceneName, Action<BaseDialogueViewer> callback) {

            InternalUnloadDialogue (DIALOGUE_SCENE_NAME, null);
            InternalRunDialogue (dialogueSceneName, callback);
        }

        public void RunDialogue (Action<BaseDialogueViewer> callback) {

            InternalUnloadDialogue (DIALOGUE_SCENE_NAME, null);
            InternalRunDialogue (DIALOGUE_SCENE_NAME, callback);
        }

        public void UnloadDialogue (string dialogueSceneName, Action callback) {

            InternalUnloadDialogue (dialogueSceneName, callback);
        }

        public void UnloadDialogue (Action callback) {

            InternalUnloadDialogue (DIALOGUE_SCENE_NAME, callback);
        }

        private void InternalRunDialogue (string sceneName, Action<BaseDialogueViewer> callback) {

            SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive).completed += (operation) => {

                var dialogueViewer = FindObjectOfType<BaseDialogueViewer> ();
                dialogueViewer.ConfigureViewer ();
                callback?.Invoke (dialogueViewer);
            };
        }

        private void InternalUnloadDialogue (string sceneName, Action callback) {

            if (IsSceneActive (sceneName)) {
                SceneManager.UnloadSceneAsync (DIALOGUE_SCENE_NAME).completed += (operation) => {

                    callback?.Invoke ();
                };
            }
        }

        private bool IsSceneActive (string sceneName) {

            for (int i = 0; i < SceneManager.sceneCount; i++) {

                if (SceneManager.GetSceneAt (i).name == sceneName)
                    return true;
            }
            return false;
        }
    }
}