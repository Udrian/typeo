using System;
using System.Windows.Forms;

namespace TypeDEditor.Helper
{
    public static class ThreadHelper
    {
        delegate void InvokeMainThreadCallback(Control uc, Action action);
        public static void InvokeMainThread(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                var callback = new InvokeMainThreadCallback(InvokeMainThread);
                control.Invoke(callback, new object[] { control, action });
            }
            else
            {
                action();
            }
        }




        delegate void SetTextCallback(UserControl f, RichTextBox ctrl, string text);
        /// <summary>
        /// Set text property of various controls
        /// </summary>
        /// <param name="form">The calling form</param>
        /// <param name="ctrl"></param>
        /// <param name="text"></param>
        public static void SetText(UserControl form, Control ctrl, string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                form.Invoke(d, new object[] { form, ctrl, text });
            }
            else
            {
                ctrl.Text = text;
            }
        }

        public static void AppendText(UserControl form, RichTextBox ctrl, string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (ctrl.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(AppendText);
                form.Invoke(d, new object[] { form, ctrl, text });
            }
            else
            {
                ctrl.AppendText(text);
            }
        }
    }
}
