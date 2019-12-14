using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace server_deneme
{
    public partial class Form1 : Form
    {
        struct Triple
        {
            public string invitee;
            public string inviter;
            public string message;
            public string friend_message; // yeni ekledim
        }

        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        List<string> dbSet = new List<string>(); //we used hashset
        HashSet<string> currentUsers = new HashSet<string>();
        Dictionary<string, Socket> name_socket = new Dictionary<string, Socket>();
        Dictionary<string, List<string>> friend_list = new Dictionary<string, List<string>>();
        List<Triple> friend_req_list = new List<Triple>();



        string dis_name = "client";
        bool terminating = false; //check for terminated or not
        bool listening = false; //before click to listen it is false
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void sendMessageToClients(string name, string message)
        {

        }

        private void button_listen_Click(object sender, EventArgs e)
        {
            
            string line;
            StreamReader database = new StreamReader("user_db.txt");
            

            while((line = database.ReadLine()) != null)
            {
                dbSet.Add(line);
            }
            int serverPort;
        
            if (Int32.TryParse(textBox_port.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;  //after click to listen it return true
                button_listen.Enabled = false; // user cannot click again

                

                for (int i = 0; i < dbSet.Count; i++)
                {
                    List<string> empty = new List<string>();
                    if (!friend_list.ContainsKey(dbSet[i]))
                    {
                        //empty.Add(dbSet[i]);
                        friend_list.Add(dbSet[i], empty);
                    }
                }

                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                Thread checkThread = new Thread(Check);
                checkThread.Start();

                logs.AppendText("Started listening on port: " + serverPort + "\n"); //successfull port number

            }
            else
            {
                logs.AppendText("Please check port number \n"); //Port number can be wrong
            }
        }

        private void Check()
        {
            while (listening)
            {
                Thread.Sleep(500);
                for (int i = 0; i < friend_req_list.Count; i++)
                {
                    Thread.Sleep(500);
                    if (friend_req_list[i].message == "wait_s")
                    {
                        if (currentUsers.Contains(friend_req_list[i].invitee))
                        {
                            Byte[] buffer = new Byte[128];
                            buffer = Encoding.Default.GetBytes("EfeYarenYigitKaya£" + friend_req_list[i].inviter);
                            logs.AppendText(friend_req_list[i].inviter + " requested friendship to " + friend_req_list[i].invitee+"\n");
                            //logs.AppendText(friend_req_list[i].inviter + "  " + deneme.ToString());
                            try
                            {
                                name_socket[friend_req_list[i].invitee].Send(buffer);
                                //logs.AppendText("Sent\n");
                                Triple new_node;
                                new_node.invitee = friend_req_list[i].invitee;
                                new_node.inviter = friend_req_list[i].inviter;
                                new_node.message = "wait_a";
                                new_node.friend_message = "-";
                                friend_req_list[i] = new_node;
                                
                            }
                            catch
                            {
                                logs.AppendText("couldn't sent1");
                            }
                            
                        }
                    }
                    else if (friend_req_list[i].message == "wait_a")
                    {
                        if (!currentUsers.Contains(friend_req_list[i].invitee))
                        {
                                Triple new_node;
                                new_node.invitee = friend_req_list[i].invitee;
                                new_node.inviter = friend_req_list[i].inviter;
                                new_node.message = "wait_s";
                                new_node.friend_message = "-";
                                friend_req_list[i] = new_node;
                        }
                    }
                    else if (friend_req_list[i].message == "accepted")
                    {
                        if (currentUsers.Contains(friend_req_list[i].inviter))
                        {
                            Byte[] buffer = new Byte[128];
                            buffer = Encoding.Default.GetBytes("EfeYarenYigitKaya#" + friend_req_list[i].invitee);
                            logs.AppendText(friend_req_list[i].invitee + " accepted the friend request of " + friend_req_list[i].inviter+"\n");
                            //logs.AppendText(friend_req_list[i].inviter + "  " + deneme.ToString());
                            try
                            {

                                name_socket[friend_req_list[i].inviter].Send(buffer);
                                string inviter = friend_req_list[i].inviter;
                                string invitee = friend_req_list[i].invitee;
                                Triple new_node;
                                new_node.invitee = friend_req_list[i].invitee;
                                new_node.inviter = friend_req_list[i].inviter;
                                new_node.message = "accepted";
                                new_node.friend_message = "-"; // bunları yeni ekledim
                                friend_req_list.Remove(new_node);
                                List<string> intr = friend_list[inviter];
                                intr.Add(invitee);
                                friend_list[inviter] = intr;
                                List<string> inte = friend_list[invitee];
                                inte.Add(inviter);
                                friend_list[invitee] = inte;
                                //friend_list["Li Li"].Add("Fatih Terim");
                                //friend_list["Fatih Terim"].Add("Li Li");
                                //logs.AppendText("Inviter: " + inviter + " invitee: " + invitee + "\n");
                                foreach(KeyValuePair<string,List<string>> a in friend_list)
                                {
                                    //logs.AppendText(a.Key + "...\n");
                                    foreach(string b in a.Value)
                                    {
                                        //logs.AppendText(b + "!!!!!!");
                                    }
                                }
                            }
                            catch(Exception e)
                            {
                                //logs.AppendText(e.Message);
                                //logs.AppendText("couldn't sent");
                            }

                        }
                    }
                    else if (friend_req_list[i].message == "rejected")
                    {
                        if (currentUsers.Contains(friend_req_list[i].inviter))
                        {
                            Byte[] buffer = new Byte[128];
                            buffer = Encoding.Default.GetBytes("EfeYarenYigitKaya*&" + friend_req_list[i].invitee+ " rejected you.");
                            logs.AppendText(friend_req_list[i].invitee + " rejected the friend request of " + friend_req_list[i].inviter+"\n");
                            //logs.AppendText(friend_req_list[i].inviter + "  " + deneme.ToString());
                            try
                            {
                                name_socket[friend_req_list[i].inviter].Send(buffer);
                                Triple new_node;
                                new_node.invitee = friend_req_list[i].invitee;
                                new_node.inviter = friend_req_list[i].inviter;
                                new_node.message = "rejected";
                                new_node.friend_message = "-";
                                friend_req_list.Remove(new_node);
                            }
                            catch
                            {
                                logs.AppendText("couldn't sent33333");
                            }

                        }
                    }
                    else if(friend_req_list[i].friend_message == "wait_for_friend")
                    {
                       if(currentUsers.Contains(friend_req_list[i].inviter))
                       {
                            Byte[] buffer = new Byte[128];
                            buffer = Encoding.Default.GetBytes("EfeYarenYigitKaya*!" + friend_req_list[i].inviter + "%" + friend_req_list[i].message);
                            try
                            {
                                name_socket[friend_req_list[i].invitee].Send(buffer);
                                Triple new_node;
                                new_node.invitee = friend_req_list[i].invitee;
                                new_node.inviter = friend_req_list[i].inviter;
                                new_node.message = friend_req_list[i].message;
                                new_node.friend_message = "wait_for_friend";
                                friend_req_list.Remove(new_node);
                            }
                            catch
                            {
                                logs.AppendText("couldn't sent to client\n");
                            }
                        }
                    }
                    else if(friend_req_list[i].message == "deleted")
                    {
                        Byte[] buffer = new Byte[128];
                        buffer = Encoding.Default.GetBytes("EfeYarenYigitKaya*<" + friend_req_list[i].inviter); // inform deletedFriend that client removed him from his friend list 
                        if (currentUsers.Contains(friend_req_list[i].invitee))
                        {
                            logs.AppendText(friend_req_list[i].invitee + " deleted " + friend_req_list[i].inviter + " from friends\n");
                            name_socket[friend_req_list[i].invitee].Send(buffer);
                            friend_list[friend_req_list[i].invitee].Remove(friend_req_list[i].inviter);
                            Triple new_node;
                            new_node.invitee = friend_req_list[i].invitee;
                            new_node.inviter = friend_req_list[i].inviter;
                            new_node.message = friend_req_list[i].message;
                            new_node.friend_message = "-";
                            friend_req_list.Remove(new_node);
                        }
                    }
                }
            }
        }

        private void Accept()
        {
            while (listening) //listening should be true for accept
            {
                try
                {
                   
                    Socket newClient = serverSocket.Accept();

                    clientSockets.Add(newClient);
                    

                    Thread receiveThread = new Thread(Receive);
                    receiveThread.Start();
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("The socket stopped working.\n"); 
                    }

                }
            }
        }

        private void Receive()
        {
           
            Socket thisClient = clientSockets[clientSockets.Count() - 1];

            bool connected = false; 
            string message = "";
            string server_message = "";
            
            while (!connected && !terminating)
            {
                try
                {
                    
                    Byte[] buffer = new Byte[128];
                    thisClient.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                 
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    bool isMessage = false, isRequest = false, isAccept = false, isReject = false,friendList = false, friendMessage = false,deleteFriend = false;
                    if (incomingMessage.Substring(incomingMessage.Length - 3, 3) == "???") //??? is for understand that is it client
                    {
                        isMessage = true;
                    }
                    if (incomingMessage.Substring(incomingMessage.Length - 1, 1) == "£") //??? is for understand that is it client
                    {
                        isRequest = true;
                    }
                    if (incomingMessage.Substring(incomingMessage.Length - 1, 1) == "#") //??? is for understand that is it client
                    {
                        isAccept = true;
                    }
                    if (incomingMessage.Substring(incomingMessage.Length - 1, 1) == "&") //??? is for understand that is it client
                    {
                        isReject = true;
                    }
                    if(incomingMessage.Substring(incomingMessage.Length-1,1) == "<") // this means client want to delete his friend
                    {
                        deleteFriend = true;
                    }
                    if (incomingMessage.Substring(incomingMessage.Length - 1, 1) == "$") //??? is for understand that is it client
                    {
                        //logs.AppendText("trying to make friendList true");
                        friendList = true;
                    }
                    if (incomingMessage.Substring(incomingMessage.Length - 3, 3) == "~~~")
                    {
                        dis_name = incomingMessage.Substring(17, incomingMessage.Length - 37); //for incoming msg we should remove ~~~ and EfeYarenYigitKaya
                        currentUsers.Remove(dis_name); //remove same user
                        name_socket.Remove(dis_name);
                        continue;
                    }
                    if(incomingMessage.Substring(incomingMessage.Length - 1, 1) == "!") // newly added (to understand that client send message only to his friends)
                    {
                        friendMessage = true;
                    }
                    if (!deleteFriend && !friendMessage && !friendList && !isAccept && !isReject && !isRequest && !isMessage && incomingMessage.Length >= 34 && incomingMessage.Substring(0, 17) == "EfeYarenYigitKaya" && incomingMessage.Substring(incomingMessage.Length - 17, 17) == "EfeYarenYigitKaya")
                    {
                        
                        string name = incomingMessage.Substring(17, incomingMessage.Length - 34);

                        

                        if (!isMessage)
                        {
                            if (dbSet.Contains(name)) //database contains that username
                            {
                                
                                if (!currentUsers.Contains(name)) //hashset doesnot contain name
                                {
                                    //connected = true;
                                   
                                    currentUsers.Add(name);//add that name
                                    name_socket.Add(name, thisClient);

                                    message = "You successfully logged in";  //successfully log in
                                    server_message = name + " successfully logged in\n";
                                }
                                else
                                {
                                    clientSockets.Remove(thisClient); //hashset contains name so remove same name
                                    message = "You are already logged in";
                                    server_message = name + " is already logged in\n";
                                    thisClient.Shutdown(SocketShutdown.Receive); //we just shutdowned Receive
                                    //thisClient.Close();

                                }

                            }
                            else //database doesnot contains that username
                            {
                                clientSockets.Remove(thisClient);
                                message = "This username does not exist in database";
                                server_message = name + " does not exist in database\n";
                            }
                        }
                       

                        if(message != "" && message.Length <= 128)
                        {
                            logs.AppendText(server_message);
                            message = "EfeYarenYigitKaya" + message + "EfeYarenYigitKaya"; //client should understand server's msg with this word
                            Byte[] bufferConnectionTry = Encoding.Default.GetBytes(message);
                            try
                            {
                                thisClient.Send(bufferConnectionTry);
                            }
                            catch
                            {
                                logs.AppendText("There is a problem! Check the connection...\n");
                                terminating = true;

                                textBox_port.Enabled = true;
                                button_listen.Enabled = true;
                                serverSocket.Close();
                            }
                        }

                    }
                    else if(isMessage)
                    {
                        int index = incomingMessage.IndexOf('*');
                        string clientName = incomingMessage.Substring(17,index - 17);
                        
                        string clientMessage = incomingMessage.Substring(index + 1, incomingMessage.Length - (index + 21)); //parsing the msg of client
                        logs.AppendText(clientName + " send a message.\n");
                        Byte[] bufferConnectionTry2 = Encoding.Default.GetBytes("EfeYarenYigitKaya" + clientName + ": " + clientMessage + "EfeYarenYigitKaya");


                        //hashsetteki clientlar

                        foreach (Socket client in clientSockets)
                        {

                            if (thisClient != client)
                            {
                                try
                                {
                                    client.Send(bufferConnectionTry2);
                                }
                                catch
                                {
                                    logs.AppendText("There is a problem! Check the connection...\n");
                                    terminating = true; //terminated

                                    textBox_port.Enabled = true; //active again
                                    button_listen.Enabled = true; //active again
                                    serverSocket.Close();
                                }
                            }
                        }
                    }
                    else if (friendMessage) // yeni ekledim
                    {
                        //string friend_message = incomingMessage.Substring(17, incomingMessage.Length - (35));
                        int index = incomingMessage.IndexOf('*');
                        string sender = incomingMessage.Substring(0, index);
                        string friend_message = incomingMessage.Substring(index + 1, incomingMessage.Length - (index + 2));
                        List<string> friendNames = friend_list[sender];
                        foreach (string friend in friendNames)
                        {
                            Byte[] buffer8 = new Byte[128];
                            buffer8 = Encoding.Default.GetBytes("EfeYarenYigitKaya*!" + sender + "%" + friend_message);
                            if (currentUsers.Contains(friend))
                            {
                                try
                                {
                                    name_socket[friend].Send(buffer8); // eklemeyi unutma
                                }
                                catch
                                {
                                    logs.AppendText("couldn't sent a message to your friend\n");
                                }
                            }else
                            {
                                Triple new_node;
                                new_node.inviter = sender;
                                new_node.invitee = friend;
                                new_node.message = friend_message;
                                new_node.friend_message = "wait_for_friend";
                                friend_req_list.Add(new_node);
                            }
                           
                        }
                    }
                    else if (deleteFriend)
                    {
                        int index = incomingMessage.IndexOf('*');
                        string clientName = incomingMessage.Substring(0, index);
                        string deleteName = incomingMessage.Substring(index + 1, incomingMessage.Length - (index + 2));
                        List<string> friends = friend_list[clientName];
                        Byte[] buffer13 = new Byte[128];
                        Byte[] buffer14 = new Byte[128];
                        if (friends.Contains(deleteName))
                        {
                            
                            if (currentUsers.Contains(deleteName))
                            {
                                buffer13 = Encoding.Default.GetBytes("EfeYarenYigitKaya*<" + clientName); // inform deletedFriend that client removed him from his friend list
                                name_socket[deleteName].Send(buffer13);
                                friend_list[deleteName].Remove(clientName);  // remove names from both names friend list
                            }else
                            {
                                // add that information to friends request list(keep checking and once deleted friend becomes online inform him)
                                Triple new_node;
                                new_node.inviter = clientName;
                                new_node.invitee = deleteName;
                                new_node.message = "deleted";
                                new_node.friend_message = "-";
                                friend_req_list.Add(new_node);
                            }
                            friends.Remove(deleteName);
                            buffer13 = Encoding.Default.GetBytes("EfeYarenYigitKaya*<s" + deleteName); // inform client that he succesfully deleted deleteName
                            name_socket[clientName].Send(buffer13);
                        }
                        else
                        {
                            buffer14 = Encoding.Default.GetBytes("EfeYarenYigitKaya*<f" + deleteName);
                            name_socket[clientName].Send(buffer14); // send client that there is no such friend
                        }

                    }
                    else if (isRequest)
                    {
                        int index = incomingMessage.IndexOf('*');
                        string inviter = incomingMessage.Substring(0, index);
                        string invitee = incomingMessage.Substring(index + 1, incomingMessage.Length - (index + 2));
                        //logs.AppendText("inviter: " + inviter + "   invitee: " + invitee + "\n");
                        if(friend_list.ContainsKey(inviter) && friend_list[inviter].Contains(invitee))
                        {
                            logs.AppendText(invitee + " and "+ "inviter are already friends");
                            //send client it is already your friend
                            Byte[] buffer1 = new Byte[128];
                            buffer1 = Encoding.Default.GetBytes("EfeYarenYigitKaya*£" + invitee + " is already your friend\n");
                            try
                            {
                                name_socket[inviter].Send(buffer1);
                            }
                            catch
                            {
                                logs.AppendText("couldn't sent\n");
                            }
                        }
                        else
                        {
                            //logs.AppendText("Arkadaş değilse\n");
                            if (dbSet.Contains(invitee))
                            {
                                bool check_req = true;
                                foreach (Triple node in friend_req_list)
                                {
                                    if (node.inviter == inviter && node.invitee == invitee)
                                    {
                                        check_req = false;
                                        //logs.AppendText("Check false");
                                        break;
                                    }
                                }
                                if (check_req)
                                {
                                    //logs.AppendText("Added\n");
                                    Triple new_node;
                                    new_node.inviter = inviter;
                                    new_node.invitee = invitee;
                                    new_node.message = "wait_s";
                                    new_node.friend_message = "-";
                                    friend_req_list.Add(new_node);
                                }
                                else
                                {
                                    logs.AppendText(inviter + " have already sent request to "+invitee+"\n");
                                    Byte[] buffer1 = new Byte[128];
                                    buffer1 = Encoding.Default.GetBytes("123you already sent request and waiting for answer\n");
                                    try
                                    {
                                        name_socket[inviter].Send(buffer1);
                                    }
                                    catch
                                    {
                                        logs.AppendText("couldn't sent");
                                    }
                                }
                            }else
                            {
                                logs.AppendText("You cannot send that request because there is no such user in database\n");
                                Byte[] buffer1 = new Byte[128];
                                buffer1 = Encoding.Default.GetBytes("321There is no such user in the database\n");
                                try
                                {
                                    name_socket[inviter].Send(buffer1);
                                }
                                catch
                                {
                                    logs.AppendText("couldn't sent");
                                }
                            }
                        }
                    }
                    else if (isAccept)
                    {
                        int index = incomingMessage.IndexOf('*');
                        string inviter = incomingMessage.Substring(0, index);
                        string invitee = incomingMessage.Substring(index + 1, incomingMessage.Length - (index + 2));
                        for (int i = 0; i < friend_req_list.Count; i++)
                        {
                            if (friend_req_list[i].invitee == invitee && friend_req_list[i].inviter == inviter && friend_req_list[i].message == "wait_a")
                            {
                                Triple new_node;
                                new_node.inviter = inviter;
                                new_node.invitee = invitee;
                                new_node.message = "accepted";
                                new_node.friend_message = "-";
                                friend_req_list[i] = new_node;
                                break;
                            }
                        }
                    }
                    else if (isReject)
                    {
                        //logs.AppendText("Girdim reject\n");
                        int index = incomingMessage.IndexOf('*');
                        string inviter = incomingMessage.Substring(0, index);
                        string invitee = incomingMessage.Substring(index + 1, incomingMessage.Length - (index + 2));

                        for (int i = 0; i < friend_req_list.Count; i++)
                        {
                            if (friend_req_list[i].invitee == invitee && friend_req_list[i].inviter == inviter && friend_req_list[i].message == "wait_a")
                            {
                                Triple new_node;
                                new_node.inviter = inviter;
                                new_node.invitee = invitee;
                                new_node.message = "rejected";
                                new_node.friend_message = "-";
                                friend_req_list[i] = new_node;
                                break;
                            }
                        }
                    }
                    else if(friendList)
                    {                
                        string name = incomingMessage.Substring(0, incomingMessage.Length - 1);
                        //logs.AppendText("I am trying to find the friends of" + name);
                        string friends = "";
                        List<string> friend_names = friend_list[name];
                        foreach(string friend in friend_names)
                        {
                            friends += friend + ",";
                        }
                        logs.AppendText(name + " would like to see his/her friend list\n");
                        friends = "EfeYarenYigitKaya$" + friends;
                        Byte[] buffer5 = new Byte[128];
                        buffer5 = Encoding.Default.GetBytes(friends);
                        try
                        {
                            name_socket[name].Send(buffer5);
                        }
                        catch
                        {
                            logs.AppendText("couldn't sent the friend list to client");
                        }
                    }
                }
                catch(Exception e)
                {
                    //logs.AppendText(e.Message);
                    
                    if (!terminating) //not terminated so it will be disconnect
                    {
                        logs.AppendText(dis_name + " has disconnected\n");
                    }
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    connected = true;
                }
            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (Socket client in clientSockets)
            {
                client.Shutdown(SocketShutdown.Both); //in closing, Receive and Accept should be shutdown
            }
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_send_Click(object sender, EventArgs e)
        {
    
        }

        private void textBox_port_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_message_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
