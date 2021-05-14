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
            ThreadHelper.InvokeMainThread(this, () =>
            {
                tbOutput.AppendText(Environment.NewLine + output);
                tbOutput.SelectionStart = tbOutput.TextLength;
                tbOutput.ScrollToCaret();
            });
        }
    }
}
