using AxShockwaveFlashObjects;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using OpenFK.OFK.Common;

namespace OpenFK
{
    class FlashRightClick : AxShockwaveFlash
    {
        protected override void WndProc(ref Message m)
        {
            if(m.Msg == 0x0204 && !ModifierKeys.HasFlag(Keys.Control)) // If it's a right click and control is not held down. If control is held down, the context menu will show.
            {
                this.SetVariable("msg", $@"<rightclick x=""{Cursor.Position.X}"" y=""{Cursor.Position.Y}"" />"); // Sends right click command to flash.
                LogManager.LogGeneral("Right click");
                m.Result = IntPtr.Zero; // Blocks context menu from showing on versions that aren't the fetched OCX... No clue why it doesn't work.
                return;
            }
            base.WndProc(ref m);
        }
    }
}
