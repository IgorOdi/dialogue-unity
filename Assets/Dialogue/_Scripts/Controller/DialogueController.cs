using System.Collections;
using System.Collections.Generic;
using Dialogues.Model;
using Dialogues.View;
using UnityEngine;

namespace Dialogues.Controller {

    public class DialogueController : MonoBehaviour {

        [HideInInspector] public bool _isDisplayingDialogue;
        private DialogueAsset _currentDialogueAsset;
        private int _currentDialogueIndex;

        private Coroutine _updateCoroutine;
        private TextWriter _textWriter;
        private DialogueAnimator _dialogueAnimator;

        private KeyCode keyCode = KeyCode.Mouse0;

        public void ShowDialogue (DialogueAsset dialogueAsset) {

            _isDisplayingDialogue = true;
            DialogueManager.Instance.RunDialogue ((textWriter, dialogueAnimator) => {

                _textWriter = textWriter;
                _dialogueAnimator = dialogueAnimator;

                _textWriter.ConfigureDialogue (dialogueAsset.Dialogues[0]);
                _dialogueAnimator.OpenDialogueBox (() => {

                    _currentDialogueAsset = dialogueAsset;
                    if (_updateCoroutine != null) {

                        _textWriter.AutoFillCurrentDialogue ();
                        StopCoroutine (_updateCoroutine);
                    }
                    StartCoroutine (UpdateDialogue (dialogueAsset));
                });
            });
        }

        public IEnumerator UpdateDialogue (DialogueAsset dialogueAsset) {

            _currentDialogueIndex = 0;
            ConfigureAndWriteDialogue (dialogueAsset.Dialogues[_currentDialogueIndex]);
            while (_currentDialogueIndex < _currentDialogueAsset.Dialogues.Count) {

                if (Input.GetKeyDown (keyCode)) {

                    if (_textWriter.Filling) {

                        _textWriter.AutoFillCurrentDialogue ();
                    } else if (_currentDialogueIndex + 1 < _currentDialogueAsset.Dialogues.Count) {

                        _currentDialogueIndex++;
                        ConfigureAndWriteDialogue (dialogueAsset.Dialogues[_currentDialogueIndex]);
                    } else {

                        FinishDialogueAssetDialogue ();
                    }
                }

                yield return null;
            }
        }

        private void ConfigureAndWriteDialogue (Dialogue dialogue) {

            _textWriter.ConfigureDialogue (dialogue);
            _textWriter.WriteText ();
        }

        private void FinishDialogueAssetDialogue () {

            //TODO: Check Conditions to Next
            _dialogueAnimator.CloseDialogueBox (() => {

                _isDisplayingDialogue = false;
                DialogueManager.Instance.FinishDialogue (null);
            });
        }
    }
}