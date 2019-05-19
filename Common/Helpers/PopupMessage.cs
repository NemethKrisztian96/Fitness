namespace Fitness.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public static class PopupMessage
    {
        /// <summary>
        /// Displayes a message to the user with an OK button
        /// </summary>
        /// <param name="title">Title of the window</param>
        /// <param name="message">Message</param>
        public static void OkButtonPopupMessage(string title, string message)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons);
        }

        /// <summary>
        /// Displayes a message to the user with YES and No buttons returning a bool value corresponding to the selected button
        /// </summary>
        /// <param name="title">Title of the window</param>
        /// <param name="message">Message</param>
        /// <returns>True if YES is clicked or false if NO is clicked</returns>
        public static bool YesNoButtonPopupMessage(string title, string message)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if(result == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
