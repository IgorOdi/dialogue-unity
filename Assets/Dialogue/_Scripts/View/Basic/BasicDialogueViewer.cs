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

		private BasicDialogue cachedBasicDialogue;

		public override void ConfigureDialogue(BaseDialogue dialogue) {

			cachedBasicDialogue = (BasicDialogue) dialogue;

			_characterSprite.sprite = cachedBasicDialogue.GetExpressionFromIndex ().Sprite;
			CopyFromLayoutData (cachedBasicDialogue.Side.Equals (Sides.LEFT) ? 0 : 1);
			TextWriter.SetTextAndBox (dialogue.Text, _textBox);
		}
	}
}