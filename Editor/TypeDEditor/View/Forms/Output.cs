using System;
using System.Windows.Forms;
using TypeD.Helpers;
using TypeDEditor.Helper;

namespace TypeDEditor.View.Forms
{
    public partial class Output : UserControl
    {
        public Output()
        {
            InitializeComponent();

            CMD.Output += CMD_Output;
#if DEBUG
            CMD.Output += (cmd) => { Console.WriteLine(cmd); };
#endif
        }

        private void CMD_Output(string output)
        {
            ThreadHelperClass.AppendText(this, tbOutput, Environment.NewLine + output);
        }

        private void tbOutput_TextChanged(object sender, EventArgs e)
        {
            //tbOutput.Select(tbOutput.TextLength + 1, 0);
            //tbOutput.SelectedText = tbOutput.Text.Substring(tbOutput.Text.Length - 2, 1);
        }
    }
}
