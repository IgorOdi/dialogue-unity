using System;
using System.Collections.Generic;
using Dialogues.View;

namespace Dialogues.Model.Tags {

    public class Tag {

        public readonly string TagString;
        public readonly Action<BaseTextWriter, int, List<float>> OnOpenTag;
        public readonly Action<BaseTextWriter, int, List<float>> OnCloseTag;

        public Tag (string tagString, Action<BaseTextWriter, int, List<float>> onOpenTag, Action<BaseTextWriter, int, List<float>> onCloseTag) {

            TagString = tagString;
            OnOpenTag = onOpenTag;
            OnCloseTag = onCloseTag;
        }
    }
}