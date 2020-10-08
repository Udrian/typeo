using System;
using System.Windows.Forms;
using TypeEd.Helper;

namespace TypeEd.View.Forms
{
    public partial class Output : UserControl
    {
        public Output()
        {
            InitializeComponent();

            CMD.Output += CMD_Output;
        }

        private void CMD_Output(string output)
        {
            ThreadHelperClass.AppendText(this, tbOutput, Environment.NewLine + output);
        }
    }
}
