using System.Collections;
using System.Collections.Generic;
using Dialogues.View.Tags;
using TMPro;
using UnityEngine;

namespace Dialogues.View {

    public class BasicTextWriter : MonoBehaviour, ITextWriter {

        private bool _filling;
        private TextMeshProUGUI _textBox;
        private Coroutine _fillCoroutine;
        private string _currentText;
        private List<TagInfo> _decodifiedTags;

        private const float textTime = 0.05f;

        public bool IsFilling () => _filling;

        public void WriteText () {

            TagDecodifier tagDecodifier = new TagDecodifier ();
            (_decodifiedTags, _currentText) = tagDecodifier.Decodify (_currentText);

            if (_fillCoroutine != null) StopCoroutine (_fillCoroutine);
            _fillCoroutine = StartCoroutine (FillText ());
        }

        private IEnumerator FillText () {

            _filling = true;
            _textBox.maxVisibleCharacters = 0;
            _textBox.text = _currentText;
            int tagIndex = 0;
            while (_textBox.maxVisibleCharacters < _textBox.textInfo.characterCount) {

                if (_decodifiedTags.Exists (i => i.TagStart == _textBox.maxVisibleCharacters)) {

                    _decodifiedTags[tagIndex].OnTagStart?.Invoke (this);
                }
                if (_decodifiedTags.Exists (i => i.TagEnd == _textBox.maxVisibleCharacters)) {

                    _decodifiedTags[tagIndex].OnTagEnd?.Invoke (this);
                }

                _textBox.maxVisibleCharacters += 1;
                yield return new WaitForSeconds (textTime);
            }

            _filling = false;
        }

        public void AutoFillText () {

            _textBox.maxVisibleCharacters = _textBox.textInfo.characterCount;
        }

        public void SetTextAndBox (string text, TextMeshProUGUI textBox) {

            _textBox = textBox;
            _currentText = text;
        }

    }
}