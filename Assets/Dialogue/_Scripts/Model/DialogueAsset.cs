using System.Collections.Generic;
using UnityEngine;

namespace Dialogues.Model {

    [CreateAssetMenu (menuName = "Dialogue/DialogueAsset", fileName = "New Dialogue Asset")]
    public class DialogueAsset : ScriptableObject {

        public List<Dialogue> Dialogues;
    }
}