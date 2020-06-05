using System;
using System.Linq;

namespace Dialogues.Model {

    [Serializable]
    public class Dialogue {

        public Character Character;
        public int ExpressionIndex;
        public string Text;

        public Expression GetExpressionFromIndex () {

            return Character.Expressions[ExpressionIndex];
        }

        public Expression GetExpressionFromName (string expressionName) {

            return Character.Expressions.Where (e => e.Name.Equals (expressionName)).FirstOrDefault ();
        }
    }
}