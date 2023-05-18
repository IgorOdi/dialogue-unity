using System.Linq;
using Dialogues.Controller.Core;
using Dialogues.Model.Core;
using UnityEngine;

namespace Dialogues.View.Core {

	public abstract class BaseDialogueViewer : MonoBehaviour {

		public DialogueController DialogueController { get; private set; }

		public BaseTextWriter TextWriter { get; private set; }
		public DialogueAnimator DialogueAnimator { get; private set; }
		public DialogueBoxLayoutData LayoutData { get; private set; }

		public bool IsShowingChoices { get; protected set; }

		public virtual void ConfigureViewer(DialogueController controller) {

			TextWriter = GetComponent<BaseTextWriter> ();
			DialogueAnimator = GetComponent<DialogueAnimator> ();
			LayoutData = GetComponent<DialogueBoxLayoutData> ();

			DialogueController = controller;
			TextWriter.DialogueController = controller;
		}

		public void CopyFromLayoutData(int index) {

			LayoutData.ConfigureSet (index);
		}

		public void CopyFromLayoutData(string name) {

			LayoutData.ConfigureSet (LayoutData.layoutDatas.Where (n => n.LayoutName.Equals (name)).FirstOrDefault ());
		}

		public virtual void ConfigureDialogue(BaseDialogue dialogue) { }
		public virtual void ShowChoicesButton() { }
	}
}