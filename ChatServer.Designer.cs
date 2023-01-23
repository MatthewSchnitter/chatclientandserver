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
/// Design for ChatServer
///  
/// </summary>

namespace ChatServer
{
    /// <summary>
    /// Designer for ChatServer
    /// </summary>
    partial class ChatServer
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
            this.participantsBox = new System.Windows.Forms.TextBox();
            this.ParticipantsLabel = new System.Windows.Forms.Label();
            this.shutdownButton = new System.Windows.Forms.Button();
            this.ServerNameLabel = new System.Windows.Forms.Label();
            this.serverNameBox = new System.Windows.Forms.TextBox();
            this.serverIPLabel = new System.Windows.Forms.Label();
            this.addressBox = new System.Windows.Forms.TextBox();
            this.chatBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // participantsBox
            // 
            this.participantsBox.BackColor = System.Drawing.SystemColors.Window;
            this.participantsBox.Enabled = false;
            this.participantsBox.Location = new System.Drawing.Point(63, 92);
            this.participantsBox.Margin = new System.Windows.Forms.Padding(2);
            this.participantsBox.Multiline = true;
            this.participantsBox.Name = "participantsBox";
            this.participantsBox.ReadOnly = true;
            this.participantsBox.Size = new System.Drawing.Size(246, 259);
            this.participantsBox.TabIndex = 0;
            // 
            // ParticipantsLabel
            // 
            this.ParticipantsLabel.AutoSize = true;
            this.ParticipantsLabel.Location = new System.Drawing.Point(63, 64);
            this.ParticipantsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ParticipantsLabel.Name = "ParticipantsLabel";
            this.ParticipantsLabel.Size = new System.Drawing.Size(69, 15);
            this.ParticipantsLabel.TabIndex = 1;
            this.ParticipantsLabel.Text = "Participants";
            // 
            // shutdownButton
            // 
            this.shutdownButton.Location = new System.Drawing.Point(63, 365);
            this.shutdownButton.Margin = new System.Windows.Forms.Padding(2);
            this.shutdownButton.Name = "shutdownButton";
            this.shutdownButton.Size = new System.Drawing.Size(244, 20);
            this.shutdownButton.TabIndex = 2;
            this.shutdownButton.Text = "Shutdown Server";
            this.shutdownButton.UseVisualStyleBackColor = true;
            this.shutdownButton.Click += new System.EventHandler(this.shutdownButton_Click);
            // 
            // ServerNameLabel
            // 
            this.ServerNameLabel.AutoSize = true;
            this.ServerNameLabel.Location = new System.Drawing.Point(432, 32);
            this.ServerNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ServerNameLabel.Name = "ServerNameLabel";
            this.ServerNameLabel.Size = new System.Drawing.Size(74, 15);
            this.ServerNameLabel.TabIndex = 3;
            this.ServerNameLabel.Text = "Server Name";
            // 
            // serverNameBox
            // 
            this.serverNameBox.Location = new System.Drawing.Point(540, 32);
            this.serverNameBox.Margin = new System.Windows.Forms.Padding(2);
            this.serverNameBox.Name = "serverNameBox";
            this.serverNameBox.Size = new System.Drawing.Size(155, 23);
            this.serverNameBox.TabIndex = 4;
            // 
            // serverIPLabel
            // 
            this.serverIPLabel.AutoSize = true;
            this.serverIPLabel.Location = new System.Drawing.Point(405, 64);
            this.serverIPLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.serverIPLabel.Name = "serverIPLabel";
            this.serverIPLabel.Size = new System.Drawing.Size(97, 15);
            this.serverIPLabel.TabIndex = 5;
            this.serverIPLabel.Text = "Server IP Address";
            // 
            // addressBox
            // 
            this.addressBox.Location = new System.Drawing.Point(533, 64);
            this.addressBox.Margin = new System.Windows.Forms.Padding(2);
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(162, 23);
            this.addressBox.TabIndex = 6;
            // 
            // chatBox
            // 
            this.chatBox.Location = new System.Drawing.Point(393, 92);
            this.chatBox.Margin = new System.Windows.Forms.Padding(2);
            this.chatBox.Multiline = true;
            this.chatBox.Name = "chatBox";
            this.chatBox.ReadOnly = true;
            this.chatBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chatBox.Size = new System.Drawing.Size(302, 306);
            this.chatBox.TabIndex = 7;
            // 
            // ChatServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(890, 424);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.addressBox);
            this.Controls.Add(this.serverIPLabel);
            this.Controls.Add(this.serverNameBox);
            this.Controls.Add(this.ServerNameLabel);
            this.Controls.Add(this.shutdownButton);
            this.Controls.Add(this.ParticipantsLabel);
            this.Controls.Add(this.participantsBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ChatServer";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox participantsBox;
        private Label ParticipantsLabel;
        private Button shutdownButton;
        private Label ServerNameLabel;
        private TextBox serverNameBox;
        private Label serverIPLabel;
        private TextBox addressBox;
        private TextBox chatBox;
    }
}