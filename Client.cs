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

<<<<<<< HEAD

            using (var ws = new WebSocket("ws://localhost:46495/Chat"))
=======
            using (var ws = new WebSocket("ws://localhost:46495/Laputa"))
>>>>>>> 709515df4dbd562c2c6e46a32e7d4d19a8db7aca
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
