using System;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServerApp
{
    class Client
    {
        public bool connectionConfirmed { get; private set; }

        public Client()
        {
            connectionConfirmed = false;
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

                Packet checkConn = new Packet(ActionCode.CONFCONN, "");

                Console.WriteLine("Sending a message");
                ws.Send(JsonConvert.SerializeObject(checkConn));



                //ws.Send("BALUS");
                Console.ReadKey(true);
            }

        }

        private WebSocket CreateWebSocket(string ip, int port)
        {
            return new WebSocket("ws://" + ip + ":" + port + "/Chat");
        }

        private void OnMessageReaction(Object sender, MessageEventArgs e)
        {
            Console.WriteLine("Got a response!");
            Packet answer = JsonConvert.DeserializeObject<Packet>(e.Data);
            if(answer.actionCode == ActionCode.CONFCONN) {
                if(answer.data == "confirmed")
                {
                    Console.WriteLine("Houston, we have a solid connection!");
                    connectionConfirmed = true;
                }
                else
                {
                    Console.WriteLine("Umm, SOMEHOW, the server TOLD us there is no connection?");
                }
            }
        }
    }
}
