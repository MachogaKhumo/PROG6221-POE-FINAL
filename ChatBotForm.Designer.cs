using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityChatbotGUI
{
    partial class ChatBotForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox userInputBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.RichTextBox chatDisplayBox;

        //Quiz Elements
        private GroupBox quizGroupBox;
        private Label quizQuestionLabel;
        private RadioButton option1Radio;
        private RadioButton option2Radio;
        private RadioButton option3Radio;
        private RadioButton option4Radio;
        private Button submitAnswerButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.userInputBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.chatDisplayBox = new System.Windows.Forms.RichTextBox();

            // 🧠 Quiz Controls
            this.quizGroupBox = new GroupBox();
            this.quizQuestionLabel = new Label();
            this.option1Radio = new RadioButton();
            this.option2Radio = new RadioButton();
            this.option3Radio = new RadioButton();
            this.option4Radio = new RadioButton();
            this.submitAnswerButton = new Button();

            this.SuspendLayout();
            //
            // chatDisplayBox
            //
            this.chatDisplayBox.Location = new System.Drawing.Point(12, 12);
            this.chatDisplayBox.Size = new System.Drawing.Size(560, 300);
            this.chatDisplayBox.ReadOnly = true;
            //
            // userInputBox
            //
            this.userInputBox.Location = new System.Drawing.Point(12, 320);
            this.userInputBox.Size = new System.Drawing.Size(460, 23);
            //
            // sendButton
            //
            this.sendButton.Location = new System.Drawing.Point(480, 318);
            this.sendButton.Size = new System.Drawing.Size(92, 27);
            this.sendButton.Text = "Send";
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            //
            //QuizBox
            //
            this.quizGroupBox.Text = "Cybersecurity Quiz";
            this.quizGroupBox.Location = new System.Drawing.Point(12, 360);
            this.quizGroupBox.Size = new System.Drawing.Size(560, 200);
            this.quizGroupBox.Visible = false;
            //
            //QuizLabel
            //
            this.quizQuestionLabel.Location = new System.Drawing.Point(10, 20);
            this.quizQuestionLabel.AutoSize = true;
            this.quizQuestionLabel.MaximumSize = new System.Drawing.Size(540, 0);
            //
            //Quiz Radio Buttons
            //
            this.option1Radio.Location = new System.Drawing.Point(10, 50);
            this.option1Radio.Size = new System.Drawing.Size(540, 20);

            this.option2Radio.Location = new System.Drawing.Point(10, 75);
            this.option2Radio.Size = new System.Drawing.Size(540, 20);

            this.option3Radio.Location = new System.Drawing.Point(10, 100);
            this.option3Radio.Size = new System.Drawing.Size(540, 20);

            this.option4Radio.Location = new System.Drawing.Point(10, 125);
            this.option4Radio.Size = new System.Drawing.Size(540, 20);
            //
            //Submit answer button
            //
            this.submitAnswerButton.Text = "Submit Answer";
            this.submitAnswerButton.Location = new System.Drawing.Point(10, 155);
            this.submitAnswerButton.Size = new System.Drawing.Size(120, 30);
            this.submitAnswerButton.Click += new EventHandler(this.submitAnswerButton_Click);
            //
            //AllquizGroupbox
            //
            this.quizGroupBox.Controls.Add(this.quizQuestionLabel);
            this.quizGroupBox.Controls.Add(this.option1Radio);
            this.quizGroupBox.Controls.Add(this.option2Radio);
            this.quizGroupBox.Controls.Add(this.option3Radio);
            this.quizGroupBox.Controls.Add(this.option4Radio);
            this.quizGroupBox.Controls.Add(this.submitAnswerButton);
            //
            // ChatBotForm
            //
            this.ClientSize = new System.Drawing.Size(584, 580);
            this.Controls.Add(this.chatDisplayBox);
            this.Controls.Add(this.userInputBox);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.quizGroupBox);

            this.Text = "Cybersecurity ChatBot";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
