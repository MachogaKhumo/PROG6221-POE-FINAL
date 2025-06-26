using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Text.RegularExpressions;

namespace CyberSecurityChatbotGUI
{
    public class ChatBotMenu
    {
        private List<ChatBotsData> data;
        private List<ChatBotTask> tasks = new List<ChatBotTask>();
        private List<string> activityLog = new List<string>();
        private string currentTopic = "";
        private string userInterest = "";
        private Random random = new Random();

        // Conversation states for multi-step task adding
        private enum ConversationState
        {
            None,
            WaitingForTaskTitle,
            WaitingForTaskDescription,
            WaitingForReminder
        }
        private ConversationState currentState = ConversationState.None;
        private ChatBotTask pendingTask = null;

        private List<string> phishingTips = new List<string>()//Tips for phishing
        {
            "📧 Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
            "⚠️ Be wary of suspicious language and tone used such as urgent requests, unfamiliar greetings, bad grammar..",
            "🔗 Do not click any random link without verifying its authenticity by searching for the website on the internet instead.",
            "📧 Phishing tricks users into sharing personal info via fake messages."
        };

        private List<string> passwordTips = new List<string>()//Tips for passwords
        {
            "🔐 Make passwords at least 12 characters long, make use of uppercase and lowercase letters, numbers and symbols.",
            "⚠️ Avoid using personal information and common patterns or words.",
            "🔑 Use unique passwords for each account or device.",
            "🔐 Strong passwords are key. Use a mix and don’t reuse!"
        };

        private List<string> browsingTips = new List<string>()//Tips for browsing
        {
            "🔗 Only visit sites that begin with https:// .",
            "📥 Be cautious about downloads, always use trusted sources to download files.",
            "🛡️ Install and make use of anti-virus software and firewall protection.",
            "🌐 Safe browsing includes avoiding suspicious websites, using HTTPS, and keeping tools updated."
        };

        private List<string> ransomwareTips = new List<string>()//Tips for ransomware attacks
        {
            "🚨 Stay up-to-date and make use of anti-malware software and systems.",
            "🔒 Make use of two-factor authentication and strong passwords.",
            "💾 Do regular backups and offline backups.",
            "💣 Ransomware encrypts your files. Use backups and strong protection."
        };

        private Dictionary<string, int> tipIndexes = new Dictionary<string, int>()
        {
            {"phishing", 0},
            {"password", 0},
            {"browsing", 0},
            {"ransomware", 0}
        };

        public ChatBotMenu()
        {
            LoadData();
        }

        public string GetResponse(string input, string userName)
        {
            input = input.ToLower().Trim();
            input = new string(input.Where(c => !char.IsPunctuation(c)).ToArray());

            // Keyword sets so chatbot can detect and respond more acuratly to the prompt
            string[] taskKeywords = { "add task", "new task", "create task", "task" };
            string[] reminderKeywords = { "remind me", "reminder", "remind" };


            switch (currentState)
            {
                case ConversationState.WaitingForTaskTitle:
                    pendingTask = new ChatBotTask
                    {
                        Title = input,
                        Description = "",
                        IsCompleted = false
                    };
                    currentState = ConversationState.WaitingForTaskDescription;
                    return $"Got it! Please provide a description for the task '{input}'.";

                case ConversationState.WaitingForTaskDescription:
                    if (pendingTask != null)
                    {
                        pendingTask.Description = input;
                        currentState = ConversationState.WaitingForReminder;
                        return $"Task added with the description '{input}'. Would like a reminder?";
                    }
                    return "❗ Could not add task description. Please try again.";

                case ConversationState.WaitingForReminder:
                    if (pendingTask != null)
                    {
                        if (input.ToLower().Contains("remind me in"))
                        {
                            int days = ExtractDays(input);
                            if (days > 0)
                            {
                                pendingTask.ReminderDate = DateTime.Now.AddDays(days);
                            }
                        }
                        else if (input.ToLower() == "no")
                        {
                            pendingTask.ReminderDate = null;
                        }
                        else
                        {
                            // Default to no reminder if input unrecognized
                            pendingTask.ReminderDate = null;
                        }

                        tasks.Add(pendingTask);

                        //Logs the activity as Task added
                        LogActivity($"Task added: '{pendingTask.Title}'{(pendingTask.ReminderDate.HasValue ? $" (Reminder set for {pendingTask.ReminderDate.Value.ToShortDateString()})" : "")}");

                        string reminderText = pendingTask.ReminderDate.HasValue ? $" I'll remind you on {pendingTask.ReminderDate.Value.ToShortDateString()}." : "";
                        string response = $"📝 Task '{pendingTask.Title}' added with description '{pendingTask.Description}'.{reminderText}";

                        // Reset state
                        pendingTask = null;
                        currentState = ConversationState.None;

                        return response;
                    }
                    return "❗ Could not set reminder. Try again.";

                case ConversationState.None:
                default:
                    input = input.ToLower();

                    if (input.Contains("interested in"))
                    {
                        userInterest = input.Split("interested in").Last().Trim();
                        return $"Got it! I'll remember that you're interested in {userInterest}.";
                    }

                    if (input.Contains("show activity log") || input.Contains("activity log") || input.Contains("what have you done"))
                    {
                        return ShowActivityLog();
                    }

                    if (input.Contains("show task") || input.Contains("show tasks") || input.Contains("list tasks"))
                    {
                        if (tasks.Count == 0)
                            return "📭 No tasks yet.";

                        StringBuilder builder = new StringBuilder("📋 Here are your tasks:\n");
                        for (int i = 0; i < tasks.Count; i++)
                        {
                            var task = tasks[i];
                            string status = task.IsCompleted ? "✅ Completed" : "🕒 Pending";
                            string reminder = task.ReminderDate.HasValue ? $"🔔 Reminder: {task.ReminderDate.Value.ToShortDateString()}" : "❌ No reminder set";
                            builder.AppendLine($"\n{i + 1}. 📌 *{task.Title}*\n📝 {task.Description}\n{reminder}\n📌 Status: {status}");
                            builder.AppendLine($"👉 To delete: type `delete task {i + 1}` | To complete: `complete task {i + 1}`");
                        }

                        return builder.ToString();
                    }

                    if (input.Contains("complete task"))
                    {
                        int index = ExtractIndex(input);
                        if (index >= 0 && index < tasks.Count)
                        {
                            tasks[index].IsCompleted = true;
                            LogActivity($"Task completed: '{tasks[index].Title}'");//Logs the activity as Task completed
                            return $"✅ Task '{tasks[index].Title}' marked as completed.";
                        }
                        else
                        {
                            return "I could not find the task you want to mark as complete.";
                        }
                    }

                    if (input.Contains("delete task"))
                    {
                        int index = ExtractIndex(input);
                        if (index >= 0 && index < tasks.Count)
                        {
                            var title = tasks[index].Title;
                            tasks.RemoveAt(index);
                            LogActivity($"Task deleted: '{title}'"); //Logs the activity as Task deleted
                            return $"🗑️ Task '{title}' deleted.";
                        }
                        else
                        {
                            return "I could not find the task you want to delete.";
                        }
                    }

                    if (taskKeywords.Any(k => input.Contains(k))) // Checks if keyword for task is recognized
                    {
                        // Try to extract task description right after keyword phrase
                        string taskDesc = ExtractAfterKeyword(input, taskKeywords);
                        if (!string.IsNullOrEmpty(taskDesc))
                        {
                            currentState = ConversationState.WaitingForTaskDescription;
                            pendingTask = new ChatBotTask { Title = CapitalizeFirstLetter(taskDesc) };
                            return $"Got it! Adding a new task: '{pendingTask.Title}'. Please provide a description.";
                        }
                        else
                        {
                            currentState = ConversationState.WaitingForTaskTitle;
                            return "Sure! What is the name of the new task?";
                        }
                    }

                    if (reminderKeywords.Any(k => input.Contains(k))) // Checks if keyword for reminders is recognized
                    {
                        string taskDesc = ExtractTaskFromReminder(input);
                        int days = ExtractDays(input);
                        if (!string.IsNullOrEmpty(taskDesc))
                        {
                            ChatBotTask newTask = new ChatBotTask
                            {
                                Title = CapitalizeFirstLetter(taskDesc),
                                ReminderDate = days > 0 ? DateTime.Now.AddDays(days) : (DateTime?)null
                            };
                            tasks.Add(newTask);
                            string reminderInfo = newTask.ReminderDate.HasValue ? $" on {newTask.ReminderDate.Value.ToShortDateString()}" : "";
                            LogActivity($"Reminder set: '{newTask.Title}'{reminderInfo}");  //Logs the activity as Reminder set
                            return $"Reminder set for '{newTask.Title}'{reminderInfo}.";
                        }
                    return "I didn't quite get that. Can you rephrase the request?";
                    }

                    // If user asks for tips or more/explain about current topic
                    if ((input.Contains("more") || input.Contains("explain")) && !string.IsNullOrEmpty(currentTopic))
                    {
                        // Just get the next tip, do NOT reset index
                        return GetMoreDetails(currentTopic);
                    }

                    if (input.Contains("phishing") && input.Contains("tip"))
                    {
                        currentTopic = "phishing";
                        tipIndexes["phishing"] = 0; // Reset BEFORE getting the tip
                        return GetMoreDetails(currentTopic);
                    }

                    if (input.Contains("password") && input.Contains("tip"))
                    {
                        currentTopic = "password";
                        tipIndexes["password"] = 0; // Reset BEFORE getting the tip
                        return GetMoreDetails(currentTopic);
                    }

                    if (input.Contains("browsing") && input.Contains("tip"))
                    {
                        currentTopic = "browsing";
                        tipIndexes["browsing"] = 0; //Reset BEFORE getting the tip
                        return GetMoreDetails(currentTopic);
                    }

                    if (input.Contains("ransomware") && input.Contains("tip"))
                    {
                        currentTopic = "ransomware";
                        tipIndexes["ransomware"] = 0; // Reset BEFORE getting the tip
                        return GetMoreDetails(currentTopic);
                    }

                    var match = data.FirstOrDefault(d => input.Contains(d.Subject.ToLower()));
                    if (match != null)
                    {
                        currentTopic = match.Subject.ToLower();
                        return match.Content;
                    }

                    if (input == "how are you")
                        return "🤖 I am as well as a program can be, thank you for asking!";
                    if (input == "why is cybersecurity important")
                        return "🤖 To protect you and others from cyber threats and attacks.";
                    if (input == "what's your purpose" || input == "what is your purpose")
                        return "🎯 I'm here to help you learn about cybersecurity and inform you on best practices.";
                    if (input == "what can i ask you about" || input == "help")
                        return $"💡 You can ask me about:\n- Password safety\n- Phishing\n- Safe browsing\n- Ransomware\n{(string.IsNullOrEmpty(userInterest) ? string.Empty : $"\n🧠 Since you're interested in {userInterest}, I can give you more on that too!")}";

                    return "I didn't quite get that. Can you rephrase or ask for a tip on passwords, phishing, etc.?";
            }
        }

        public void LogCustomActivity(string description) // Retrives activities logged from ChatBotForm
        {
            LogActivity(description);
        }

        private void LogActivity(string description) //Logs activities
        {
            string entry = $"[{DateTime.Now:HH:mm}] {description}";
            activityLog.Add(entry);
            if (activityLog.Count > 10)
                activityLog.RemoveAt(0);
        }

        private string ShowActivityLog()//Displays the activities logged when users prompts chatbot
        {
            if (activityLog.Count == 0)
                return "📭 No activity logged yet.";

            StringBuilder builder = new StringBuilder("📌 Here's a summary of all actions:\n");
            foreach (var entry in activityLog)
                builder.AppendLine($"- {entry}");

            return builder.ToString();
        }

        private int ExtractDays(string input)//Calculates the date reminder or task is set for by adding the number of days to the current date
        {
            var parts = input.Split(' ');
            return int.TryParse(parts[^2], out int days) ? days : 0;
        }

        private int ExtractIndex(string input)
        {
            var parts = input.Split(' ');
            return int.TryParse(parts[^1], out int i) ? i - 1 : -1;
        }

        private string ExtractAfterKeyword(string input, string[] keywords)//Checks for keywords so chatbot can respon accordingly
        {
            foreach (var kw in keywords)
            {
                int pos = input.IndexOf(kw);
                if (pos >= 0)
                {
                    string after = input.Substring(pos + kw.Length).Trim();
                    if (!string.IsNullOrEmpty(after))
                        return after;
                }
            }
            return null;
        }

        private string ExtractTaskFromReminder(string input) //Checks for keywords of reminder so chatbot can respond accordingly
        {
            var match = Regex.Match(input, @"remind me to (.+?)( tomorrow| today| next week| on \w+|$)");
            if (match.Success)
                return CapitalizeFirstLetter(match.Groups[1].Value.Trim());
            return null;
        }

        private string GetMoreDetails(string topic)//Provides additional information when user prompts for "more"
        {
            List<string> tipsList = topic switch
            {
                "phishing" => phishingTips,
                "password" => passwordTips,
                "browsing" => browsingTips,
                "ransomware" => ransomwareTips,
                _ => null
            };

            if (tipsList == null)
                return "Sorry, I don’t have tips on that topic.";

            int idx = tipIndexes[topic];

            if (idx < tipsList.Count)
            {
                string tip = tipsList[idx];
                tipIndexes[topic] = idx + 1; // increment index for next tip
                return tip;
            }
            else
            {
                return $"You've seen all the tips for {topic}! If you want, ask about another topic.";
            }
        }

        private void LoadData()
        {
            data = new List<ChatBotsData>
            {
                new ChatBotsData { Subject = "password", Content = "🔑 Use strong and complex passwords, never reuse passwords and consider password managers to store passwords!" },
                new ChatBotsData { Subject = "phishing", Content = "📧 Beware of emails asking for personal information. Always verify the sender!" },
                new ChatBotsData { Subject = "browsing", Content = "🌐 Always use HTTPS websites, avoid suspicious links, and install browser security extensions." },
                new ChatBotsData { Subject = "ransomware", Content = " Be cautious about who or what you allow access to your data, hackers will steal your data and lock it away from you unless you pay them." }
            };
        }

        private string CapitalizeFirstLetter(string input)// Capitalizes the first letter of the sentence
        {
            if (string.IsNullOrEmpty(input)) return input;
            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}
