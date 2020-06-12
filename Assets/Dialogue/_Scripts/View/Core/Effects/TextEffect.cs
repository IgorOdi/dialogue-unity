using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Dialogues.View.Effect {

    public abstract class TextEffect : MonoBehaviour {

        protected List < (int, int) > _intervalValues = new List < (int, int) > ();
        protected TMP_Text _textComponent;
        protected bool _hasTextChanged;
        protected BaseTextWriter _textWriter;

        protected Coroutine _effectCoroutine;

        void Awake () {
            
            _textComponent = GetComponent<TMP_Text> ();
            _textWriter = GetComponentInParent<BaseTextWriter> ();
        }

        protected virtual void OnEnable () {

            TMPro_EventManager.TEXT_CHANGED_EVENT.Add (OnTextChanged);
            _effectCoroutine = StartCoroutine (Effect ());
        }

        protected virtual void OnDisable () {

            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove (OnTextChanged);
            if (_effectCoroutine != null) StopCoroutine (_effectCoroutine);
        }

        void OnTextChanged (Object obj) {

            if (obj == _textComponent)
                _hasTextChanged = true;
        }

        protected virtual IEnumerator Effect () { yield return null; }
    }
}