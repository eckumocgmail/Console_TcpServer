using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

internal class Program
{
    internal static int Main(params string[] args)
    {
        TcpServerProgram.Run(args);
        return 0;
    }
     
}