using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ICSharpCode.AvalonEdit;
using System.IO;
using Microsoft.Win32;
using System.Xml;

namespace AvalonTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TextEdit.TextArea.MouseUp += TextAreaMouseDown;
        }

        private void btnFile_Click(object sender, RoutedEventArgs e)
        {
            var openFile = new OpenFileDialog();
            if(openFile.ShowDialog() == true)
            {
                // open the file name and load into the AvalonEdit TextEdit
                TextEdit.Load(openFile.FileName);
            }
        }

        private void btnHighlight_Click(object sender, RoutedEventArgs e)
        {
            var openFile = new OpenFileDialog();
            if(openFile.ShowDialog() == true)
            {
                Stream InStream = default(Stream);
                InStream = File.OpenRead(openFile.FileName);
                XmlTextReader XTextReader = default(XmlTextReader);
                XTextReader = new XmlTextReader(InStream);
                TextEdit.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load(XTextReader, ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance);
            }
        }

        private void btnGoToLn_Click(object sender, RoutedEventArgs e)
        {
            // get the number from the text box
            // go to that line number in the textedit
            int lineNo;
            lineNo = int.Parse(tbLnNo.Text);
            if(lineNo > TextEdit.LineCount)
            {
                MessageBox.Show("File isn't that big!");
                lineNo = TextEdit.LineCount;
                tbLnNo.Text = lineNo.ToString();
            }

            // how to highlight that line?
            TextEdit.ScrollTo(lineNo, 0);
            tbPosition.Text = String.Format("{0} : {1}", lineNo, 0);
            TextEdit.TextArea.Caret.Line = lineNo;
            TextEdit.TextArea.Caret.Column = 0;

        }

        void TextAreaMouseDown(object sender, MouseButtonEventArgs e)
        {
            // MessageBox.Show("Click!");
            // update the position textbox
            int LineNo = TextEdit.TextArea.Caret.Line;
            int LineCol = TextEdit.TextArea.Caret.Column;
            tbPosition.Text = String.Format("{0} : {1}", LineNo, LineCol);
        }
    }
}
