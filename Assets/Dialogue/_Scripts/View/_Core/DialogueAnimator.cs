using System;
using System.Collections;
using UnityEngine;

namespace Dialogues.View.Core {

    public class DialogueAnimator : MonoBehaviour {

        private Animator animator;
        private int speedMultiplierIndex;

        private const string OPEN_ANIMATION = "Open";
        private const string CLOSE_ANIMATION = "Close";
        private const string SPEED_MULTIPLIER_PARAM = "SpeedMultiplier";

        void Awake () {

            animator = GetComponent<Animator> ();
            speedMultiplierIndex = Animator.StringToHash (SPEED_MULTIPLIER_PARAM);
        }

        public virtual void OpenDialogueBox (Action opened) {

            animator.Play (OPEN_ANIMATION);
            StartCoroutine (WaitFor (opened));
        }

        public virtual void CloseDialogueBox (Action closed) {

            animator.Play (CLOSE_ANIMATION);
            StartCoroutine (WaitFor (closed));
        }

        private IEnumerator WaitFor (Action callback) {

            var animationClip = animator.GetCurrentAnimatorClipInfo (0);
            yield return new WaitForSeconds (animationClip[0].clip.length / animator.GetFloat (speedMultiplierIndex));
            callback?.Invoke ();
        }
    }
}