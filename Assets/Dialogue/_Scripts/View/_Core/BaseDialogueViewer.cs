using System.Linq;
using Dialogues.Controller.Core;
using Dialogues.Model.Core;
using UnityEngine;

namespace Dialogues.View.Core {

	public abstract class BaseDialogueViewer : MonoBehaviour {

		public BaseTextWriter TextWriter { get; private set; }
		public DialogueAnimator DialogueAnimator { get; private set; }
		public DialogueBoxLayoutData LayoutData { get; private set; }
		public DialogueController Context { get; private set; }

		public virtual void ConfigureViewer() {

			TextWriter = GetComponent<BaseTextWriter> ();
			DialogueAnimator = GetComponent<DialogueAnimator> ();
			LayoutData = GetComponent<DialogueBoxLayoutData> ();
			TextWriter.Context = Context;
		}

		public void CopyFromLayoutData(int index) {

			LayoutData.ConfigureSet (index);
		}

		public void CopyFromLayoutData(string name) {

			LayoutData.ConfigureSet (LayoutData.layoutDatas.Where (n => n.LayoutName.Equals (name)).FirstOrDefault ());
		}

		public virtual void ConfigureDialogue(BaseDialogue dialogue) { }
	}
}