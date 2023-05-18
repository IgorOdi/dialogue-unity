using System.Collections.Generic;
using Dialogues.Controller.Core;
using Dialogues.Model.Core;
using Dialogues.Model.VisualNovel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogues.View.Core {

	public class VisualNovelDialogueViewer : BaseDialogueViewer {

		private VisualNovelDialogue cachedVisualNovelDialogue;
		[SerializeField]
		protected List<Image> characterSprites;
		[SerializeField] protected TextMeshProUGUI _textBox;

		public override void ConfigureViewer(DialogueController controller) {

			base.ConfigureViewer (controller);
			for (int i = 0; i < characterSprites.Count; i++) {
				characterSprites[i].enabled = false;
			}
		}

		public override void ConfigureDialogue(BaseDialogue dialogue) {

			cachedVisualNovelDialogue = (VisualNovelDialogue) dialogue;

			Command currentCommand;
			for (int i = 0; i < cachedVisualNovelDialogue.Command.Count; i++) {

				currentCommand = cachedVisualNovelDialogue.Command[i];
				characterSprites[(int) currentCommand.Side].enabled = currentCommand.CommandType.Equals (CommandType.SHOW);
				characterSprites[(int) currentCommand.Side].sprite = currentCommand.Character.Expressions[currentCommand.Expression].Sprite;
			}

			int characterIndex = (int) cachedVisualNovelDialogue.Side;
			if (!characterSprites[characterIndex].enabled) characterSprites[characterIndex].enabled = true;
			characterSprites[characterIndex].sprite = cachedVisualNovelDialogue.GetExpressionFromIndex ().Sprite;
			
			TextWriter.SetTextAndBox (dialogue.Text, _textBox);
		}
	}
}