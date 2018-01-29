﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    class Datastructures
    {
    }


    [Serializable]
    public class ChatMessages
    {
        public List<Message> messages { get; set; }
        public ChatMessages(List<Message> messages)
        {
            this.messages = messages;
        }


        public ChatMessages()
        {
            messages = new List<Message>();
        }

        public void AddMessage(string content)
        {
            messages.Add(new Message(content));
        }

        public class Message
        {
            public Message(string content)
            {
                this.content = content;
                createdAt = DateTime.Now;
                creator = -1;
            }

            public DateTime createdAt { get; set; }
            public string content { get; set; }
            public int creator { get; set; }
        }
    }
}
