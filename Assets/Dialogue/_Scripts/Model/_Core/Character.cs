using System.Collections.Generic;
using UnityEngine;

namespace Dialogues.Model.Core {

	[CreateAssetMenu (menuName = "Dialogue/Character", fileName = "New Character")]
	public class Character : ScriptableObject {

		public string Name;
		public List<Expression> Expressions;
	}
}