using System;
using System.Collections;
using Dialogues.Model.Core;
using Dialogues.View.Core;
using UnityEngine;

namespace Dialogues.Controller.Core {

	public class DialogueController : MonoBehaviour {

		public Action OnDialogueBoxOpen;
		public Action OnDialogueFinishes;
		public Action OnDialogueBoxClose;
		public DialogueCustomScripts CustomScripts { get; private set; }

		[HideInInspector] public bool _isDisplayingDialogue;
		private int _dialogueCount;
		private int _currentDialogueIndex;

		private BaseDialogueViewer _dialogueViewer;
		private Coroutine _updateCoroutine;

		private KeyCode keyCode = KeyCode.Mouse0;

		void Awake() => CustomScripts = GetComponentInChildren<DialogueCustomScripts> ();

		public void ShowDialogue(IDialogueAsset dialogueAsset) {

			_isDisplayingDialogue = true;
			DialogueSceneManager.RunDialogue ((dialogueViewer) => {

				_dialogueViewer = dialogueViewer;
				_dialogueViewer.TextWriter.Context = this;

				_dialogueViewer.ConfigureDialogue (dialogueAsset.GetDialogues ()[0]);
				_dialogueViewer.DialogueAnimator.OpenDialogueBox (() => {

					_dialogueCount = dialogueAsset.GetDialogues ().Count;
					//_currentDialogueAsset = dialogueAsset;
					if (_updateCoroutine != null) {

						StopCoroutine (_updateCoroutine);
					}
					_updateCoroutine = StartCoroutine (UpdateDialogue (dialogueAsset));
					OnDialogueBoxOpen?.Invoke ();
				});
			});
		}

		public IEnumerator UpdateDialogue(IDialogueAsset dialogueAsset) {

			_currentDialogueIndex = 0;
			_dialogueViewer.TextWriter.WriteText ();
			while (_currentDialogueIndex < _dialogueCount) {

				if (Input.GetKeyDown (keyCode)) {

					if (_dialogueViewer.TextWriter.IsFilling) {

						_dialogueViewer.TextWriter.AutoFillText ();
					} else if (_currentDialogueIndex + 1 < _dialogueCount) {

						_currentDialogueIndex++;
						_dialogueViewer.ConfigureDialogue (dialogueAsset.GetDialogues ()[_currentDialogueIndex]);
						_dialogueViewer.TextWriter.WriteText ();
					} else {

						FinishDialogue ();
					}
				}

				yield return null;
			}
		}

		private void FinishDialogue() {

			//TODO: Check Conditions to Next
			OnDialogueFinishes?.Invoke ();
			StopCoroutine (_updateCoroutine);
			_dialogueViewer.DialogueAnimator.CloseDialogueBox (() => {

				_isDisplayingDialogue = false;
				DialogueSceneManager.UnloadDialogue (OnDialogueBoxClose);
			});
		}
	}
}