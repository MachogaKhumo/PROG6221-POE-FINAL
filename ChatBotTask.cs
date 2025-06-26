using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityChatbotGUI
{
    public class ChatBotTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString()//completion status
        {
            string status = IsCompleted ? "✅ Done" : "🕒 Pending";
            string reminder = ReminderDate.HasValue ? $"⏰ Reminder: {ReminderDate.Value.ToShortDateString()}" : "";
            return $"{Title} - {Description} {reminder} [{status}]";
        }
    }
}
