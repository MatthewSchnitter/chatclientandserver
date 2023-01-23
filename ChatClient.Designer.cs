using Microsoft.Extensions.Logging;

/// <summary> 
/// Author:    Griffin Shannon and Matthew Schnitter
/// Date:      4/11/2022 
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500, Griffin Shannon, and Matthew Schnitter - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Matthew Schnitter, and I, Griffin Shannon, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// Designer for ChatClient
///  
/// </summary>

namespace ChatClient
{
    /// <summary>
    /// Designer for ChatClient
    /// </summary>
    partial class ChatClient
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serverNameLabel = new System.Windows.Forms.Label();
            this.serverNameTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.typingTextBox = new System.Windows.Forms.TextBox();
            this.yourNameTextBox = new System.Windows.Forms.Label();
            this.typingLabel = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.participantsTextBox = new System.Windows.Forms.TextBox();
            this.chatTextBox = new System.Windows.Forms.TextBox();
            this.retrieveParticipantsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverNameLabel
            // 
            this.serverNameLabel.AutoSize = true;
            this.serverNameLabel.Location = new System.Drawing.Point(65, 41);
            this.serverNameLabel.Name = "serverNameLabel";
            this.serverNameLabel.Size = new System.Drawing.Size(121, 15);
            this.serverNameLabel.TabIndex = 0;
            this.serverNameLabel.Text = "Server Name/Address";
            // 
            // serverNameTextBox
            // 
            this.serverNameTextBox.Location = new System.Drawing.Point(204, 38);
            this.serverNameTextBox.Name = "serverNameTextBox";
            this.serverNameTextBox.Size = new System.Drawing.Size(282, 23);
            this.serverNameTextBox.TabIndex = 1;
            this.serverNameTextBox.Text = "localhost";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(204, 83);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(282, 23);
            this.nameTextBox.TabIndex = 2;
            // 
            // typingTextBox
            // 
            this.typingTextBox.Location = new System.Drawing.Point(204, 217);
            this.typingTextBox.Name = "typingTextBox";
            this.typingTextBox.Size = new System.Drawing.Size(464, 23);
            this.typingTextBox.TabIndex = 3;
            this.typingTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.typingTextBox_KeyPress);
            // 
            // yourNameTextBox
            // 
            this.yourNameTextBox.AutoSize = true;
            this.yourNameTextBox.Location = new System.Drawing.Point(65, 86);
            this.yourNameTextBox.Name = "yourNameTextBox";
            this.yourNameTextBox.Size = new System.Drawing.Size(66, 15);
            this.yourNameTextBox.TabIndex = 4;
            this.yourNameTextBox.Text = "Your Name";
            // 
            // typingLabel
            // 
            this.typingLabel.AutoSize = true;
            this.typingLabel.Location = new System.Drawing.Point(65, 220);
            this.typingLabel.Name = "typingLabel";
            this.typingLabel.Size = new System.Drawing.Size(93, 21);
            this.typingLabel.TabIndex = 5;
            this.typingLabel.Text = "Type Something";
            this.typingLabel.UseCompatibleTextRendering = true;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(204, 135);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(282, 29);
            this.connectButton.TabIndex = 6;
            this.connectButton.Text = "Connect to Server";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // participantsTextBox
            // 
            this.participantsTextBox.Location = new System.Drawing.Point(799, 38);
            this.participantsTextBox.Multiline = true;
            this.participantsTextBox.Name = "participantsTextBox";
            this.participantsTextBox.ReadOnly = true;
            this.participantsTextBox.Size = new System.Drawing.Size(222, 217);
            this.participantsTextBox.TabIndex = 7;
            // 
            // chatTextBox
            // 
            this.chatTextBox.AcceptsReturn = true;
            this.chatTextBox.Location = new System.Drawing.Point(113, 326);
            this.chatTextBox.Multiline = true;
            this.chatTextBox.Name = "chatTextBox";
            this.chatTextBox.ReadOnly = true;
            this.chatTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chatTextBox.Size = new System.Drawing.Size(756, 230);
            this.chatTextBox.TabIndex = 8;
            // 
            // retrieveParticipantsButton
            // 
            this.retrieveParticipantsButton.Location = new System.Drawing.Point(799, 261);
            this.retrieveParticipantsButton.Name = "retrieveParticipantsButton";
            this.retrieveParticipantsButton.Size = new System.Drawing.Size(222, 23);
            this.retrieveParticipantsButton.TabIndex = 9;
            this.retrieveParticipantsButton.Text = "Retrieve Participants";
            this.retrieveParticipantsButton.UseVisualStyleBackColor = true;
            this.retrieveParticipantsButton.Click += new System.EventHandler(this.retrieveParticipantsButton_Click);
            // 
            // ChatClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 595);
            this.Controls.Add(this.retrieveParticipantsButton);
            this.Controls.Add(this.chatTextBox);
            this.Controls.Add(this.participantsTextBox);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.typingLabel);
            this.Controls.Add(this.yourNameTextBox);
            this.Controls.Add(this.typingTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.serverNameTextBox);
            this.Controls.Add(this.serverNameLabel);
            this.Name = "ChatClient";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChatClient_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label serverNameLabel;
        private TextBox serverNameTextBox;
        private TextBox nameTextBox;
        private TextBox typingTextBox;
        private Label yourNameTextBox;
        private Label typingLabel;
        private Button connectButton;
        private TextBox participantsTextBox;
        private TextBox chatTextBox;
        private Button retrieveParticipantsButton;
    }
}