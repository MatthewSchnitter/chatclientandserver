using Communications;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;
using static Communications.Networking;
using FileLogger;
using System.Text.RegularExpressions;

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
/// Contains commands method used by ChatClient
///  
/// </summary>

namespace ChatClientModel
{
    /// <summary>
    /// Contains commands method used by ChatClient
    /// </summary>
    public class ChatClientModel
    {
        private Networking clientNetwork;
        private ILogger logger;

        /// <summary>
        /// Retrieves the names of all participants
        /// </summary>
        /// <param name="command"> Command to retrieve names from </param>
        /// <returns></returns>
        public static List<string> GetClientNamesFromCommand(string command)
        {
            List<string> names = new List<string>();
            string[] namesArray = command.Split(", "); // Split the names into an string array


            for (int i = 1; i < namesArray.Length; i++)
            {
                names.Add(namesArray[i]); // Add strings to the list of names
            }

            return names;

        }
 
        /// <summary>
        /// Checks if a command should retrieve participants
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static bool IsParticipantsCommand(string command)
        {
            string cmdPattern = @"Command Participants, .*";
            Regex cmdRegex = new Regex(cmdPattern); // RegEx to set the pattern the command can follow

            if (cmdRegex.IsMatch(command))
            {
                return true;
            }

            return false;
        }

    }
}