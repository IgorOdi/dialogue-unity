using Dialogues.Model.Core;
using Dialogues.Model.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogues.View {

	public sealed class BasicDialogueViewer : BaseDialogueViewer {

		[SerializeField] private Image _dialogueBox;
		[SerializeField] private Image _characterSprite;
		[SerializeField] private TextMeshProUGUI _textBox;

		private BasicDialogue cachedBasicDialogue;

		public override void ConfigureDialogue(BaseDialogue dialogue) {

			cachedBasicDialogue = (BasicDialogue) dialogue;

			_characterSprite.sprite = cachedBasicDialogue.GetExpressionFromIndex ().Sprite;
			CopyFromLayoutData (cachedBasicDialogue.Side.Equals (Sides.LEFT) ? 0 : 1);
			TextWriter.SetTextAndBox (dialogue.Text, _textBox);
		}
	}
}