using System;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    class Client
    {
        public Client()
        {

            using (var ws = new WebSocket("ws://localhost:46495/Laputa"))
            {
                ws.OnMessage += (sender, e) =>
                    Console.WriteLine("Oliver says: " + e.Data);

                ws.Connect();
                ws.Send("BALUS");
                Console.ReadKey(true);
            }
        }
    }
}
