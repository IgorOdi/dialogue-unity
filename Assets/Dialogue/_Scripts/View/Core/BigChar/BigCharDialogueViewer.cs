using Dialogues.Model.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogues.View {

    public class BigCharDialogueViewer : BaseDialogueViewer {

        [SerializeField] private Image _dialogueBox;
        [SerializeField] private Image _characterSprite;
        [SerializeField] private TextMeshProUGUI _textBox;

        public override void ConfigureDialogue (Dialogue dialogue) {

            _characterSprite.sprite = dialogue.GetExpressionFromIndex ().Sprite;
            _textBox.maxVisibleCharacters = 0;

            string text = dialogue.Text.Insert (0, $"<b><color=yellow><size=120%>{dialogue.Character.Name}:</b></color></size=120%> ");
            TextWriter.SetTextAndBox (text, _textBox);
        }
    }
}