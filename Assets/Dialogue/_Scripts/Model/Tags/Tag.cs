using System;
using Dialogues.View;

namespace Dialogues.Model.Tags {

    public class Tag {

        public readonly string TagString;
        public readonly float Parameter;
        public readonly Action<BaseTextWriter, float> OnOpenTag;
        public readonly Action<BaseTextWriter, float> OnCloseTag;

        public Tag (string tagString, Action<BaseTextWriter, float> onOpenTag, Action<BaseTextWriter, float> onCloseTag) {

            TagString = tagString;
            OnOpenTag = onOpenTag;
            OnCloseTag = onCloseTag;
        }
    }
}