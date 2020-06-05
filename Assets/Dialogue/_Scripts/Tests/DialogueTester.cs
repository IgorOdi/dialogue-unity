using UnityEngine;

public class DialogueTester : MonoBehaviour {

    public Dialogues.Model.DialogueAsset dialogueAsset;

    void Start () {

        FindObjectOfType<Dialogues.Controller.DialogueController> ().ShowDialogue (dialogueAsset);
    }
}