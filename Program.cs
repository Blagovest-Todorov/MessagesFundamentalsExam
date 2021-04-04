using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Test
{
    
    class Program
    {
        static void Main(string[] args)
        {
            
            int countCapacity = int.Parse(Console.ReadLine());
            Dictionary<string, int> messagesByUser = new Dictionary<string, int>();
            Dictionary<string, int> messSentByUser = new Dictionary<string, int>();

            while (true)
            {
                string line = Console.ReadLine();

                if (line == "Statistics")
                {
                    //ToDo
                    break;
                }

                string[] parts = line.Split("=", StringSplitOptions.RemoveEmptyEntries);
                string comand = parts[0];

                if (comand == "Add")
                { // Add=username=sent=received

                    string userName = parts[1];
                    int messageSent = int.Parse(parts[2]);
                    int messageReceived = int.Parse(parts[3]);

                    if (messagesByUser.ContainsKey(userName))
                    {
                        continue;
                    }

                    messagesByUser.Add(userName, messageSent);
                    messagesByUser[userName] += messageReceived;
                    messSentByUser.Add(userName, messageSent);
                }
                else if (comand == "Message")
                { // Message=sender=receiver
                    string userSent = parts[1];
                    string userReceived = parts[2];

                    if (messagesByUser.ContainsKey(userSent) && messagesByUser.ContainsKey(userReceived))
                    {
                        messagesByUser[userSent] += 1;
                        messagesByUser[userReceived] += 1;
                        messSentByUser[userSent] += 1;

                        if (countCapacity <= messagesByUser[userSent])
                            
                           // countCapacity == messagesByUser[userReceived])
                        {
                            Console.WriteLine($"{userSent} reached the capacity!");
                            messagesByUser.Remove(userSent);
                            messSentByUser.Remove(userSent);
                        }

                        if (countCapacity <= messagesByUser[userReceived])
                        {
                            Console.WriteLine($"{userReceived} reached the capacity!");
                            messagesByUser.Remove(userReceived);
                            messSentByUser.Remove(userReceived);
                        }
                    }
                }
                else if (comand == "Empty")
                {
                    string user = parts[1];

                    if (messagesByUser.ContainsKey(user))
                    {
                        messagesByUser.Remove(user);
                        messSentByUser.Remove(user);
                    }

                    if (user == "All")
                    {
                        messagesByUser.Clear();
                        messSentByUser.Clear();
                    }
                }
            }

            Console.WriteLine($"Users count: {messagesByUser.Count}");

            Dictionary<string, int> sorted = messSentByUser
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .ToDictionary(x => x.Key,  x => x.Value);

            foreach (var kvp in sorted)
            {
                string user = kvp.Key;
                Console.WriteLine($"{kvp.Key} - {messagesByUser[user]}");
            }
        }
    }
}
