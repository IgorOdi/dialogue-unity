using System;
using System.Collections;
using UnityEngine;

namespace Dialogues.View.Core {

    public class DialogueAnimator : MonoBehaviour {

        private Animator _animator;
        private int _speedMultiplierIndex;

        private const string OPEN_ANIMATION = "Open";
        private const string CLOSE_ANIMATION = "Close";
        private const string SPEED_MULTIPLIER_PARAM = "SpeedMultiplier";

        void Awake () {

            _animator = GetComponent<Animator> ();
            _speedMultiplierIndex = Animator.StringToHash (SPEED_MULTIPLIER_PARAM);
        }

        public virtual void OpenDialogueBox (Action opened) {

            _animator.Play (OPEN_ANIMATION);
            StartCoroutine (WaitFor (opened));
        }

        public virtual void CloseDialogueBox (Action closed) {

            _animator.Play (CLOSE_ANIMATION);
            StartCoroutine (WaitFor (closed));
        }

        private IEnumerator WaitFor (Action callback) {

            var animationClip = _animator.GetCurrentAnimatorClipInfo (0);
            yield return new WaitForSeconds (animationClip[0].clip.length / _animator.GetFloat (_speedMultiplierIndex));
            callback?.Invoke ();
        }
    }
}