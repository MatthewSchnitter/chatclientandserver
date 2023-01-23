namespace ChatClient
{
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
    /// GUI code for ChatClient
    ///  
    /// </summary>

    using Communications;
    using ChatClientModel;
    using System.Diagnostics;
    using Microsoft.Extensions.Logging;
    using System.Text;
    using System;

    /// <summary>
    /// Form for ChatClient
    /// </summary>
    public partial class ChatClient : Form
    {

        private Networking client;

        private string clientName;

        private bool currentlyConnected;

        private readonly ILogger<ChatClient> logger;

        /// <summary>
        /// Creates a new instance of ChatClient
        /// </summary>
        /// <param name="_logger"></param>
        public ChatClient(ILogger<ChatClient> _logger)
        {
            InitializeComponent();
            clientName = "";
            logger = _logger;
            currentlyConnected = false;
            client = new Networking(logger, ReportConnectionEstablished, ReportDisconnect, ReportMessageArrived, '\n');
        }

        /// <summary>
        /// Button for the client to connect to a server. When clicked, the client will check if it
        /// is already connected, and if not connect using the text in the server name text box (default is localhost).
        /// The client will then begin awaiting messages on a new thread. The client will then automatically use the 
        /// name command to tell the server its name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectButton_Click(object sender, EventArgs e)
        {
            // Written immediately when button is clicked
            Invoke(() => { chatTextBox.Text = "Attempting to Connect to Server..." + Environment.NewLine + chatTextBox.Text; });

            if (!currentlyConnected)
            {

                this.clientName = nameTextBox.Text;
                client.ID = clientName;

                try
                {
                    client.Connect(serverNameTextBox.Text, 11000); // Try connecting
                    Thread clientMessageThread = new Thread(() => client.ClientAwaitMessagesAsync()); // Begin awaiting messages
                    clientMessageThread.Start(); 
                    client.tcpClient.GetStream().Write(Encoding.UTF8.GetBytes("Command Name " + clientName + "\n")); // Send name command to tell server the client's name
                    currentlyConnected = true;
                    Invoke(() => { chatTextBox.Text = "Connected To Server! :)" + Environment.NewLine + chatTextBox.Text; }); // Printed on GUI is successfully connected

                }
                catch
                {
                    // If failure to connect, print message to GUI
                    Invoke(() => { chatTextBox.Text = "Couldn't Connect to Server" + Environment.NewLine + chatTextBox.Text; });
                }
            } else
            {
                // If already connected, print message to GUI
                Invoke(() => { chatTextBox.Text = "Already Connected to a Server" + Environment.NewLine + chatTextBox.Text; });
            }
        }

        /// <summary>
        /// Client's disconnect callback
        /// Set currently connected to false, and disposes of the client.
        /// </summary>
        /// <param name="channel"> the client </param>
        public void ReportDisconnect(Networking channel)
        {

            currentlyConnected = false;
            client.tcpClient.Dispose();

            Invoke(() => { chatTextBox.Text = "Lost Connection To Server :(" + Environment.NewLine + chatTextBox.Text; });

        }

        /// <summary>
        /// Connection callback for client
        /// Set the client to the connected Networking object and set name
        /// </summary>
        /// <param name="channel"> the client </param>
        public void ReportConnectionEstablished(Networking channel)
        {

            client = channel;
            channel.ID = clientName;
            Debug.WriteLine(channel);

        }

        /// <summary>
        /// Message callback for clients. First checks if the message is a command. 
        /// If the command is a paticipants command, the client will get the names of participants from the command,
        /// and prints their names in the participants box.
        /// If it is a disconnect command, calls disconnect on the client. 
        /// If it is a normal message, prints the message to the chat box. 
        /// </summary>
        /// <param name="channel"> the client </param>
        /// <param name="text"> the message </param>
        public void ReportMessageArrived(Networking channel, string text)
        {

            if (ChatClientModel.IsParticipantsCommand(text)) // Check for participants command
            {
                // Get the names
                List<string> names = ChatClientModel.GetClientNamesFromCommand(text);

                Invoke(() => { participantsTextBox.Text = (""); }); // Clear the names from the text box

                foreach (string name in names)
                {
                    Invoke(() => { participantsTextBox.AppendText(name + Environment.NewLine); }); // Print the new lisr of names in the text box
                }

            } else if (text == "Command Disconnect\n")
            {
                client.Disconnect();
            } 
            else
            {
                Invoke(() => { chatTextBox.Text = text + Environment.NewLine + chatTextBox.Text; }); // Print message
            }    
            
        }

        /// <summary>
        /// Used when sending a message. Clicking enter or return will send the text in the chatTextBox
        /// Will report if the client is not connected or has disconnected by printing a message in the GUI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void typingTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (client.tcpClient == null) // If not connected print a message saying so
            {
                Invoke(() => { chatTextBox.Text = "Not connected to server" + Environment.NewLine + chatTextBox.Text; });
            }
            else
            {

                try
                {
                    if (e.KeyChar == (char)Keys.Return) // If the user hits enter, the text in the chat box is written to the tcpClient's stream
                    {
                        client.tcpClient.GetStream().Write(Encoding.UTF8.GetBytes(typingTextBox.Text + "\n")); // Append the termination char so the server will know when the message ends
                        Invoke(() => { typingTextBox.Text = ""; }); // Remove the text once sent
                    }
                }
                catch (Exception)
                {
                    // If the tcpClient exists but cannot write, it has disconnected
                    Invoke(() => { chatTextBox.Text = "Disconnected from server :(" + Environment.NewLine + chatTextBox.Text; });
                }

            }

        }

        /// <summary>
        /// Handler for retrieve participants button. If the client exists, write the participants
        /// command to its stream. This will let the server know to return a list of clients names 
        /// to be printed. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void retrieveParticipantsButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (client.tcpClient != null)
                {
                    // Write command to stream to get participants 
                    client.tcpClient.GetStream().Write(Encoding.UTF8.GetBytes("Command Participants\n"));
                } else
                {
                    // Throw exception if not connected
                    throw new Exception("Can't retrieve participants. Not connected to server!");
                }
                
            } catch (Exception exception)
            {
                // Print a message in the participants box saying not connected.
                Invoke(() => { participantsTextBox.Text = (""); });
                Invoke(() => { participantsTextBox.Text = "Not Connected To Server"; });
            }

            

        }

        /// <summary>
        /// Disconnects the client when the form is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            client.Disconnect();
        }
    }
}