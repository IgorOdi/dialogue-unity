using TMPro;

namespace Dialogues.View {

    public interface ITextWriter {

        void WriteText (string text);

        void AutoFillText ();

        bool IsFilling ();

        void SetTextBox (TextMeshProUGUI textMeshProUGUI);
    }
}