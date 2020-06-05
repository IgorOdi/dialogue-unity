using System.Collections.Generic;
using UnityEngine;

namespace Dialogues.Model {

    [CreateAssetMenu (menuName = "Dialogue/Character", fileName = "New Character")]
    public class Character : ScriptableObject {

        public string Name;
        public List<Expression> Expressions;
    }
}