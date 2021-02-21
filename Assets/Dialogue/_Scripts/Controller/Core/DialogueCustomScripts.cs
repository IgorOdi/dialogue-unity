using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogues.Controller.Core {

    public class DialogueCustomScripts : MonoBehaviour {

        [SerializeField]
        private List<UnityEvent> Events;

        public void CallEvent(int index) {

            if (Events.Count < index) {

                Debug.LogWarning($"Event at index {index} not found");
                return;
            }
            Events[index].Invoke();
        }

        public void CallAllEvents() {

            for (int i = 0; i < Events.Count; i++) {

                CallEvent(i);
            }
        }
    }
}