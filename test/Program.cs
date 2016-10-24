using Pipes;
using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace ConsoleApplication
{
    public class Program
    {
        public static Thread readThread;
        public static Thread writeThread;

        static bool run;

        static EasyPipes pipe;

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(Directory.GetCurrentDirectory());
            
            pipe = new EasyPipes("/tmp/test.sock");

            Console.WriteLine("Created Pipe @ /tmp/test.sock");
            Console.ReadKey();

            writeThread = new Thread(WriteContext);
            writeThread.IsBackground = true;
            readThread = new Thread(ReadContext);
            readThread.IsBackground = true;

            run = true;

            writeThread.Start();
            readThread.Start();

            Console.WriteLine("Started reading sock");
            Console.WriteLine($"{writeThread.IsBackground} {writeThread.IsAlive} | {readThread.IsBackground} {readThread.IsAlive}");
            Console.ReadKey();

            run = false;

            writeThread.Join();
            readThread.Join();

            pipe.Dispose();
            Console.WriteLine("Removed Pipe @ /tmp/test.sock");
            Console.ReadKey();
        }

        class JsonContext 
        {
            [JsonPropertyAttribute("payload")]
            public string Payload { get; set; }
        }

        public static void WriteContext()
        {
            using (StreamWriter writer = new StreamWriter(pipe.OpenPipeWrite()))
            {
                writer.AutoFlush = true;
                JsonContext payload = new JsonContext();
                payload.Payload = "I'm nolonger retarded";

                while (run)
                {
                    writer.WriteLine(JsonConvert.SerializeObject(payload));
                    writer.Flush();
                    Thread.Sleep(1000);
                }
            }
        }

        public static void ReadContext()
        {
            using (StreamReader reader = new StreamReader(pipe.OpenPipeRead()))
            {
                string line;
                while ((line = reader.ReadLine()) != null && run)
                {
                    //Console.WriteLine(JsonConvert.DeserializeObject<JsonContext>(line).Payload);
                    Console.WriteLine(line);
                }
            }
        }
    }
}
