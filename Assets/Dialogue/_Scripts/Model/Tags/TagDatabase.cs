using System.Collections.Generic;
using UnityEngine;

namespace Dialogues.Model.Tags {

    public static class TagDatabase {

        public static List<Tag> TagList = new List<Tag> {

            new Tag ("<cs>", string.Empty, (textWriter) => Debug.Log ("Hello World"), null),
            new Tag ("<t>", "</t>", (textWriter) => Debug.Log ("Alterou o tempo"), (textWriter) => Debug.Log ("Tempo voltou ao normal")),

        };
    }
}