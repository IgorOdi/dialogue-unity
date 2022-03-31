using System.Linq;
using Dialogues.Model.Core;
using Dialogues.Model.Enum;

namespace Dialogues.Model.Basic {

	[System.Serializable]
	public class BasicDialogue : BaseDialogue {

		public Character Character;
		public int ExpressionIndex;
		public Sides Side;

		public Expression GetExpressionFromIndex() {

			return Character.Expressions[ExpressionIndex];
		}


		public Expression GetExpressionFromName(string expressionName) {

			return Character.Expressions.Where (e => e.Name.Equals (expressionName)).FirstOrDefault ();
		}
	}
}