﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseImitation
{
    class Program
    {
        static void Main(string[] args)
        {
            new ByteMessagesFormatTests().Start();
            new Connection().Start(new ClientCommandsHandler());
        }
    }
}