using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace ServerApp
{
    class Server
    {
        WebSocketServer wssv;
        public Server()
        {
            wssv = new WebSocketServer(46495);
            wssv.AddWebSocketService<Chat>("/Chat");
            wssv.Start();

            if (wssv.IsListening)
            {
                Console.WriteLine("Listening on port {0}, and providing WebSocket services:", wssv.Port);
                foreach (var path in wssv.WebSocketServices.Paths)
                    Console.WriteLine("- {0}", path);
            }

        }

        public void CleanUp()
        {
            wssv.Stop();
        }

        public class Chat : WebSocketBehavior
        {
            protected override void OnMessage(MessageEventArgs e)
            {
                Console.WriteLine("serverReact");
                Packet packet = JsonConvert.DeserializeObject<Packet>(e.Data);
                Packet answer;

                //If client request active connection confirmation
                if (packet.actionCode == ActionCode.CONFCONN)
                {
                    answer = new Packet(ActionCode.CONFCONN, "confirmed");
                }
                else
                {
                    answer = new Packet(ActionCode.UNKNOWN, "");
                }
                
                Send(JsonConvert.SerializeObject(answer));
            }
        }
    }
}
