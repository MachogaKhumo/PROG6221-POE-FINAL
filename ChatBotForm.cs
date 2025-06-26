using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;

namespace CyberSecurityChatbotGUI
{
    public partial class ChatBotForm : Form
    {
        private ChatBotMenu chatbot;
        private string userName;
        private List<ChatBotQuiz> quizQuestions;
        private int currentQuestionIndex = 0;
        private int score = 0;

        public ChatBotForm() //Start page and prompt when code first runs
        {
            InitializeComponent();
            chatbot = new ChatBotMenu();
            AppendChat("🤖 CyberBot: Welcome! Please enter your name to begin.");
        }

        private void sendButton_Click(object sender, EventArgs e) //Logic for send button
        {
            string input = userInputBox.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            AppendChat($"You: {input}");
            userInputBox.Clear();

            if (string.IsNullOrEmpty(userName))
            {
                userName = input;
                AppendChat($"🤖 CyberBot: Nice to meet you, {userName}! How can I assist you today?");
                return;
            }

            if (input.ToLower().Contains("start quiz"))//Retrives questions from ChatBotQuizQuestions
            {
                chatbot.LogCustomActivity("Started cybersecurity quiz");//Logs the activity as Started cybersecurity quiz
                quizQuestions = ChatBotQuizQuestions.GetQuestions();
                currentQuestionIndex = 0;
                score = 0;
                DisplayQuizQuestion(currentQuestionIndex);
                return;
            }

            string response = chatbot.GetResponse(input, userName);
            AppendChat($"🤖 CyberBot: {response}");
        }

        private void AppendChat(string message)
        {
            chatDisplayBox.AppendText(message + Environment.NewLine);
            chatDisplayBox.ScrollToCaret();
        }

        private void DisplayQuizQuestion(int index)//Design of how the quiz questions will be displayed
        {
            if (index >= quizQuestions.Count)
            {
                ShowQuizResults();
                return;
            }

            var q = quizQuestions[index];

            quizQuestionLabel.Text = $"Q{index + 1}: {q.QuestionText}";
            option1Radio.Text = q.Options[0];
            option2Radio.Text = q.Options[1];
            option3Radio.Text = q.Options[2];
            option4Radio.Text = q.Options[3];

            option1Radio.Checked = false;
            option2Radio.Checked = false;
            option3Radio.Checked = false;
            option4Radio.Checked = false;

            quizGroupBox.Visible = true;
        }

        private void submitAnswerButton_Click(object sender, EventArgs e)//Logic for submit button for the quiz
        {
            if (currentQuestionIndex >= quizQuestions.Count)
                return;

            int selectedOption = GetSelectedOptionIndex();
            if (selectedOption == -1)
            {
                MessageBox.Show("Please select an answer.");
                return;
            }

            var currentQuestion = quizQuestions[currentQuestionIndex];
            bool isCorrect = selectedOption == currentQuestion.CorrectOption;

            string feedback = isCorrect ? "✅ Correct!" : $"❌ Incorrect. {currentQuestion.Feedback}";
            if (isCorrect) score++;

            AppendChat($"🤖 CyberBot: {feedback}");
            currentQuestionIndex++;

            if (currentQuestionIndex < quizQuestions.Count)
                DisplayQuizQuestion(currentQuestionIndex);
            else
                ShowQuizResults();
        }

        private int GetSelectedOptionIndex()//retrives the answer starting with 0 where 0 = first option
        {
            if (option1Radio.Checked) return 0;
            if (option2Radio.Checked) return 1;
            if (option3Radio.Checked) return 2;
            if (option4Radio.Checked) return 3;
            return -1;
        }

        private void ShowQuizResults()//Displays the results of the quiz 
        {
            quizGroupBox.Visible = false;
            string resultMessage = $"🎯 Quiz complete! You scored {score}/{quizQuestions.Count}.";

            if (score == quizQuestions.Count)
                resultMessage += " 🏆 Perfect score! You're a cybersecurity pro!";
            else if (score >= quizQuestions.Count / 2)
                resultMessage += " 👍 Good job! Keep practicing to improve even more.";
            else
                resultMessage += " 📚 Keep learning to stay safe online.";

            AppendChat($"🤖 CyberBot: {resultMessage}");
            chatbot.LogCustomActivity($"Completed quiz with score: {score}/{quizQuestions.Count}");//Logs the activity as Completed quiz with score
        }

    }
}
