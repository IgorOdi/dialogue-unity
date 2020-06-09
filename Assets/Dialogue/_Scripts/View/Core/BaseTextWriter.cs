using System.Collections;
using System.Collections.Generic;
using Dialogues.View.Tags;
using TMPro;
using UnityEngine;

namespace Dialogues.View {

    public class BaseTextWriter : MonoBehaviour {

        public virtual bool IsFilling { get; private set; }
        public virtual float CurrentWriteTime { get; set; }

        private TextMeshProUGUI _textBox;
        private Coroutine _fillCoroutine;
        private string _currentText;
        private List<TagInfo> _decodifiedTags;

        private void Awake () => CurrentWriteTime = Model.DialoguePreferences.Instance.BaseWriteTime;

        public virtual void WriteText () {

            (_decodifiedTags, _currentText) = TagDecodifier.Decodify (_currentText);

            if (_fillCoroutine != null) StopCoroutine (_fillCoroutine);
            _fillCoroutine = StartCoroutine (FillText ());
        }

        protected virtual IEnumerator FillText () {

            IsFilling = true;
            _textBox.maxVisibleCharacters = 0;
            _textBox.text = _currentText;
            int tagIndex = 0;
            while (_textBox.maxVisibleCharacters < _textBox.textInfo.characterCount) {

                if (_decodifiedTags.Exists (i => i.TagPosition == _textBox.maxVisibleCharacters)) {

                    _decodifiedTags[tagIndex].OnTag?.Invoke (this, _decodifiedTags[tagIndex].Parameter);
                    tagIndex++;
                }

                _textBox.maxVisibleCharacters += 1;
                yield return new WaitForSeconds (CurrentWriteTime);
            }

            IsFilling = false;
        }

        public virtual void AutoFillText () {

            _textBox.maxVisibleCharacters = _textBox.textInfo.characterCount;
        }

        public virtual void SetTextAndBox (string text, TextMeshProUGUI textBox) {

            _textBox = textBox;
            _currentText = text;
        }
    }
}