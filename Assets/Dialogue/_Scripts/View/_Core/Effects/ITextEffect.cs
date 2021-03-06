using System.Collections.Generic;

namespace Dialogues.View.Effects {

    public interface ITextEffect {

        void SetParameters (List<float> parameters);

        void RegisterValues (int start, int end);

        void ClearValues ();
    }
}