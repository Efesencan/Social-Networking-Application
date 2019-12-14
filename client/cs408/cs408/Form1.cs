using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cs408
{
    public partial class 
        Form1 : Form
    {
        string userName;
        string incomingName;
        bool terminating = false; //check for terminated
        bool connected = false; //check for connection
        bool server_check = true;
        List<string> requestList = new List<string>();
        Socket clientSocket;
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }
        private void connect_button_Click(object sender, EventArgs e) 
        {          
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBox_ip.Text;
            int portNum;
            if (IP != "" && Int32.TryParse(textBox_port.Text, out portNum))
            {
                try
                {
                    clientSocket.Connect(IP, portNum);
                    connected = true; // connect button activated
                    incomingName = ("EfeYarenYigitKaya" + textBox_name.Text +  "EfeYarenYigitKaya"); //message should come to server so
                                                                                                     //understand to this, we use specific word EfeYarenYigitKaya
                                                                                                     //before and after the message
                    
                    if (incomingName != "" && incomingName.Length <= 128) //incoming message's length should be
                    {
                        Byte[] buffer = new Byte[128];
                        buffer = Encoding.Default.GetBytes(incomingName);
                        //for(int i = 0; i < 100000; i++) { }
                        clientSocket.Send(buffer);
                        //logs.AppendText("Message send to server");
                    }
                    Thread receiveThread = new Thread(Receive); 
                    receiveThread.Start(); 

                }
                catch
                {
                    logs.AppendText("Could not connect to the server!\n"); //message doesn't come to the server
                }
            }
            else
            {
                if(IP == "")
                {
                    logs.AppendText("Check the IP\n");
                }else
                {
                    logs.AppendText("Check the port\n"); //can be wrong port number
                }
                
            }
        }
        private void Receive()
        {
            bool already_check = true;
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[128];
                    clientSocket.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    //logs.AppendText("Message received" + "\n");
                    //logs.AppendText("Server: " + incomingMessage + "\n");
                    if(incomingMessage == "EfeYarenYigitKayaYou successfully logged inEfeYarenYigitKaya")
                    {
                        textBox_message.Enabled = true; //after successfully connection, textbox button should be active.
                        send_button.Enabled = true; //after connection, send button should be active.
                        logs.AppendText("Connected to the server!\n"); 
                        disconnect_button.Enabled = true; //after connected disconnect button should be active.
                        userName = incomingName;
                        connect_button.Enabled = false; //already connected so connect button shoul be inactive
                        AcceptBut.Enabled = true;
                        RejectBut.Enabled = true;
                        friendsBut.Enabled = true;
                        sendReqBut.Enabled = true;
                        button1.Enabled = true;
                        deleteButton.Enabled = true;

                    }
                    else if(incomingMessage == "EfeYarenYigitKayaThis username does not exist in databaseEfeYarenYigitKaya")//server sends does not exist message
                    {
                        logs.AppendText("You are not in the database\n"); //if this message came from the server and username was wrong, print this
                    }
                    else if(incomingMessage == "EfeYarenYigitKayaYou are already logged inEfeYarenYigitKaya")
                    {
                        logs.AppendText("You already logged in\n"); //correct username but it already connected
                        already_check = false; //for blocked to another connection from same user 

                    }
                    else if(incomingMessage.Length > 18 && incomingMessage.Substring(0, 18) == "EfeYarenYigitKaya£")
                    {
                        string inviter = incomingMessage.Substring(18, incomingMessage.Length - 18);
                        if (!requestList.Contains(inviter))
                            requestList.Add(inviter); // yeni ekledim whenever you receive request that does not exist in your request list add it to your list
                        friendReqList.Clear();
                        //requestList.Add(inviter);
                        //bool flag = true;
                        string currentRequest = "";
                        foreach (string name in requestList)
                        {
                            currentRequest += name + "\n";
                        }
                        friendReqList.AppendText(currentRequest);
                        //    if(name == inviter)
                        //    {
                        //        flag = false;
                        //    }
                        //}
                        //if (flag)
                        //{
                        //    requestList.Add(inviter);
                        //    currentRequest += inviter;
                        //}
                        //friendReqList.AppendText(currentRequest + "\n");
                        //friendReqList.AppendText(inviter + "\n");
                        
                    }
                    else if (incomingMessage.Length > 18 && incomingMessage.Substring(0, 19) == "EfeYarenYigitKaya*£")
                    {
                        string myMessage = incomingMessage.Substring(19, incomingMessage.Length - 19);
                        logs.AppendText(myMessage + "\n"); 
                    }
                    else if (incomingMessage.Length > 18 && incomingMessage.Substring(0, 19) == "EfeYarenYigitKaya*!")
                    {
                        int index = incomingMessage.IndexOf("%");
                        string sender = incomingMessage.Substring(19,index-19);
                        string myMessage = incomingMessage.Substring(index+1);
                        logs.AppendText(sender+": "+myMessage+"\n");
                    }
                    else if (incomingMessage.Length > 18 && incomingMessage.Substring(0, 20) == "EfeYarenYigitKaya*<f")
                    {
                        string deletedfriend = incomingMessage.Substring(20);
                        logs.AppendText("There is no such friend as " + deletedfriend + " in your friends list\n");
                    }
                    else if (incomingMessage.Length > 18 && incomingMessage.Substring(0, 20) == "EfeYarenYigitKaya*<s")
                    {
                        string deletedfriend = incomingMessage.Substring(20);
                        logs.AppendText("You have succesfully remove " + deletedfriend + " from your friends list\n");
                    }
                    else if(incomingMessage.Length > 18 && incomingMessage.Substring(0, 19) == "EfeYarenYigitKaya*<")
                    {
                        string clientName = incomingMessage.Substring(19);
                        logs.AppendText(clientName + " removed you from friends\n");
                    }
                    
                    else if (incomingMessage.Length > 18 && incomingMessage.Substring(0, 18) == "EfeYarenYigitKaya#") // once accept friend request
                    {
                        string invitee = incomingMessage.Substring(18, incomingMessage.Length - 18);
                        //friendReqList.AppendText(invitee + "\n");
                        logs.AppendText(invitee + " Accepted your friend request\n");
                    }
                    else if (incomingMessage.Length > 18 && incomingMessage.Substring(0, 18) == "EfeYarenYigitKaya&")
                    {
                        string invitee = incomingMessage.Substring(18, incomingMessage.Length - 18);
                        friendReqList.AppendText(invitee + "\n");
                    }
                    else if (incomingMessage.Length > 18 && incomingMessage.Substring(0, 19) == "EfeYarenYigitKaya*&")
                    {
                        string invitee = incomingMessage.Substring(19, incomingMessage.Length - 19);
                        logs.AppendText(invitee + "\n");

                    }
                   
                    else if (incomingMessage.Length > 17 && incomingMessage.Substring(0, 18) == "EfeYarenYigitKaya$") // receive friends from server
                    {
                        //FriendList.AppendText("print friend list");
                        FriendList.Clear();
                        string friends = incomingMessage.Substring(18, incomingMessage.Length - 18);
                        friends = friends.Replace(',', '\n');
                        FriendList.AppendText(friends);
                        //if(friends.Length > 1)
                        //FriendList.AppendText("********\n");
                    }
                    else if(incomingMessage.Substring(0,3) == "123")
                    {
                        logs.AppendText(incomingMessage.Substring(3)); // you have already requested and wait for answer
                    }
                    else if(incomingMessage.Substring(0,3) == "321")
                    {
                        logs.AppendText(incomingMessage.Substring(3)); // you requested but there is no such user in database
                    }
                    else
                    {
                        incomingMessage = incomingMessage.Substring(17, incomingMessage.Length - 34); //true connection and message
                        logs.AppendText(incomingMessage + "\n"); //print the incoming message
                    }
                        
                }
                catch(Exception e)
                {
                    //FriendList.AppendText(e.Message);
                    if (!terminating)
                    {
                        if (already_check)
                        {
                            logs.AppendText("The server has disconnected\n"); //disconnect button activated
                        }
                        server_check = false; 
                        already_check = true; // user can enter again
                        connect_button.Enabled = true; //user can connect again because disconnect now
                        textBox_message.Enabled = false; //user cannot write text
                        send_button.Enabled = false; //user cannot send message
                    }

                    clientSocket.Close();
                    connected = false;
                }

            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (server_check)
            {
                Byte[] buffer = new Byte[128];
                buffer = Encoding.Default.GetBytes(userName + "~~~"); //for understand the username which comes from server
           
                clientSocket.Send(buffer);
            }
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }
        private void send_button_Click(object sender, EventArgs e) 
        {
            string message = ("EfeYarenYigitKaya" + textBox_name.Text + "*"+ textBox_message.Text + "EfeYarenYigitKaya");
            if (message != "" && message.Length <= 128) //message length should be smaller than 128
            {
                Byte[] buffer = new Byte[128];
                buffer = Encoding.Default.GetBytes(message + "???");
                clientSocket.Send(buffer);
                logs.AppendText("You send a message\n");
            }
        }

        private void disconnect_button_Click(object sender, EventArgs e)
        {
            logs.AppendText("The client has disconnected\n");
            connected = false; 
            terminating = true;
            connect_button.Enabled = true; //already disconnected so user can connect
            disconnect_button.Enabled = false; //already disconnected, user cannot click again
            textBox_message.Enabled = false; //in disconnect user cannot write text
            send_button.Enabled = false; //user cannot send message in disconnection
            sendReqBut.Enabled = false;
            AcceptBut.Enabled = false;
            RejectBut.Enabled = false;
            friendsBut.Enabled = false;
            button1.Enabled = false;
            deleteButton.Enabled = false;
            Byte[] buffer = new Byte[128];
            buffer = Encoding.Default.GetBytes(userName + "~~~");
            clientSocket.Send(buffer);
            

            clientSocket.Close();
        }

        private void sendReqBut_Click(object sender, EventArgs e)
        {
            string invitee = sendName.Text;
            string friendReq = (textBox_name.Text + "*" + invitee + "£"); // serverda olmayan isim geldiğinde 
            if (friendReq.Length <= 128 && !requestList.Contains(invitee) && invitee != textBox_name.Text)
            {
                Byte[] buffer = new Byte[128];
                buffer = Encoding.Default.GetBytes(friendReq);
                clientSocket.Send(buffer);
                logs.AppendText("You send a friend request to " + invitee + "\n");
            }else
            {
                logs.AppendText("You cannot send a friend request\n");
            }
        }
        private void friendsBut_Click(object sender, EventArgs e)
        {
            string friendListReq = (textBox_name.Text + "$");
            Byte[] buffer = new Byte[128];
            buffer = Encoding.Default.GetBytes(friendListReq);
            clientSocket.Send(buffer);
            logs.AppendText("You send a request to see your friend list\n");
        }
        private void AcceptBut_Click(object sender, EventArgs e)
        {
            string inviter = WriteName.Text;
            string friendAcc = (inviter + "*" + textBox_name.Text + "#"); // serverda olmayan isim geldiğinde 
            if (requestList.Contains(inviter))
            {
                if (friendAcc.Length <= 128)
                {
                    Byte[] buffer = new Byte[128];
                    buffer = Encoding.Default.GetBytes(friendAcc);
                    clientSocket.Send(buffer);
                    logs.AppendText("You accepted the friend request of " + inviter + "\n");
                }
                requestList.Remove(inviter); // yeni ekledim
                friendReqList.Clear();
                string currentRequest = "";
                foreach (string name in requestList)
                {
                    currentRequest += name + "\n";
                }
                friendReqList.AppendText(currentRequest);
            }else
            {
                logs.AppendText("There is no such user in your request list\n");
            }
            

        }

        private void RejectBut_Click(object sender, EventArgs e)
        {
            string inviter = WriteName.Text;
            string friendRej = (inviter + "*" + textBox_name.Text + "&"); // serverda olmayan isim geldiğinde
            if (requestList.Contains(inviter))
            {
                if (friendRej.Length <= 128)
                {
                    Byte[] buffer = new Byte[128];
                    buffer = Encoding.Default.GetBytes(friendRej);
                    clientSocket.Send(buffer);
                    logs.AppendText("You rejected the friend request of " + inviter + "\n");
                }
                requestList.Remove(inviter); // yeni ekledim
                friendReqList.Clear();
                string currentRequest = "";
                foreach (string name in requestList)
                {
                    currentRequest += name + "\n";
                }
                friendReqList.AppendText(currentRequest);
            }else
            {
                logs.AppendText("There is no such user in your request list\n");
            } 
            
        }

        private void friendsBut_Click_1(object sender, EventArgs e)
        {
            string friendListReq = (textBox_name.Text + "$");
            Byte[] buffer = new Byte[128];
            buffer = Encoding.Default.GetBytes(friendListReq);
            clientSocket.Send(buffer);
            logs.AppendText("You send a request to see your friend list\n");
        }

        private void button1_Click(object sender, EventArgs e) // to send messages to friends
        {
            //string invitee = sendName.Text;
            string friendMessage = (textBox_name.Text + "*" + textBox_message.Text + "!");
            if (friendMessage.Length <= 128)
            {
                Byte[] buffer = new Byte[128];
                buffer = Encoding.Default.GetBytes(friendMessage);
                clientSocket.Send(buffer);
                logs.AppendText("You send a message only to your friends\n");
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string friendToBeDeleted = (textBox_name.Text + "*" + deleteFriend.Text + "<");
            if(friendToBeDeleted.Length <= 128)
            {
                Byte[] buffer = new Byte[128];
                buffer = Encoding.Default.GetBytes(friendToBeDeleted);
                clientSocket.Send(buffer);
                logs.AppendText("You wanted to delete " + deleteFriend.Text + " from your friends list\n");
            }
        }
    }
}
