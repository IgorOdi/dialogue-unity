using Dialogues.Model.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogues.View {

    public sealed class BasicDialogueViewer : BaseDialogueViewer {
        
        [SerializeField] private Image _dialogueBox;
        [SerializeField] private Image _characterSprite;
        [SerializeField] private TextMeshProUGUI _textBox;

        public override void ConfigureDialogue (Dialogue dialogue) {

            _characterSprite.sprite = dialogue.GetExpressionFromIndex ().Sprite;
            _textBox.maxVisibleCharacters = 0;
            
            TextWriter.SetTextAndBox (dialogue.Text, _textBox);
        }
    }
}