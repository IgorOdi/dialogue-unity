using Dialogues.Model.Basic;
using Dialogues.Model.Core;
using Dialogues.View.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogues.View {

    public class VisualNovelDialogueViewer : BaseDialogueViewer {

        [SerializeField] private Image _dialogueBox;
        [SerializeField] private Image _characterSprite;
        [SerializeField] private TextMeshProUGUI _textBox;

        public override void ConfigureDialogue (BaseDialogue dialogue) {

            BasicDialogue basicDialogue = (BasicDialogue)dialogue;

            _characterSprite.sprite = basicDialogue.GetExpressionFromIndex ().Sprite;

            string text = dialogue.Text.Insert (0, $"<b><color=yellow><size=120%>{basicDialogue.Character.Name}:</b></color></size=120%> ");
            TextWriter.SetTextAndBox (text, _textBox);
        }
    }
}