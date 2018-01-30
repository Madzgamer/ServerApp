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
            // CreateWebSocket()
            // CheckConnection(), server returnib "Here" et kindlaks teha kas serveriga on ühendus
            // RequestId, saab id
            // loop
            // GetNewMessages, messagesObject
            //
            //oliveri ip  84.50.187.110
            //port 46495

            Run(); //tegelt peaks seda väljaspoolt tegema
        }

        public void Run()
        {

            using (var ws = CreateWebSocket("localhost", 46495))
            {
                ws.OnMessage += (sender, e) => OnMessageReaction(sender, e);

                ws.Connect();
                ws.Send("checkConnection");


                ws.Send("BALUS");
                Console.ReadKey(true);
            }

        }

        private WebSocket CreateWebSocket(string ip, int port)
        {
            return new WebSocket("ws://" + ip + ":" + port + "/Chat");
        }

        private void OnMessageReaction(Object sender, MessageEventArgs e)
        {
            Console.WriteLine("Pede *Randal says: " + e.Data);
            if (e.GetType() == Type.GetType("String"))
            {

            }
        }
    }
}
