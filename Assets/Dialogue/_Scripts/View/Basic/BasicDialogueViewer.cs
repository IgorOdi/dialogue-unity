using Dialogues.Controller.Core;
using Dialogues.Model.Basic;
using Dialogues.Model.Core;
using Dialogues.Model.Enum;
using Dialogues.View.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogues.View {
	public class BasicDialogueViewer : BaseDialogueViewer {

		[SerializeField] protected Image _characterSprite;
		[SerializeField] protected TextMeshProUGUI _textBox;
		[SerializeField] protected Button[] _choicesObjects = new Button[4];
		[SerializeField] protected TextMeshProUGUI[] _choicesTexts = new TextMeshProUGUI[MAX_DIALOGUE_CHOICES];

		private const int MAX_DIALOGUE_CHOICES = 4;

		private BasicDialogue _cachedBasicDialogue;

		public override void ConfigureDialogue(BaseDialogue dialogue) {

			_cachedBasicDialogue = (BasicDialogue) dialogue;

			for (int i = 0; i < _choicesObjects.Length; i++) {

				_choicesObjects[i].gameObject.SetActive (false);
				_choicesObjects[i].onClick.RemoveAllListeners ();
			}

			_characterSprite.sprite = _cachedBasicDialogue.GetExpressionFromIndex ().Sprite;
			CopyFromLayoutData (_cachedBasicDialogue.Side.Equals (Sides.LEFT) ? 0 : 1);
			TextWriter.SetTextAndBox (dialogue.Text, _textBox);

			if (_cachedBasicDialogue.Choices.Count > MAX_DIALOGUE_CHOICES)
				throw new System.Exception ("There's more choices than supported by this dialogue layout");

			for (int i = 0; i < _cachedBasicDialogue.Choices.Count; i++) {

				_choicesTexts[i].text = _cachedBasicDialogue.Choices[i].Text;
			}
		}

		public override void ShowChoicesButton() {

			IsShowingChoices = _cachedBasicDialogue.Choices.Count > 0;

			for (int i = 0; i < _cachedBasicDialogue.Choices.Count; i++) {

				_choicesObjects[i].gameObject.SetActive (true);

				Choice currentChoice = _cachedBasicDialogue.Choices[i];
				_choicesObjects[i].onClick.AddListener (() => OnButtonClick (currentChoice));
			}
		}

		private void OnButtonClick(Choice choice) {

			IsShowingChoices = false;

			if (!string.IsNullOrEmpty (choice.VariableName) && !string.IsNullOrEmpty (choice.VariableValue)) {

				DialogueController.SaveVariable (choice.VariableName, choice.VariableValue);
			}

			if (choice.NextDialogueAsset != null) {

				DialogueController.FinishDialogue (choice.NextDialogueAsset);
				return;
			}

			DialogueController.FinishDialogue ();
		}
	}
}