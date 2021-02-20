using Dialogues.Model;
using UnityEngine;

public class DialogueTester : MonoBehaviour {

    public Dialogues.Model.Core.DialogueAsset dialogueAsset;

    void Start () {

        FindObjectOfType<Dialogues.Controller.Core.DialogueController> ().ShowDialogue (dialogueAsset);
    }

    void Update () {

        if (Input.GetKeyDown (KeyCode.Space)) {

            DialoguePreferences.Instance.DialogueScene = "BigCharDialogue";
            FindObjectOfType<Dialogues.Controller.Core.DialogueController> ().ShowDialogue (dialogueAsset);
        }
    }
}