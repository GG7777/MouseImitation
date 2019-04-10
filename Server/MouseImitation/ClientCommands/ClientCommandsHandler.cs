using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ByteMessagesFormat;

namespace MouseImitation
{
    class ClientCommandsHandler : IClientCommandsHandler
    {
        readonly Dictionary<Commands, MethodInfo> methodDecode;

        public ClientCommandsHandler()
        {
            var type = typeof(MouseAPI);
            methodDecode = new Dictionary<Commands, MethodInfo>
            {
                { Commands.MoveCursor, type.GetMethod("MoveCursor") },
                { Commands.HoldLeftButton, type.GetMethod("HoldLeftButton") },
                { Commands.UnholdLeftButton, type.GetMethod("UnholdLeftButton") },
                { Commands.HoldRightButton, type.GetMethod("HoldRightButton") },
                { Commands.UnholdRightButton, type.GetMethod("UnholdRightButton") },
                { Commands.Wheel, type.GetMethod("Wheel") },
                { Commands.HorizontalWheel, type.GetMethod("HorizontalWheel") },
                { Commands.LeftClick, type.GetMethod("LeftClick") },
                { Commands.RightClick, type.GetMethod("RightClick") }
            };
        }

        public void Handle(byte[] arr)
        {
            foreach (var command in GetCommands(arr))
            {
                var message = Message.Parse(command);
                methodDecode[(Commands)message.Command]
                    .Invoke(null, message.Parameters.Cast<object>().ToArray());
            }
        }

        readonly List<byte> tempData = new List<byte>();
        IEnumerable<byte[]> GetCommands(byte[] arr)
        {
            foreach (var b in arr)
            {
                tempData.Add(b);
                if (b == 0)
                {
                    yield return tempData.ToArray();
                    tempData.Clear();
                }
            }
        }
    }
}
