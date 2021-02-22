using System.Collections;
using System.Collections.Generic;
using Dialogues.View.Core;
using TMPro;
using UnityEngine;

namespace Dialogues.View.Effects {

    public abstract class TextEffect : MonoBehaviour, ITextEffect {

        protected List<(int, int)> _intervalValues = new List<(int, int)>();
        protected TMP_Text _textComponent;
        protected bool _hasTextChanged;
        protected BaseTextWriter _textWriter;

        protected Coroutine _effectCoroutine;

        void Awake() {

            _textComponent = GetComponent<TMP_Text>();
            _textWriter = GetComponentInParent<BaseTextWriter>();
            _textWriter.RegisterEffect(this);
        }

        protected virtual void OnEnable() {

            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(OnTextChanged);
            _effectCoroutine = StartCoroutine(Effect());
            _textWriter.UnregisterEffect(this);
        }

        protected virtual void OnDisable() {

            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(OnTextChanged);
            if (_effectCoroutine != null) StopCoroutine(_effectCoroutine);
        }

        void OnTextChanged(Object obj) {

            if (obj == _textComponent)
                _hasTextChanged = true;
        }

        protected virtual IEnumerator Effect() { yield return null; }

        public virtual void SetParameters(List<float> parameters) { throw new System.NotImplementedException(); }
        public virtual void RegisterValues(int start, int end) { throw new System.NotImplementedException(); }
        
        public virtual void ClearValues() {

            _intervalValues.Clear();
        }
    }
}