using System.Collections;
using Dialogues.Model.Core;
using Dialogues.View;
using UnityEngine;

namespace Dialogues.Controller.Core {

    public class DialogueController : MonoBehaviour {

        [HideInInspector] public bool _isDisplayingDialogue;
        private DialogueAsset _currentDialogueAsset;
        private int _currentDialogueIndex;

        private BaseDialogueViewer _dialogueViewer;
        private Coroutine _updateCoroutine;

        private KeyCode keyCode = KeyCode.Mouse0;

        public void ShowDialogue (DialogueAsset dialogueAsset) {

            _isDisplayingDialogue = true;
            DialogueManager.Instance.RunDialogue ((dialogueViewer) => {

                _dialogueViewer = dialogueViewer;

                _dialogueViewer.ConfigureDialogue (dialogueAsset.Dialogues[0]);
                _dialogueViewer.DialogueAnimator.OpenDialogueBox (() => {

                    _currentDialogueAsset = dialogueAsset;
                    if (_updateCoroutine != null) {

                        StopCoroutine (_updateCoroutine);
                    }
                    _updateCoroutine = StartCoroutine (UpdateDialogue (dialogueAsset));
                });
            });
        }

        public IEnumerator UpdateDialogue (DialogueAsset dialogueAsset) {

            _currentDialogueIndex = 0;
            _dialogueViewer.TextWriter.WriteText ();
            while (_currentDialogueIndex < _currentDialogueAsset.Dialogues.Count) {

                if (Input.GetKeyDown (keyCode)) {

                    if (_dialogueViewer.TextWriter.IsFilling) {

                        _dialogueViewer.TextWriter.AutoFillText ();
                    } else if (_currentDialogueIndex + 1 < _currentDialogueAsset.Dialogues.Count) {

                        _currentDialogueIndex++;
                        _dialogueViewer.ConfigureDialogue (dialogueAsset.Dialogues[_currentDialogueIndex]);
                        _dialogueViewer.TextWriter.WriteText ();
                    } else {

                        FinishDialogue ();
                    }
                }

                yield return null;
            }
        }

        private void FinishDialogue () {

            //TODO: Check Conditions to Next
            StopCoroutine (_updateCoroutine);
            _dialogueViewer.DialogueAnimator.CloseDialogueBox (() => {

                _isDisplayingDialogue = false;
                DialogueManager.Instance.FinishDialogue (null);
            });
        }
    }
}