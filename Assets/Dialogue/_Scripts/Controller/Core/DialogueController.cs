using System;
using System.Collections;
using System.Collections.Generic;
using Dialogues.Model.Core;
using Dialogues.View.Core;
using UnityEngine;

namespace Dialogues.Controller.Core {

	public class DialogueController : MonoBehaviour {

		public Action OnDialogueBoxOpen;
		public Action OnDialogueBoxClose;
		public Action OnDialogueFinishes;
		public Action OnDialogueSequenceFinishes;
		public DialogueCustomScripts CustomScripts { get; private set; }

		public bool IsDisplayingDialogue { get; private set; }
		private int _dialogueCount;
		private int _currentDialogueIndex;

		private BaseDialogueViewer _dialogueViewer;
		private Coroutine _updateCoroutine;

		private KeyCode keyCode = KeyCode.Mouse0;

		private static Dictionary<string, string> SavedVariables = new Dictionary<string, string> ();

		void Awake() => CustomScripts = GetComponentInChildren<DialogueCustomScripts> ();

		public void ShowDialogue(BaseDialogueAsset dialogueAsset, bool reload = true) {

			IsDisplayingDialogue = true;

			if (!reload) {

				LoadDialogueViewer (dialogueAsset);
				return;
			}

			DialogueSceneManager.RunDialogue (this, (dialogueViewer) => {

				_dialogueViewer = dialogueViewer;
				_dialogueViewer.DialogueAnimator.OpenDialogueBox (() => {

					LoadDialogueViewer (dialogueAsset);
				});
			});
		}

		private void LoadDialogueViewer(BaseDialogueAsset dialogueAsset) {

			_dialogueViewer.ConfigureDialogue (dialogueAsset.GetDialogues ()[0]);
			_dialogueCount = dialogueAsset.GetDialogues ().Count;
			if (_updateCoroutine != null) {

				StopCoroutine (_updateCoroutine);
			}
			_updateCoroutine = StartCoroutine (UpdateDialogue (dialogueAsset));

			OnDialogueBoxOpen?.Invoke ();
			OnDialogueBoxOpen = null;
		}

		public IEnumerator UpdateDialogue(BaseDialogueAsset dialogueAsset) {

			_currentDialogueIndex = 0;
			_dialogueViewer.TextWriter.WriteText (_dialogueViewer.ShowChoicesButton);
			while (_currentDialogueIndex < _dialogueCount) {

				if (Input.GetKeyDown (keyCode)) {

					if (_dialogueViewer.TextWriter.IsFilling) {

						_dialogueViewer.TextWriter.AutoFillText ();
					} else if (_currentDialogueIndex + 1 < _dialogueCount) {

						_currentDialogueIndex++;
						_dialogueViewer.ConfigureDialogue (dialogueAsset.GetDialogues ()[_currentDialogueIndex]);
						_dialogueViewer.TextWriter.WriteText (_dialogueViewer.ShowChoicesButton);
					} else if (_dialogueViewer.IsShowingChoices) {

						yield return null;
					} else {

						FinishDialogue ();
					}
				}

				yield return null;
			}
		}

		public void FinishDialogue(BaseDialogueAsset nextDialogueAsset = null) {

			OnDialogueFinishes?.Invoke ();
			OnDialogueFinishes = null;

			StopCoroutine (_updateCoroutine);

			if (nextDialogueAsset != null) {

				ShowDialogue (nextDialogueAsset, false);
				return;
			}

			_dialogueViewer.DialogueAnimator.CloseDialogueBox (() => {

				IsDisplayingDialogue = false;
				DialogueSceneManager.UnloadDialogue (() => {

					OnDialogueBoxClose?.Invoke ();
					OnDialogueSequenceFinishes?.Invoke ();

					OnDialogueBoxClose = null;
					OnDialogueSequenceFinishes = null;
				});

			});
		}

		public void SaveVariable(string name, string value) {

			if (!SavedVariables.ContainsKey (name))
				SavedVariables.Add (name, value);
			else
				SavedVariables[name] = value;
		}

		public string ReadVariable(string name) {

			string value = SavedVariables[name];
			if (string.IsNullOrEmpty (value)) return null;
			return value;
		}
	}
}