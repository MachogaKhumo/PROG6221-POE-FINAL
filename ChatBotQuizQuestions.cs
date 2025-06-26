using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityChatbotGUI
{
    internal class ChatBotQuizQuestions
    {
        public static List<ChatBotQuiz> GetQuestions()//questions that are asked in the quiz are coded into this class
        {
            return new List<ChatBotQuiz>
            {
                new ChatBotQuiz
                {
                    QuestionText = "What should you do if you receive an email asking for your password?",
                    Options = new[] { "Reply with your password", "Delete the email", "Report the email as phishing", "Ignore it" },
                    CorrectOption = 2,
                    Feedback = "📨 Reporting phishing emails helps prevent scams and protects others too."
                },
                new ChatBotQuiz
                {
                    QuestionText = "Which of the following is a strong password?",
                    Options = new[] { "123456", "Abby2021", "Password!", "tG7#xL9&v!" },
                    CorrectOption = 3,
                    Feedback = "🔐 Strong passwords include random characters, numbers, and symbols."
                },
                new ChatBotQuiz
                {
                    QuestionText = "True or False: You should use the same password for multiple accounts.",
                    Options = new[] { "True", "False", "", "" },
                    CorrectOption = 1,
                    Feedback = "⚠️ Reusing passwords increases the risk of account compromise."
                },
                new ChatBotQuiz
                {
                    QuestionText = "What does HTTPS in a website URL indicate?",
                    Options = new[] { "It’s hosted in the US", "It’s a secure and encrypted connection", "It’s a paid website", "It’s a blog" },
                    CorrectOption = 1,
                    Feedback = "🔒 HTTPS ensures your connection to the site is encrypted."
                },
                new ChatBotQuiz
                {
                    QuestionText = "What is phishing?",
                    Options = new[] { "A sport", "A way to catch viruses", "A scam tricking users into revealing info", "A secure website" },
                    CorrectOption = 2,
                    Feedback = "🎣 Phishing scams pretend to be trustworthy to steal information."
                },
                new ChatBotQuiz
                {
                    QuestionText = "True or False: It’s okay to click on unknown email links if they look urgent.",
                    Options = new[] { "True", "False", "", "" },
                    CorrectOption = 1,
                    Feedback = "⚠️ Always verify links before clicking, especially in urgent messages."
                },
                new ChatBotQuiz
                {
                    QuestionText = "Why is two-factor authentication important?",
                    Options = new[] { "It annoys hackers", "It speeds up login", "It adds an extra layer of security", "It’s required by all sites" },
                    CorrectOption = 2,
                    Feedback = "🔐 2FA helps prevent unauthorized access even if your password is stolen."
                },
                new ChatBotQuiz
                {
                    QuestionText = "Which of the following could be a sign of a phishing attempt?",
                    Options = new[] { "An email from a known sender", "Perfect grammar", "A request for personal info and urgency", "Company logo used correctly" },
                    CorrectOption = 2,
                    Feedback = "📧 Phishing often uses urgency and requests for sensitive info."
                },
                new ChatBotQuiz
                {
                    QuestionText = "What is a secure way to store passwords?",
                    Options = new[] { "In a notebook", "In your browser", "Using a password manager", "Memorizing all of them" },
                    CorrectOption = 2,
                    Feedback = "🧠 Password managers encrypt and store your credentials securely."
                },
                new ChatBotQuiz
                {
                    QuestionText = "True or False: Antivirus software protects against ransomware.",
                    Options = new[] { "True", "False", "", "" },
                    CorrectOption = 0,
                    Feedback = "🛡️ Antivirus software helps detect and block ransomware attacks."
                }
            };
        }
    }
}
