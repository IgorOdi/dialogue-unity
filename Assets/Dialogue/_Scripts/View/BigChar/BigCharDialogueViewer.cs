using Dialogues.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogues.View {

    public class BigCharDialogueViewer : BaseDialogueViewer {

        [SerializeField] private Image _dialogueBox;
        [SerializeField] private Image _characterSprite;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _textBox;

        public override void ConfigureDialogue (Dialogue dialogue) {

            //_nameText.text = dialogue.Character.Name;
            _characterSprite.sprite = dialogue.GetExpressionFromIndex ().Sprite;

            _textBox.text = dialogue.Text.Insert (0, $"<b><color=yellow><size=120%>{dialogue.Character.Name}:</b></color></size=120%> ");
            _textBox.maxVisibleCharacters = 0;

            TextWriter.SetTextBox (_textBox);
        }
    }
}