# PROG6221-POE-FINAL
Here's an updated and complete `README.md` section for **Part 3 / Final POE** of your CyberSecurity Chatbot GUI project, including setup instructions, usage guidance, and examples:

---

# 🛡️ CyberSecurity Chatbot GUI - Part 3 / Final POE

## 📚 Overview

This is **Part 3** (Final POE) of the CyberSecurity Chatbot GUI application. It builds upon the foundational chatbot created in Parts 1 and 2, integrating new GUI-based features:

* ✅ **Task Assistant with Reminders**
* 🧠 **Cybersecurity Mini-Game (Quiz)**
* 💬 **NLP Simulation with GUI**
* 📑 **Activity Log**
* 🔗 **Seamless integration with existing chatbot features**

---

## 🚀 Features Overview

### ✅ Task Assistant with Reminders

* Add tasks with optional due dates.
* Automatic reminders triggered based on the current date.
* Fully interactive GUI form with inputs and task display.
* Logs task creation and reminders to the Activity Log.

### 🧠 Cybersecurity Mini-Game (Quiz)

* Multiple-choice quiz on key cybersecurity concepts.
* Instant feedback after each answer.
* Final score summary and encouragement.
* Tracks quiz activity in the log.

### 💬 NLP Chatbot Simulation

* Responds to various inputs using keywords and intent detection.
* Personalized responses using the user’s name.
* Natural Language interaction simulation.
* Retains and builds on functionality from Parts 1 and 2 (sentiment detection, dynamic response).

### 📑 Activity Log

* All major actions are logged: starting the quiz, completing the quiz, setting reminders, etc.
* View logs in a separate GUI form.
* Helps track usage and learning history.

---

## 🖥️ Setup Instructions (Visual Studio 2022 - Windows Forms App)

1. **Open Visual Studio 2022**.
2. **Create a new project** > Select **Windows Forms App (.NET Framework)**.
3. **Name your project**: `CyberSecurityChatbotGUIst10396677`.
4. Add the following Form:

   * `ChatBotForm`
     
5. Add the following classes to your project:

   * `ChatBotMenu.cs` (Handles responses and logging)
   * `ChatBotQuiz.cs` (Quiz question model)
   * `ChatBotQuizQuestions.cs` (Holds list of questions)
   * `ChatBotData.cs` (Static log list manager)
   * `ChatBotTask.cs` (Handles scheduling reminders)
6. **Design each form** using the Windows Forms designer:

   * Add labels, textboxes, buttons, for Chatbot form
7. Run the application from `ChatBotForm`.

---

## 📸 Example Usage

1. **Launch the App** → `ChatBotForm` appears with buttons.
2. **Click "NLP Chatbot"** → Opens chat form.
3. Type your name when prompted:

   ```
   CyberBot: Welcome! Please enter your name.
   You: John
   CyberBot: Nice to meet you, John! How can I assist you today?
   ```
4. Ask something like:

   ```
   You: How can I stay safe online?
   CyberBot: Always use strong passwords and enable 2FA.
   ```
5. Start a quiz:

   ```
   You: start quiz
   CyberBot: Q1: What is phishing? [Options appear]
   ```
6. Submit answers and get your score. Results are automatically logged.
7. Add a task in the Task Assistant and see reminders triggered on due date.
8. Click **"View Log"** to see all activities.

---

## 🧪 Testing Tips

* Test NLP responses for various cybersecurity topics.
* Try adding tasks with and without due dates.
* Use incorrect quiz answers to test feedback.
* Check Activity Log updates after each interaction.

---

## 🔧 Dependencies

* .NET Framework (Windows Forms App Template)
* Visual Studio 2022

> No third-party libraries required. All functionality is handled with standard C# and Windows Forms.

---

## 📁 File Structure Summary

```
CyberSecurityChatbotGUI/
│
├── ChatbotForm.cs
|---ChatBotForm.Designer.cs
├── ChatBotMenu.cs
├── ChatBotQuiz.cs
├── ChatBotQuizQuestions.cs
├── ChatBotsData.cs
|---ChatBotTask.cs


## 🏁 Conclusion

This final POE brings together interactivity, security education, and a responsive chatbot into a cohesive application. Each feature is built with usability in mind and integrated into a professional GUI layout.

