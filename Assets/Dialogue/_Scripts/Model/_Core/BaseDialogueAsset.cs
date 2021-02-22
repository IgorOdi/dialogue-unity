using System.Collections.Generic;
using UnityEngine;

namespace Dialogues.Model.Core {

	public abstract class BaseDialogueAsset : ScriptableObject, IDialogueAsset {

		//public List<T> Dialogues;
		public virtual List<BaseDialogue> GetDialogues() { throw new System.NotImplementedException (); }
	}
}