using System;
using Dialogues.Model;
using Dialogues.View;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dialogues.Controller.Core {

    internal class DialogueManager : MonoBehaviour {

        internal static DialogueManager Instance {
            get { return _instance; }
            private set { }
        }
        private static DialogueManager _instance;

        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void InstantiateManager () {

            if (!DialoguePreferences.SystemEnabled) return;

            GameObject manager = new GameObject ("DialogueManager");
            manager.AddComponent (typeof (DialogueManager));
            _instance = manager.GetComponent<DialogueManager> ();
            Model.DialoguePreferences.Init ();
            DontDestroyOnLoad (manager);
        }

        internal void RunDialogue (Action<BaseDialogueViewer> callback) {

            InternalUnloadDialogue (DialoguePreferences.Instance.DialogueScene, () => {

                InternalRunDialogue (DialoguePreferences.Instance.DialogueScene, callback);
            });
        }

        internal void UnloadDialogue (Action callback) {

            InternalUnloadDialogue (DialoguePreferences.Instance.DialogueScene, callback);
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
                SceneManager.UnloadSceneAsync (sceneName).completed += (operation) => {

                    callback?.Invoke ();
                };
            } else {

                callback?.Invoke ();
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