using System;
using System.Windows.Forms;

namespace DCEStudyTools.BeamTypeCorrect
{
    public class WindowHandle : IWin32Window
    {
        IntPtr _hwnd;

        public WindowHandle(IntPtr h)
        {
            System.Diagnostics.Debug.Assert(IntPtr.Zero != h,
              "expected non-null window handle");

            _hwnd = h;
        }

        public IntPtr Handle
        {
            get
            {
                return _hwnd;
            }
        }
    }
}
