using Dialogues.Model.Core;
using Dialogues.Model.VisualNovel;

namespace Dialogues.View.Core {

	public class VisualNovelDialogueViewer : BasicDialogueViewer {

		private VisualNovelDialogue cachedVisualNovelDialogue;

		public override void ConfigureDialogue(BaseDialogue dialogue) {

			cachedVisualNovelDialogue = (VisualNovelDialogue) dialogue;

			_characterSprite.sprite = cachedVisualNovelDialogue.GetExpressionFromIndex ().Sprite;
			CopyFromLayoutData ((int) cachedVisualNovelDialogue.Side);
			TextWriter.SetTextAndBox (dialogue.Text, _textBox);
		}
	}
}