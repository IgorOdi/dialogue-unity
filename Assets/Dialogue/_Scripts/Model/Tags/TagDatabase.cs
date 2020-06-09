using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dialogues.Model.Tags {

    public static class TagDatabase {

        public static List<Tag> TagList {
            get {
                return new List<Tag> {
                    new Tag ("cs", (textWriter, parameter) => Debug.Log ("Hello World"), null),
                    new Tag ("t",
                        (textWriter, parameter) => textWriter.CurrentWriteTime = parameter,
                        (textWriter, parameter) => textWriter.CurrentWriteTime = DialoguePreferences.Instance.BaseWriteTime),
                };
            }
        }

        public static Tag GetTagWithString (string tagComparison) {

            return TagList.Where (t => tagComparison.Contains (t.TagString)).FirstOrDefault ();
        }
    }
}