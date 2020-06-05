using System.Collections;
using Dialogues.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogues.View {

    public class TextWriter : MonoBehaviour {

        public bool Filling { get; set; }

        [SerializeField] private Image _dialogueBox;
        [SerializeField] private Image _characterSprite;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _textBox;

        private Dialogue _currentDialogue;
        private int _currentDialogueIndex;
        private Coroutine _fillCoroutine;

        private const float textTime = 0.05f;

        public void ConfigureDialogue (Dialogue dialogue) {

            _currentDialogue = dialogue;
            //_nameText.text = dialogue.Character.Name;
            _characterSprite.sprite = dialogue.GetExpressionFromIndex ().Sprite;
            _textBox.text = dialogue.Text;
            _textBox.maxVisibleCharacters = 0;
        }

        public void WriteText () {

            if (_fillCoroutine != null) StopCoroutine (_fillCoroutine);
            _fillCoroutine = StartCoroutine (FillText ());
        }

        public void AutoFillCurrentDialogue () {

            _textBox.maxVisibleCharacters = _textBox.text.Length;
        }

        private IEnumerator FillText () {

            Filling = true;
            _textBox.maxVisibleCharacters = 0;
            while (_textBox.maxVisibleCharacters < _currentDialogue.Text.Length) {

                _textBox.maxVisibleCharacters += 1;
                yield return new WaitForSeconds (textTime);
            }

            Filling = false;
        }

        public void ClearDialogueBox () {

            _currentDialogue = null;
            _nameText.text = "";
            _characterSprite.sprite = null;
            _textBox.text = "";
        }
    }
}