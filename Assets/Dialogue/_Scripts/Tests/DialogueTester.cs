using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTester : MonoBehaviour {

    public Dialogues.Model.Core.DialogueAsset dialogueAsset;

    void Start () {

        FindObjectOfType<Dialogues.Controller.Core.DialogueController> ().ShowDialogue (dialogueAsset);
    }

    void Update () {

        if (Input.GetKeyDown (KeyCode.Space)) {

            FindObjectOfType<Dialogues.Controller.Core.DialogueController> ().ShowDialogue (dialogueAsset);
        }

        if (Input.GetKeyDown (KeyCode.P)) {

            for (int i = 0; i < SceneManager.sceneCount; i++) {

                Debug.Log (i);
            }
        }
    }
}