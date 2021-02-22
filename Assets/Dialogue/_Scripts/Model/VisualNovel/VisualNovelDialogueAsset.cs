using System.Collections.Generic;
using System.Linq;
using Dialogues.Model.Core;
using UnityEngine;

namespace Dialogues.Model.VisualNovel {

	[CreateAssetMenu (menuName = "Dialogue/Dialogue Asset/Visual Novel Dialogue Asset", fileName = "New Visual Novel Dialogue Asset")]
	public class VisualNovelDialogueAsset : BaseDialogueAsset {

		public List<VisualNovelDialogue> Dialogues;

		public override List<BaseDialogue> GetDialogues() {

			return Dialogues.Cast<BaseDialogue> ().ToList ();
		}
	}
}