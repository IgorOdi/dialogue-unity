using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dialogues.View.Effect;
using UnityEngine;

namespace Dialogues.Model.Tags {

    public static class TagDatabase {

        public static List<Tag> TagList {
            get {
                return new List<Tag> {

                    new Tag ("cs",
                        (textWriter, position, parameters) => Debug.Log ("Hello World"),
                        null),

                    new Tag ("t",
                        (textWriter, position, parameters) => textWriter.CurrentWriteTime = parameters[0],
                        (textWriter, position, parameters) => textWriter.CurrentWriteTime = DialoguePreferences.Instance.BaseWriteTime),

                    new Tag ("shake",
                        (textWriter, position, parameters) => {
                            var effect = textWriter.GetEffect<JitterEffect> ();
                            effect.RegisterValues (position, 999);
                            effect.SetParameters (parameters);
                        }, (textWriter, position, parameters) => {
                            var effect = textWriter.GetEffect<JitterEffect> ();
                            effect.RegisterValues (999, position);
                        }),
                    new Tag ("wave",
                        (textWriter, position, parameters) => {
                            var effect = textWriter.GetEffect<WarpEffect> ();
                            effect.RegisterValues (position, 999);
                            effect.SetParameters (parameters);
                        }, (textWriter, position, parameters) => {
                            var effect = textWriter.GetEffect<WarpEffect> ();
                            effect.RegisterValues (999, position);
                        }),
                };
            }
        }

        public static Tag GetTagWithString (string tagComparison) {

            return TagList.Where (t => tagComparison.Contains (t.TagString)).FirstOrDefault ();
        }
    }
}