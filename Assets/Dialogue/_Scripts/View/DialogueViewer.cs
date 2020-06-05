using Dialogues.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogues.View {

    public class DialogueViewer : MonoBehaviour {

        public ITextWriter TextWriter { get; set; }
        public DialogueAnimator DialogueAnimator { get; set; }

        [SerializeField] private Image _dialogueBox;
        [SerializeField] private Image _characterSprite;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _textBox;

        public void ConfigureViewer () {
            
            TextWriter = GetComponent<ITextWriter> ();
            DialogueAnimator = GetComponent<DialogueAnimator> ();
        }

        public virtual void ConfigureDialogue (Dialogue dialogue) {

            _nameText.text = dialogue.Character.Name;
            _characterSprite.sprite = dialogue.GetExpressionFromIndex ().Sprite;
            _textBox.text = dialogue.Text;
            _textBox.maxVisibleCharacters = 0;

            TextWriter.SetTextBox (_textBox);
        }
    }
}