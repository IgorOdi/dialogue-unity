using Dialogues.Model.Core;
using UnityEngine;

namespace Dialogues.View {

    public abstract class BaseDialogueViewer : MonoBehaviour {

        public BaseTextWriter TextWriter { get; set; }
        public DialogueAnimator DialogueAnimator { get; set; }

        public virtual void ConfigureViewer () {

            TextWriter = GetComponent<BaseTextWriter> ();
            DialogueAnimator = GetComponent<DialogueAnimator> ();
        }

        public virtual void ConfigureDialogue (Dialogue dialogue) { }
    }
}