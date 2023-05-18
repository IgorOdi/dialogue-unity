using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dialogues.Controller.Core;
using Dialogues.Model;
using Dialogues.View.Effects;
using Dialogues.View.Tags;
using TMPro;
using UnityEngine;

namespace Dialogues.View.Core {

	public class BaseTextWriter : MonoBehaviour {

		public DialogueController DialogueController { get; set; }
		public virtual float CurrentWriteTime { get; set; }
		public virtual bool IsFilling { get; private set; }

		private string _currentText;
		private TextMeshProUGUI _textBox;
		private Coroutine _fillCoroutine;
		private List<TagInfo> _decodifiedTags;
		private List<ITextEffect> _textEffects = new List<ITextEffect> ();
		private int _lastTagIndex = -1;

		public virtual void WriteText(Action onFinishWritingText = null) {

			ResetFillingTime ();
			(_decodifiedTags, _currentText) = TagDecodifier.Decodify (_currentText);

			for (int i = 0; i < _textEffects.Count; i++)
				_textEffects[i].ClearValues ();

			if (_fillCoroutine != null) StopCoroutine (_fillCoroutine);
			_fillCoroutine = StartCoroutine (FillText (onFinishWritingText));
		}

		protected virtual IEnumerator FillText(Action onFinishWritingText) {

			IsFilling = true;
			_textBox.maxVisibleCharacters = 0;
			_textBox.text = _currentText;
			_lastTagIndex = -1;

			while (_textBox.maxVisibleCharacters < _textBox.textInfo.characterCount) {

				if (_decodifiedTags.Exists (i => i.TagPosition == _textBox.maxVisibleCharacters)) {

					_lastTagIndex += 1;
					_decodifiedTags[_lastTagIndex].OnTag?.Invoke (this,
						_decodifiedTags[_lastTagIndex].TagPosition,
						_decodifiedTags[_lastTagIndex].Parameters);
				}

				_textBox.maxVisibleCharacters += 1;
				yield return new WaitForSeconds (CurrentWriteTime);
			}

			onFinishWritingText?.Invoke ();
			IsFilling = false;
		}

		public virtual void AutoFillText() {

			_textBox.maxVisibleCharacters = _textBox.textInfo.characterCount;
			for (int i = _lastTagIndex + 1; i < _decodifiedTags.Count; i++) {

				_decodifiedTags[i].OnTag?.Invoke (this,
					_decodifiedTags[i].TagPosition,
					_decodifiedTags[i].Parameters);

				_lastTagIndex = i;
			}
		}

		public virtual void SetTextAndBox(string text, TextMeshProUGUI textBox) {

			_textBox = textBox;
			_currentText = text;
		}

		public void RegisterEffect(ITextEffect effect) {

			_textEffects.Add (effect);
		}

		public void UnregisterEffect(ITextEffect effect) {

			_textEffects.Remove (effect);
		}

		public ITextEffect GetEffect<T>() where T : TextEffect {

			return _textEffects.Where (t => t.GetType () == typeof (T)).FirstOrDefault ();
		}

		private void ResetFillingTime() {

			CurrentWriteTime = DialoguePreferences.Instance.BaseWriteTime;
		}
	}
}