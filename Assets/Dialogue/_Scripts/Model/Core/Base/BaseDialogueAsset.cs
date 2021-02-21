using System.Collections.Generic;
using UnityEngine;

namespace Dialogues.Model.Core {

    public class BaseDialogueAsset<T> : ScriptableObject where T : BaseDialogue {

        public List<T> Dialogues;
    }
}