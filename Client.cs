using System;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;

namespace ServerApp
{
    class Client
    {
        public bool connectionConfirmed { get; private set; }
        private int id { get; set; }

        public Client()
        {
            connectionConfirmed = false;
            // CreateWebSocket()
            // CheckConnection()
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

                Thread.Sleep(1000);
                if (connectionConfirmed)
                {
                    Packet getID = new Packet(ActionCode.UNIQUEID, "");
                    ws.Send(JsonConvert.SerializeObject(getID));
                }
                Thread.Sleep(1000);

                //ws.Send("BALUS");
                //Console.ReadKey(true);
            }

        }

        private WebSocket CreateWebSocket(string ip, int port)
        {
            return new WebSocket("ws://" + ip + ":" + port + "/Chat");
        }

        private void RequestUniqueId(WebSocket ws)
        {
            ws.Send(JsonConvert.SerializeObject(   
                new Packet(ActionCode.UNIQUEID, "")));
        }

        private void OnMessageReaction(Object sender, MessageEventArgs e)
        {
            Console.WriteLine("Got a response!");
            Packet answer = JsonConvert.DeserializeObject<Packet>(e.Data);

            switch (answer.actionCode)
            {
                case ActionCode.CONFCONN:
                    if (answer.data == "confirmed")
                    {
                        Console.WriteLine("Houston, we have a solid connection!");
                        connectionConfirmed = true;
                    }
                    else
                    {
                        Console.WriteLine("Umm, SOMEHOW, the server TOLD us there is no connection?");
                    }
                    break;
                case ActionCode.UNIQUEID:
                    id = Int32.Parse(answer.data);
                    Console.WriteLine("My new unique chat ID is " + id);
                    break;
                case ActionCode.SENDMSG:
                    break;
                case ActionCode.RECMSG:
                    break;
                case ActionCode.UNKNOWN:
                    break;
                default:
                    break;
            }
        }
    }
}
