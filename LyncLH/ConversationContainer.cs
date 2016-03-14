using System;
using Microsoft.Lync.Model.Conversation;

namespace SkyForBusLH
{
    public class ConversationContainer
    {
        public Conversation Conversation { get; set; }
        public DateTime ConversationCreated { get; set; }
        public int ConversationId { get; set; }
    }
}
