using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteMessagesFormat
{
    public class Message
    {
        public byte Command;
        public int[] Parameters;

        public Message(byte command, params int[] parameters)
        {
            Command = command;
            Parameters = parameters;
        }

        const byte EndMessageMark = 0;
        const byte ParametersSeparator = 32;

        public byte[] Build()
        {
            return Build(this);
        }

        public static byte[] Build(Message message)
        {
            var result = new List<byte>();
            result.Add(message.Command);
            foreach (var p in message.Parameters)
            {
                result.Add(ParametersSeparator);
                result.AddRange(Encoding.UTF8.GetBytes(p.ToString()));
            }
            result.Add(EndMessageMark);
            return result.ToArray();
        }

        public static Message Parse(byte[] message)
        {
            if (message.Length < 2) throw new Exception();
            var command = message[0];
            var parameters = new List<int>();
            var digits = new List<byte>();
            for (var i = 2; i < message.Length; i++)
            {
                if (message[i] != ParametersSeparator && message[i] != EndMessageMark)
                    digits.Add(message[i]);
                else
                {
                    parameters.Add(int.Parse(Encoding.UTF8.GetString(digits.ToArray())));
                    digits.Clear();
                }
            }
            return new Message(command, parameters.ToArray());
        }
    }
}
