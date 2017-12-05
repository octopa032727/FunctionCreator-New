using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;


namespace FunctionCreator_New
{
    public class CompletionData : ICompletionData
    {
        public CompletionData(string text)
        {
            Text = text;
        }

        public object Content { get; set; }
        public object Description { get; set; }
        public ImageSource Image { get; set; }
        public double Priority { get; set; }
        public string Text { get; set; }

        public void Complete(TextArea textArea,ISegment segment,EventArgs insertionRequestEventArgs)
        {
            textArea.Document.Replace(segment, Text);
        }
    }
}
