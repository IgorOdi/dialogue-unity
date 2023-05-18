using System;
using System.Collections.Generic;
using System.Linq;
using Dialogues.Model.Core;
using Dialogues.Model.Enum;

namespace Dialogues.Model.Basic {

	[Serializable]
	public class BasicDialogue : BaseDialogue {

		public Character Character;
		public int ExpressionIndex;
		public Sides Side;
		public List<Choice> Choices = new List<Choice> ();

		public Expression GetExpressionFromIndex() {

			return Character.Expressions[ExpressionIndex];
		}

		public Expression GetExpressionFromName(string expressionName) {

			return Character.Expressions.Where (e => e.Name.Equals (expressionName)).FirstOrDefault ();
		}
	}

	[Serializable]
	public class Choice {

		public string Text;
		public BaseDialogueAsset NextDialogueAsset;

		public string VariableName;
		public string VariableValue;
	}
}