using System.Collections.Generic;
using System.Linq;
using Dialogues.Model.Core;
using UnityEngine;

namespace Dialogues.Model.Basic {

	[CreateAssetMenu (menuName = "Dialogue/Dialogue Asset/Basic Dialogue Asset", fileName = "New Basic Dialogue Asset")]
	public class BasicDialogueAsset : BaseDialogueAsset {

		public List<BasicDialogue> Dialogues;

		public override List<BaseDialogue> GetDialogues() {

			return Dialogues.Cast<BaseDialogue> ().ToList ();
		}
	}
}