using System;
using System.Collections.Generic;
using System.Linq;
using Dialogues.Model.Tags;

namespace Dialogues.View.Tags {

    public struct TagInfo {

        public readonly int TagPosition;
        public readonly float Parameter;
        public readonly Action<BaseTextWriter, float> OnTag;

        public TagInfo (int tagPosition, float param, Action<BaseTextWriter, float> onTag) {

            TagPosition = tagPosition;
            Parameter = param;
            OnTag = onTag;
        }
    }

    public static class TagDecodifier {

        public static List<Tag> TagList { get { return TagDatabase.TagList; } }

        public static (List<TagInfo> tagInfos, string newText) Decodify (string text) {

            List<TagInfo> tagInfos = new List<TagInfo> ();

            for (int i = 0; i < text.Length; i++) {

                if (text[i].Equals ('<')) {

                    int indexOfCloseTag = 0;
                    for (int j = i; j <= text.Length; j++) {
                        if (text[j].Equals ('>')) {
                            indexOfCloseTag = j + 1;
                            break;
                        }
                    }

                    string tagString = text.Substring (i, indexOfCloseTag - i);
                    float parameter = tagString.Contains ("=") ? DecodifyParameter (tagString) : float.NaN;
                    bool isOpening = !tagString.Contains ('/');

                    Tag tag = TagDatabase.GetTagWithString (tagString);

                    text = text.Remove (i, indexOfCloseTag - i);
                    tagInfos.Add (new TagInfo (i, parameter, isOpening ? tag.OnOpenTag : tag.OnCloseTag));
                }
            }

            return (tagInfos, text);
        }

        private static float DecodifyParameter (string param) {

            int equalsIndex = param.IndexOf ('=') + 1;
            int endTag = param.IndexOf ('>');
            param = param.Substring (equalsIndex, endTag - equalsIndex);
            float paramResult = float.NaN;
            if (!float.TryParse (param, out paramResult)) {
                throw new Exception ("Parameter format incorrect");
            }
            return paramResult;
        }
    }
}