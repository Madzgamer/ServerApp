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
            // requestId, saab id
            // getNewMessages, 
            //
            //oliveri ip  84.50.187.110
            using (var ws = new WebSocket("ws://localhost:46495/Chat"))
            {
                ws.OnMessage += (sender, e) =>
                    Console.WriteLine("Pede *Randal says: " + e.Data);

                ws.Connect();
                ws.Send("BALUS");
                Console.ReadKey(true);
            }
        }
    }
}
