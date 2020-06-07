using System;
using System.Collections.Generic;
using Dialogues.Model.Tags;

namespace Dialogues.View.Tags {

    public struct TagInfo {

        public readonly int TagStart;
        public readonly int TagEnd;
        public readonly Action<ITextWriter> OnTagStart;
        public readonly Action<ITextWriter> OnTagEnd;

        public TagInfo (int tagStart, int tagEnd, Action<ITextWriter> onTagStart, Action<ITextWriter> onTagEnd) {

            TagStart = tagStart;
            TagEnd = tagEnd;
            OnTagStart = onTagStart;
            OnTagEnd = onTagEnd;
        }
    }

    public class TagDecodifier {

        public List<Tag> TagList { get { return TagDatabase.TagList; } }

        public (List<TagInfo> tagInfos, string newText) Decodify (string text) {

            List<TagInfo> tagInfos = new List<TagInfo> ();
            for (int i = 0; i < TagList.Count; i++) {

                bool hasTag = false;
                (int openSub, int closeSub) tagPositions = (-1, -1);
                if (TagList[i].OpenTag != string.Empty && text.Contains (TagList[i].OpenTag)) {

                    hasTag = true;
                    tagPositions.openSub = text.IndexOf (TagList[i].OpenTag);
                    text = text.Remove (tagPositions.openSub, TagList[i].OpenTag.Length);
                }

                if (TagList[i].CloseTag != string.Empty && text.Contains (TagList[i].CloseTag)) {

                    hasTag = true;
                    tagPositions.closeSub = text.IndexOf (TagList[i].CloseTag);
                    text = text.Remove (tagPositions.closeSub, TagList[i].CloseTag.Length);
                }

                if (hasTag)
                    tagInfos.Add (new TagInfo (tagPositions.openSub, tagPositions.closeSub, TagList[i].OnOpenTag, TagList[i].OnCloseTag));

            }
            return (tagInfos, text);
        }
    }
}