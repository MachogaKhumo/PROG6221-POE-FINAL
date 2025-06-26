using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityChatbotGUI
{
    public class ChatBotQuiz //the questions, answers, options and feedback are called here 
    {
        public string QuestionText { get; set; }
        public string[] Options { get; set; }
        public int CorrectOption { get; set; }
        public string Feedback { get; set; }
    }
}
