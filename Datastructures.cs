using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{

    [Serializable]
    public class ChatMessages
    {
        //Fields
        public List<Message> messages { get; set; }

        //Contructors
        public ChatMessages(List<Message> messages)
        {
            this.messages = messages;
        }

        public ChatMessages()
        {
            messages = new List<Message>();
        }

        //Methods
        public void AddMessage(string content)
        {
            messages.Add(new Message(content, DateTime.Now, -1));
        }

        //Message class
        public class Message
        {
            public Message(string content, DateTime createdAt, int creatorId)
            {
                this.content = content;
                this.createdAt = createdAt;
                this.creatorId = creatorId;
            }
            public Message(string content, int creatorId)
            {
                this.content = content;
                createdAt = DateTime.Now;
                this.creatorId = creatorId;
            }

            public DateTime createdAt { get; set; }
            public string content { get; set; }
            public int creatorId { get; set; }
        }
    }
}
