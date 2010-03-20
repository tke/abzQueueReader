using System;
using abzQueueReader;
using System.Windows.Forms;
using System.Text;
using System.Xml.Linq;
using System.Net;
namespace QueueView
{
    static class Program
    {
        [STAThread]
        static void Main()
        {

            abzWeb.Server server = new abzWeb.Server();
            server.Start(IPAddress.Any, 22980);
            Console.WriteLine("Listening!");
            while (Console.ReadKey().Key != ConsoleKey.Escape) ;
            server.Stop();
            ////Application.Run(new Form1());
            //FileReader fr = new FileReader();
            //fr.loadFile("N:\\altbinz\\misc\\queue.abz");
            //fr.seek(253);
            //FileNode fn = new FileNode(fr, 11);
        }
    }
}