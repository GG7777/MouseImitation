using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MouseImitation
{
    class MouseAPI
    {
        [DllImport("User32.dll")]
        static extern void mouse_event(MouseFlags dwFlags, int dx, int dy, int dwData, UIntPtr dwExtraInfo);

        [DllImport("User32.dll")]
        static extern bool GetCursorPos(out Point point);

        [DllImport("user32.dll")]
        static extern void SetCursorPos(int x, int y);

        struct Point
        {
            public int x;
            public int y;
        }

        [Flags]
        enum MouseFlags
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            Wheel = 0x0800,
            HWheel = 0x01000,
            Absolute = 0x8000
        };

        public static void MoveCursor(int dx, int dy)
        {
            if (GetCursorPos(out Point point))
                SetCursorPos(point.x + dx, point.y + dy);
        }

        public static void RightClick()
        {         
            mouse_event(MouseFlags.RightDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseFlags.RightUp, 0, 0, 0, UIntPtr.Zero);
        }

        public static void LeftClick()
        {
            mouse_event(MouseFlags.LeftDown, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MouseFlags.LeftUp, 0, 0, 0, UIntPtr.Zero);
        }

        public static void HoldLeftButton()
        {
            mouse_event(MouseFlags.LeftDown, 0, 0, 0, UIntPtr.Zero);
        }

        public static void UnholdLeftButton()
        {
            mouse_event(MouseFlags.LeftUp, 0, 0, 0, UIntPtr.Zero);
        }

        public static void HoldRightButton()
        {
            mouse_event(MouseFlags.RightDown, 0, 0, 0, UIntPtr.Zero);
        }

        public static void UnholdRightButton()
        {
            mouse_event(MouseFlags.RightUp, 0, 0, 0, UIntPtr.Zero);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">
        /// count of mouse ticks:
        /// > 0 = up
        /// else = down
        /// </param>
        public static void Wheel(int value)
        {
            mouse_event(MouseFlags.Wheel, 0, 0, 120 * value, UIntPtr.Zero);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">
        /// count of mouse ticks:
        /// > 0 = right
        /// else = left
        /// </param>
        public static void HorizontalWheel(int value)
        {
            mouse_event(MouseFlags.HWheel, 0, 0, 120 * value, UIntPtr.Zero);
        }
    }
}
