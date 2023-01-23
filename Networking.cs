using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
/// Contains the newtorking and communications functionality of the client and server. 
///  
/// </summary>

namespace Communications
{
    /// <summary>
    /// Networking handles connections and message sending between the client and server.
    /// </summary>
    public class Networking

    {
        public string ID { set; get; }
        private char terminationChar;
        public TcpClient tcpClient;
        private TcpListener network_listener;
        private List<TcpClient> clients;
        private CancellationTokenSource _WaitForCancellation;
        private ILogger _logger;

        public delegate void ReportMessageArrived(Networking channel, string message);
        public delegate void ReportDisconnect(Networking channel);
        public delegate void ReportConnectionEstablished(Networking channel);

        ReportMessageArrived onMessageCallback;
        ReportConnectionEstablished onConnectCallback;
        ReportDisconnect onDisconectCallback;

        /// <summary>
        /// Creates a new networking object to be used by the client or server.
        /// </summary>
        /// <param name="logger"> Logger necessary to log files </param>
        /// <param name="onConnect"> Callback for an established connection </param>
        /// <param name="onDisconnect"> Callback for a disconnect </param>
        /// <param name="onMessage"> Callback for a recieved message </param>
        /// <param name="terminationCharacter"> Character to end a message with </param>
        public Networking(ILogger logger, ReportConnectionEstablished onConnect, ReportDisconnect onDisconnect, ReportMessageArrived onMessage,
            char terminationCharacter)
        {
            ID = "RemoteEndPoint";
            terminationChar = terminationCharacter;
            onConnectCallback = onConnect;
            onMessageCallback = onMessage;
            onDisconectCallback = onDisconnect;
            _logger = logger;
            clients = new List<TcpClient>();
        }

        /// <summary>
        /// Connects a tcpClient to the server by creating a new tcpClient using the given host
        /// and port.
        /// </summary>
        /// <param name="host"> IP address of the host </param>
        /// <param name="port"> Port server is listening on</param>
        /// <exception cref="Exception"> Exception thrown if failure to connect</exception>
        public void Connect(string host, int port)
        {
            try
            {
                // Creating the tcpClient will establish the connection
                tcpClient = new TcpClient(host, port);

                if (tcpClient.Connected)
                {
                    onConnectCallback(this); // If connection is established, call the onConnect callback
                    Console.WriteLine($"Connected to {tcpClient.Client.RemoteEndPoint}");
                    Console.WriteLine($"Awaiting Data...");
                }
                else
                {
                    Console.WriteLine($"Not Connected. Terminating Program.");
                    Environment.Exit(0);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failure to connect");
            }
        }

        /// <summary>
        /// Used by both the server and clients to listen for messages. If the client writes something
        /// to its stream, the server will decode the message, and use the OnMessage callback to handle it.
        /// If the something is written to a client's stream by the server, the client calls its OnMessage 
        /// callback ot handle the message.
        /// </summary>
        /// <param name="infinite"> Always be waiting for messages </param>
        public async void ClientAwaitMessagesAsync(bool infinite = true)
        {
            try
            {
                StringBuilder dataBacklog = new StringBuilder(); // Backlog for the message
                byte[] buffer = new byte[4096];
                NetworkStream stream = tcpClient.GetStream();

                if (stream == null) return;

                while (true)
                {
                    int total = await stream.ReadAsync(buffer, 0, buffer.Length); // Waits for a stream and reads its contents

                    string current_data = Encoding.UTF8.GetString(buffer, 0, total); // Converts the stream to a string

                    dataBacklog.Append(current_data);

                    string message;

                    if (CheckForMessage(dataBacklog, out message)) // Check if the message is valid. If not, do nothing
                    {
                        onMessageCallback(this, message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"oops{ex}");
            }
        }

        /// <summary>
        /// Called by the server on startup to begin waiting for clients. If a client connects,
        /// use the onConnect callback
        /// </summary>
        /// <param name="port"> Port for server to listen on </param>
        /// <param name="infinite"></param>
        public async void WaitForClients(int port, bool infinite)
        {
            try
            {

                _WaitForCancellation = new();
                network_listener = new TcpListener(IPAddress.Any, port); // Create a listener to lisen for connections

                while (true)
                {
                    network_listener.Start();

                    // Wait for a tcpClient to connect and set it to a variable connection for the server to store
                    TcpClient connection = await network_listener.AcceptTcpClientAsync(_WaitForCancellation.Token);

                    lock (this.clients)
                    {
                        this.clients.Add(connection);
                    }

                    // Create a new networking object using the connected tcpClient
                    Networking newNetworkingObject = new Networking(_logger, onConnectCallback, onDisconectCallback, onMessageCallback, terminationChar);
                    newNetworkingObject.tcpClient = connection;

                    // Have that client wait for messages on a new thread
                    Thread clientThread = new Thread(() => newNetworkingObject.ClientAwaitMessagesAsync());

                    clientThread.Start();

                    onConnectCallback(newNetworkingObject);

                }

            }
            catch (Exception e)
            {
                network_listener.Stop();
            }
        }

        /// <summary>
        /// Method to call when server disconnects
        /// </summary>
        public void StopWaitingForClients()
        {
            _WaitForCancellation.Cancel();
        }

        /// <summary>
        /// Disconnects the client
        /// </summary>
        public void Disconnect()
        {

            onDisconectCallback(this);

        }

        /// <summary>
        /// Used by server to write a message to every stored client's stream. 
        /// Doing so will allow the client's AwaitMessage's async method to handle the sent message
        /// via the callback.
        /// </summary>
        /// <param name="text"> Message to send </param>
        public async void Send(string text)
        {
            // Temporary lists to determine what clients to send to
            List<TcpClient> toRemove = new();
            List<TcpClient> toSendTo = new();

            //while (true)
            //{
            string message = text;

            //
            // Encode and Send the message
            //
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            //
            // Cannot have clients adding while we send messages, so make a copy of the
            // current list of clients.
            //
            lock (clients)
            {
                foreach (var client in clients)
                {
                    toSendTo.Add(client);
                }
            }

            // Iterate over "saved" list of clients
            //
            // Question: Why can't we lock clients around this loop?
            //
            Console.WriteLine($"  Sending a message of size ({message.Length}) to {toSendTo.Count} clients");

            foreach (TcpClient client in toSendTo)
            {
                try
                {
                    // Writes the message to each clients stream
                    await client.GetStream().WriteAsync(messageBytes, 0, messageBytes.Length);
                    Console.WriteLine($"    Message Sent from:   {client.Client.LocalEndPoint} to {client.Client.RemoteEndPoint}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"    Client Disconnected: {client.Client.RemoteEndPoint} - {ex.Message}");
                    toRemove.Add(client);
                }
            }

            lock (clients)
            {
                // update list of "current" clients by removing closed clients
                foreach (TcpClient client in toRemove)
                {
                    clients.Remove(client);
                    Networking newNetworkingObject = new Networking(_logger, onConnectCallback, onDisconectCallback, onMessageCallback, terminationChar);
                    newNetworkingObject.tcpClient = client;

                    onDisconectCallback(newNetworkingObject);
                }
            }

            toSendTo.Clear();
            toRemove.Clear();
            //}
        }

        /// <summary>
        ///   Given a string (actually a string builder object)
        ///   check to see if it contains one or more messages as defined by
        ///   our protocol (the period '\n').
        /// </summary>
        /// <param name="data"> all characters encountered so far</param>
        private bool CheckForMessage(StringBuilder data, out string message)
        {
            string allData = data.ToString();
            // Finds the termination character
            int terminator_position = allData.IndexOf(terminationChar);
            bool foundOneMessage = false;
            message = data.ToString();

            while (terminator_position >= 0)
            {
                foundOneMessage = true;

                // Trim the message so it does not contain the termination character
                message = allData.Substring(0, terminator_position + 1);
                data.Remove(0, terminator_position + 1);

                allData = data.ToString();
                // Finds the termination character
                terminator_position = allData.IndexOf(terminationChar);
            }

            if (!foundOneMessage)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Handles sockets and message arrays for encoding/decoding
        /// </summary>
        private class SocketState
        {
            public Socket theSocket;
            public byte[] messageBuffer;
            public StringBuilder sb;

            /// <summary>
            /// Creates a new socket state with the given socket and a message buffer and string builder
            /// </summary>
            /// <param name="s"></param>
            public SocketState(Socket s)
            {
                theSocket = s;
                messageBuffer = new byte[1024];
                sb = new StringBuilder();
            }
        }

    }
}
