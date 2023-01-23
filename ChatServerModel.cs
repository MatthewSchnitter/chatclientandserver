using Communications;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;
using static Communications.Networking;
using FileLogger;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;

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
/// Contains commands method used by ChatServer
///  
/// </summary>

namespace ChatServerModel
{
    /// <summary>
    /// Contains commands method used by ChatServer
    /// </summary>
    public class ChatServerModel
    {

        /// <summary>
        ///   Nothing to "construct" at this time
        /// </summary>
        public ChatServerModel()
        {

        }

        /// <summary>
        /// Checks if a message is a command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static bool IsCommand(string command)
        {
            string cmdPattern = @"Command .*";
            Regex cmdRegex = new Regex(cmdPattern); // Use RegEx for pattern

            if (cmdRegex.IsMatch(command))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if a command is the participants command
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static bool ParticipantsCommand(string command)
        {
            if (command == "Command Participants\n")
            {
                return true;
            }

            return false;

        }

        /// <summary>
        /// Retrieve the name from the name command
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"> name from command </param>
        /// <returns></returns>
        public static bool NameCommand(string command, out string name)
        {

            string cmdPattern = @"Command Name .*";
            Regex cmdRegex = new Regex(cmdPattern); // Use RegEx for pattern

            if (cmdRegex.IsMatch(command))
            {

                name = command.Remove(0, 12); // Set name to the name given after the command
                return true;
            }

            name = ""; // If it is not a name command, do not set name

            return false;
        }

        /// <summary>
        /// Returns the string of all participants
        /// </summary>
        /// <param name="names"> The set of tcpClients and their respective names </param>
        /// <returns> the string of names </returns>
        public static string GetParticipantsCommandString(Dictionary<TcpClient, string> names)
        {
            StringBuilder participantsCommand = new StringBuilder();
            participantsCommand.Append("Command Participants");

            foreach (KeyValuePair<TcpClient, string> name in names)
            {
                participantsCommand.Append(", " + name.Value); // Get the name for the tcpClient and append
            }

            participantsCommand.Append("\n");

            return participantsCommand.ToString();
        }

        /// <summary>
        /// Creates the string that will be displayed in the particpants box for the server.
        /// Gets the list of names from the input parameter and creates a string using the client
        /// name and IP address.
        /// </summary>
        /// <param name="participants"> client names </param>
        /// <returns> a string with the name and IP address </returns>
        public static string CreateParticipantsString(Dictionary<TcpClient, string> participants)
        {

            StringBuilder participantsString = new StringBuilder();

            foreach(KeyValuePair<TcpClient, string> participant in participants)
            {

                // Get name and append client's IP address
                string nameAndIP = participant.Value + ": " + (IPEndPoint)participant.Key.Client.RemoteEndPoint;

                participantsString.Append(nameAndIP + Environment.NewLine);

            }

            return participantsString.ToString();

        }

    }
}