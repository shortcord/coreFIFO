using System;
using System.Runtime.InteropServices;

namespace Pipes
{
    public partial class EasyPipes
    {
        [DllImport("libPipes")]
        extern static int makeSock(string file, int mode);

        [DllImport("libPipes")]
        extern static int deleteSock(string file);

        [DllImport("libPipes")]
        extern static IntPtr getLastErrorStr();

        [DllImport("libPipes")]
        extern static int getLastErrorInt();
    }
}