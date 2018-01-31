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

        List<int> idList = new List<int>();
        Queue<int> releasedIds = new Queue<int>();
        int maxID = 0;

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

        public int GiveUniqueID()
        {
            if (releasedIds.Count == 0)
            {
                maxID++;
                idList.Add(maxID);
                Console.WriteLine("SERVER: Giving out new id with value of " + maxID);
                return maxID;
            }
            else
            {
                int id = releasedIds.Dequeue();
                idList.Add(id);
                return id;
            }
        }

        public class Chat : WebSocketBehavior
        {

            protected override void OnMessage(MessageEventArgs e)
            {
                Console.WriteLine("Received a message!");
                Console.WriteLine("It contains: " + e.Data);
                Packet packet = JsonConvert.DeserializeObject<Packet>(e.Data);

                Packet answer;

                //If client request active connection confirmation
                if (packet.actionCode == ActionCode.CONFCONN)
                {
                    Console.WriteLine("Sending a confirmation");
                    answer = new Packet(ActionCode.CONFCONN, "confirmed");
                } else if (packet.actionCode == ActionCode.UNIQUEID)
                {
                    answer = new Packet(ActionCode.UNIQUEID, Give);
                }
                else
                {
                    Console.WriteLine("Sending a confused message");

                    answer = new Packet(ActionCode.UNKNOWN, "");
                }

                Send(JsonConvert.SerializeObject(answer));
            }
        }
    }
}
