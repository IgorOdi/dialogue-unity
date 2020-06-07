using TMPro;

namespace Dialogues.View {

    public interface ITextWriter {

        void WriteText ();

        void AutoFillText ();

        bool IsFilling ();

        void SetTextAndBox (string text, TextMeshProUGUI textMeshProUGUI);
    }
}