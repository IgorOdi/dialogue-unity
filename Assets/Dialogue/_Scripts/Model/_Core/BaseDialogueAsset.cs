using System.Collections.Generic;
using UnityEngine;

namespace Dialogues.Model.Core {

	public abstract class BaseDialogueAsset : ScriptableObject {

		public virtual List<BaseDialogue> GetDialogues() { throw new System.NotImplementedException (); }
	}
}