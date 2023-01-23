namespace ChatServer
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
    /// GUI code for the ChatServer 
    ///  
    /// </summary>

    using ChatServerModel;
    using Communications;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using static Communications.Networking;

    /// <summary>
    /// Form for the ChatServer
    /// </summary>
    public partial class ChatServer : Form
    {

        Networking connection;

        private List<TcpClient> clients;
        private Dictionary<TcpClient, string> clientNames;
        private List<Networking> clientNetworks;
        private readonly ILogger<ChatServer> logger;

        /// <summary>
        /// Creates a new instance of ChatServer
        /// </summary>
        /// <param name="_logger"> logger for info </param>
        public ChatServer(ILogger<ChatServer> _logger)
        {
            InitializeComponent();
            clients = new List<TcpClient>();
            clientNames = new Dictionary<TcpClient, string>();
            clientNetworks = new List<Networking>();
            logger = _logger;

            //Server's newtorking object which will be used for callbacks
            connection = new Networking(logger, ReportConnectionEstablished, ReportDisconnect, ReportMessageArrived, '\n');

            serverNameBox.Text = Dns.GetHostName();

            IPHostEntry ip = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] ipAddress = ip.AddressList;

            addressBox.AppendText(ipAddress[ipAddress.Length - 1].ToString());

            connection.WaitForClients(11000, true); // Begin waiting for client's to connect

        }

        /// <summary>
        /// Server's disconnect callback. Removes tcpClients from the list, as well as 
        /// client's names and netwroking objects from their repsective lists. 
        /// </summary>
        /// <param name="channel"> the server </param>
        public void ReportDisconnect(Networking channel)
        {

            clients.Remove(channel.tcpClient);
            clientNames.Remove(channel.tcpClient);
            clientNetworks.Remove(channel);

            // Print the names of the current clients
            Invoke(() => { participantsBox.Text = ChatServerModel.CreateParticipantsString(clientNames); });

        }

        /// <summary>
        /// Server's connection callback. Adds the client's tcpClient, name, and Networking objects
        /// to their repsective lists.
        /// </summary>
        /// <param name="channel"> the server </param>
        public void ReportConnectionEstablished(Networking channel)
        {

            clients.Add(channel.tcpClient);
            string clientName;
            clientNames.TryGetValue(channel.tcpClient, out clientName);
            clientNetworks.Add(channel);

        }

        /// <summary>
        /// Message sent callback for server. First checks if the message is a command. 
        /// If the command is a paticipants command, the server will send the names of all current 
        /// participants to each client. If it is a name command the server updates its names list and 
        /// its participants box. If it is a normal message, the server sends the message to all clients 
        /// with the sending client's name in front of the message, while writing the message to the server's
        /// chat window. Also logs the message. 
        /// </summary>
        /// <param name="channel"> the server </param>
        /// <param name="text"> the message </param>
        public void ReportMessageArrived(Networking channel, string text)
        {

            if (ChatServerModel.IsCommand(text))
            {
                if (ChatServerModel.ParticipantsCommand(text))
                {
                    // Send the particiapants names to clients
                    connection.Send(ChatServerModel.GetParticipantsCommandString(clientNames));
                }
                else if (ChatServerModel.NameCommand(text, out string clientName))
                {
                    // Update list and change names 
                    if (clientNames.ContainsKey(channel.tcpClient))
                    {
                        clientNames.Remove(channel.tcpClient);
                    }

                    clientNames.Add(channel.tcpClient, clientName.Trim());

                    // Update participants names in text box
                    Invoke(() => { participantsBox.Text = ChatServerModel.CreateParticipantsString(clientNames); });

                }
            } 
            else
            {

                string clientName;
                clientNames.TryGetValue(channel.tcpClient, out clientName);

                string message = $"{clientName} - {text}";

                connection.Send(message);
                logger.LogInformation(message); // Logs the message using FileLogger
                Invoke(() => { chatBox.AppendText(message + Environment.NewLine); }); // Update chat box window
            } 
        }

        /// <summary>
        /// Shuts down the server and clears clients lists. 
        /// Sends a command to clients to tell them to disconnect 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shutdownButton_Click(object sender, EventArgs e)
        {
            connection.StopWaitingForClients(); // Stop the network listener
            
            foreach (Networking client in clientNetworks)
            {
                connection.Send("Command Disconnect\n"); // Command for clients to disconnect
            }

            clients = new List<TcpClient>();
            clientNames = new Dictionary<TcpClient, string>();
            clientNetworks = new List<Networking>();
        }
    }
}