using System.Collections;
using Dialogues.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogues.View {

    public class BasicTextWriter : MonoBehaviour, ITextWriter {

        private bool _filling;
        private TextMeshProUGUI _textBox;
        private Coroutine _fillCoroutine;

        private const float textTime = 0.05f;

        public bool IsFilling () => _filling;
        
        public void WriteText (string text) {

            if (_fillCoroutine != null) StopCoroutine (_fillCoroutine);
            _fillCoroutine = StartCoroutine (FillText (text));
        }

        public void AutoFillText () {

            _textBox.maxVisibleCharacters = _textBox.textInfo.characterCount;
        }

        IEnumerator FillText (string text) {

            _filling = true;
            _textBox.maxVisibleCharacters = 0;
            while (_textBox.maxVisibleCharacters < _textBox.textInfo.characterCount) {

                _textBox.maxVisibleCharacters += 1;
                yield return new WaitForSeconds (textTime);
            }

            _filling = false;
        }

        public void SetTextBox (TextMeshProUGUI textBox) {

            _textBox = textBox;
        }

    }
}