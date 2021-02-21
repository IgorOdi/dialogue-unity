using Dialogues.Model.Core;
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
            TextWriter.SetTextAndBox (basicDialogue.Text, _textBox);
        }
    }
}