using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Pipes
{
    public partial class EasyPipes : IDisposable {
        /// <summary>
        /// Gets frendly error string based on errorno.
        /// </summary>
        public static string GetLastErrorStr() => PtrToString(getLastErrorStr());

        /// <summary>
        /// Get last error from C.<para/>
        /// Same as errorno.
        /// </summary>
        public static PipesError GetLastError() => (PipesError)getLastErrorInt();

        private static string PtrToString(IntPtr p) => Marshal.PtrToStringAnsi(p);

        private static int StringToOctal(string value) => Convert.ToInt32(value, 8);

        private static Exception HandleError(PipesError e, string errorStr)
        {
            switch (e)
            {
                case PipesError.EACCES:
                    return new UnauthorizedAccessException($"[{e}] {errorStr}");
                default:
                    return new IOException($"[{e}] {errorStr}");
            }
        }

        /// <summary>
        /// Create a FIFO file for piping.
        /// </summary>
        /// <param name="file">Path to store the FIFO file.</param>
        /// <param name="mode">umask in string; Defaults to 0660 (-rw-rw----)</param>
        public static void CreatePipe(string file, string mode = "0660")
        {
            int m = StringToOctal(mode);
            
            if (makeSock(file, m) < 0)
            {
                throw new IOException($"Failed to create pipe file {file} with mask {m}. See inner exception", 
                    HandleError(GetLastError(), GetLastErrorStr()));
            }
        }

        /// <summary>
        /// Remove a FIFO file.
        /// </summary>
        /// <param name="file">Path to the FIFO file.</param>
        public static void RemovePipe(string file)
        {
            if (deleteSock(file) < 0)
            {
                throw new IOException($"Failed to remove pipe file {file}. See inner exception", 
                    HandleError(GetLastError(), GetLastErrorStr()));
            }
        }

        public string Path { get; private set; }
        public string Mode { get; private set; }
        object lockObj;

        public EasyPipes(string path, string mode = "0660")
        {
            Path = path;
            Mode = mode;
            lockObj = new Object();
        }
        

        public FileStream OpenPipeRead() => OpenPipe(FileAccess.Read);
        public FileStream OpenPipeWrite() => OpenPipe(FileAccess.Write);
        public FileStream OpenPipeReadWrite() => OpenPipe(FileAccess.ReadWrite);

        public FileStream OpenPipe(FileAccess access, FileShare share = FileShare.ReadWrite)
        {
            lock (lockObj)
                if (!File.Exists(Path)) //dont recreate the FIFO file
                    CreatePipe(Path, Mode);

            return File.Open(Path, FileMode.Open, access, share);
        }

        public void ClosePipe()
        {
            lock (lockObj)
                if (File.Exists(Path)) //dont cause needless exceptions
                    RemovePipe(Path);
        }

        public void Dispose() => ClosePipe();
    }
}