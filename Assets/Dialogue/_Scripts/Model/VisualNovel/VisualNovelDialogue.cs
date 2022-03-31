using System;
using System.Collections.Generic;
using Dialogues.Model.Basic;
using Dialogues.Model.Core;
using Dialogues.Model.Enum;

namespace Dialogues.Model.VisualNovel {

	[Serializable]
	public class VisualNovelDialogue : BasicDialogue {

		public List<Command> Command;
	}

	[Serializable]
	public class Command {

		public CommandType CommandType;
		public Character Character;
		public int Expression;
		public Sides Side;
	}

	public enum CommandType {

		SHOW,
		HIDE
	}
}