using System;
using Dialogues.View;

namespace Dialogues.Model.Tags {

    public class Tag {

        public string OpenTag { get; set; }
        public string CloseTag { get; set; }
        public Action<ITextWriter> OnOpenTag { get; set; }
        public Action<ITextWriter> OnCloseTag { get; set; }

        public Tag (string openTag, string closeTag, Action<ITextWriter> onOpenTag, Action<ITextWriter> onCloseTag) {

            OpenTag = openTag;
            CloseTag = closeTag;
            OnOpenTag = onOpenTag;
            OnCloseTag = onCloseTag;
        }
    }
}