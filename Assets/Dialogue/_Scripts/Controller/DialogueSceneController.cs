using System;
using System.Collections;
using System.Collections.Generic;
using Dialogues.View;
using UnityEngine;

namespace Dialogues.Controller {

    public class DialogueSceneController : MonoBehaviour {

        [SerializeField] private TextWriter _textWriter;
        [SerializeField] private DialogueAnimator _dialogueAnimator;

        public (TextWriter textWriter, DialogueAnimator dialogueAnimator) GetDialogueComponents () {

            return (_textWriter, _dialogueAnimator);
        }
    }
}