using System.Collections.Generic;
using Dialogues.Model.Core;

namespace Dialogues.Model.Core {

	public interface IDialogueAsset {

		List<BaseDialogue> GetDialogues();
	}
}