namespace cs408
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.IP = new System.Windows.Forms.Label();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.Port = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.Label();
            this.connect_button = new System.Windows.Forms.Button();
            this.disconnect_button = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.message = new System.Windows.Forms.Label();
            this.textBox_message = new System.Windows.Forms.TextBox();
            this.send_button = new System.Windows.Forms.Button();
            this.sendName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sendReqBut = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.friendsBut = new System.Windows.Forms.Button();
            this.WriteName = new System.Windows.Forms.TextBox();
            this.FriendList = new System.Windows.Forms.RichTextBox();
            this.RejectBut = new System.Windows.Forms.Button();
            this.AcceptBut = new System.Windows.Forms.Button();
            this.friendReqList = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.deleteFriend = new System.Windows.Forms.TextBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IP
            // 
            this.IP.AutoSize = true;
            this.IP.Location = new System.Drawing.Point(20, 46);
            this.IP.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(20, 13);
            this.IP.TabIndex = 0;
            this.IP.Text = "IP:";
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(78, 46);
            this.textBox_ip.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(118, 20);
            this.textBox_ip.TabIndex = 1;
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(77, 80);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(119, 20);
            this.textBox_port.TabIndex = 2;
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(77, 113);
            this.textBox_name.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(119, 20);
            this.textBox_name.TabIndex = 3;
            // 
            // Port
            // 
            this.Port.AutoSize = true;
            this.Port.Location = new System.Drawing.Point(20, 80);
            this.Port.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(29, 13);
            this.Port.TabIndex = 4;
            this.Port.Text = "Port:";
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Location = new System.Drawing.Point(20, 117);
            this.name.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(38, 13);
            this.name.TabIndex = 5;
            this.name.Text = "Name:";
            // 
            // connect_button
            // 
            this.connect_button.Location = new System.Drawing.Point(22, 144);
            this.connect_button.Margin = new System.Windows.Forms.Padding(2);
            this.connect_button.Name = "connect_button";
            this.connect_button.Size = new System.Drawing.Size(173, 28);
            this.connect_button.TabIndex = 6;
            this.connect_button.Text = "Connect";
            this.connect_button.UseVisualStyleBackColor = true;
            this.connect_button.Click += new System.EventHandler(this.connect_button_Click);
            // 
            // disconnect_button
            // 
            this.disconnect_button.Enabled = false;
            this.disconnect_button.Location = new System.Drawing.Point(22, 176);
            this.disconnect_button.Margin = new System.Windows.Forms.Padding(2);
            this.disconnect_button.Name = "disconnect_button";
            this.disconnect_button.Size = new System.Drawing.Size(173, 27);
            this.disconnect_button.TabIndex = 7;
            this.disconnect_button.Text = "Disconnect";
            this.disconnect_button.UseVisualStyleBackColor = true;
            this.disconnect_button.Click += new System.EventHandler(this.disconnect_button_Click);
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(214, 33);
            this.logs.Margin = new System.Windows.Forms.Padding(2);
            this.logs.Name = "logs";
            this.logs.Size = new System.Drawing.Size(161, 334);
            this.logs.TabIndex = 8;
            this.logs.Text = "";
            // 
            // message
            // 
            this.message.AutoSize = true;
            this.message.Location = new System.Drawing.Point(20, 214);
            this.message.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(53, 13);
            this.message.TabIndex = 9;
            this.message.Text = "Message:";
            // 
            // textBox_message
            // 
            this.textBox_message.Location = new System.Drawing.Point(78, 214);
            this.textBox_message.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_message.Multiline = true;
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.Size = new System.Drawing.Size(118, 97);
            this.textBox_message.TabIndex = 10;
            // 
            // send_button
            // 
            this.send_button.Enabled = false;
            this.send_button.Location = new System.Drawing.Point(37, 329);
            this.send_button.Margin = new System.Windows.Forms.Padding(2);
            this.send_button.Name = "send_button";
            this.send_button.Size = new System.Drawing.Size(68, 22);
            this.send_button.TabIndex = 11;
            this.send_button.Text = "Send";
            this.send_button.UseVisualStyleBackColor = true;
            this.send_button.Click += new System.EventHandler(this.send_button_Click);
            // 
            // sendName
            // 
            this.sendName.Location = new System.Drawing.Point(395, 68);
            this.sendName.Margin = new System.Windows.Forms.Padding(2);
            this.sendName.Name = "sendName";
            this.sendName.Size = new System.Drawing.Size(99, 20);
            this.sendName.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(393, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Send Friend Request:";
            // 
            // sendReqBut
            // 
            this.sendReqBut.Enabled = false;
            this.sendReqBut.Location = new System.Drawing.Point(395, 106);
            this.sendReqBut.Margin = new System.Windows.Forms.Padding(2);
            this.sendReqBut.Name = "sendReqBut";
            this.sendReqBut.Size = new System.Drawing.Size(98, 25);
            this.sendReqBut.TabIndex = 16;
            this.sendReqBut.Text = "Send";
            this.sendReqBut.UseVisualStyleBackColor = true;
            this.sendReqBut.Click += new System.EventHandler(this.sendReqBut_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(412, 133);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "Write Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(410, 217);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Friend Requests";
            // 
            // friendsBut
            // 
            this.friendsBut.Enabled = false;
            this.friendsBut.Location = new System.Drawing.Point(398, 356);
            this.friendsBut.Margin = new System.Windows.Forms.Padding(2);
            this.friendsBut.Name = "friendsBut";
            this.friendsBut.Size = new System.Drawing.Size(98, 27);
            this.friendsBut.TabIndex = 31;
            this.friendsBut.Text = "Friends";
            this.friendsBut.UseVisualStyleBackColor = true;
            this.friendsBut.Click += new System.EventHandler(this.friendsBut_Click_1);
            // 
            // WriteName
            // 
            this.WriteName.Location = new System.Drawing.Point(398, 154);
            this.WriteName.Margin = new System.Windows.Forms.Padding(2);
            this.WriteName.Name = "WriteName";
            this.WriteName.Size = new System.Drawing.Size(99, 20);
            this.WriteName.TabIndex = 30;
            // 
            // FriendList
            // 
            this.FriendList.Location = new System.Drawing.Point(554, 33);
            this.FriendList.Margin = new System.Windows.Forms.Padding(2);
            this.FriendList.Name = "FriendList";
            this.FriendList.Size = new System.Drawing.Size(114, 334);
            this.FriendList.TabIndex = 29;
            this.FriendList.Text = "";
            // 
            // RejectBut
            // 
            this.RejectBut.Enabled = false;
            this.RejectBut.Location = new System.Drawing.Point(448, 177);
            this.RejectBut.Margin = new System.Windows.Forms.Padding(2);
            this.RejectBut.Name = "RejectBut";
            this.RejectBut.Size = new System.Drawing.Size(47, 34);
            this.RejectBut.TabIndex = 28;
            this.RejectBut.Text = "Reject";
            this.RejectBut.UseVisualStyleBackColor = true;
            this.RejectBut.Click += new System.EventHandler(this.RejectBut_Click);
            // 
            // AcceptBut
            // 
            this.AcceptBut.Enabled = false;
            this.AcceptBut.Location = new System.Drawing.Point(398, 176);
            this.AcceptBut.Margin = new System.Windows.Forms.Padding(2);
            this.AcceptBut.Name = "AcceptBut";
            this.AcceptBut.Size = new System.Drawing.Size(44, 35);
            this.AcceptBut.TabIndex = 27;
            this.AcceptBut.Text = "Accept";
            this.AcceptBut.UseVisualStyleBackColor = true;
            this.AcceptBut.Click += new System.EventHandler(this.AcceptBut_Click);
            // 
            // friendReqList
            // 
            this.friendReqList.Location = new System.Drawing.Point(395, 244);
            this.friendReqList.Margin = new System.Windows.Forms.Padding(2);
            this.friendReqList.Name = "friendReqList";
            this.friendReqList.Size = new System.Drawing.Size(99, 108);
            this.friendReqList.TabIndex = 26;
            this.friendReqList.Text = "";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(121, 329);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 34;
            this.button1.Text = "sendFriend";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(700, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Delete Friend:";
            // 
            // deleteFriend
            // 
            this.deleteFriend.Location = new System.Drawing.Point(689, 68);
            this.deleteFriend.Name = "deleteFriend";
            this.deleteFriend.Size = new System.Drawing.Size(100, 20);
            this.deleteFriend.TabIndex = 36;
            // 
            // deleteButton
            // 
            this.deleteButton.Enabled = false;
            this.deleteButton.Location = new System.Drawing.Point(698, 106);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 37;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 387);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.deleteFriend);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.friendsBut);
            this.Controls.Add(this.WriteName);
            this.Controls.Add(this.FriendList);
            this.Controls.Add(this.RejectBut);
            this.Controls.Add(this.AcceptBut);
            this.Controls.Add(this.friendReqList);
            this.Controls.Add(this.sendReqBut);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendName);
            this.Controls.Add(this.send_button);
            this.Controls.Add(this.textBox_message);
            this.Controls.Add(this.message);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.disconnect_button);
            this.Controls.Add(this.connect_button);
            this.Controls.Add(this.name);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_ip);
            this.Controls.Add(this.IP);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label IP;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label Port;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Button connect_button;
        private System.Windows.Forms.Button disconnect_button;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.Label message;
        private System.Windows.Forms.TextBox textBox_message;
        private System.Windows.Forms.Button send_button;
        private System.Windows.Forms.TextBox sendName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button sendReqBut;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button friendsBut;
        private System.Windows.Forms.TextBox WriteName;
        private System.Windows.Forms.RichTextBox FriendList;
        private System.Windows.Forms.Button RejectBut;
        private System.Windows.Forms.Button AcceptBut;
        private System.Windows.Forms.RichTextBox friendReqList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox deleteFriend;
        private System.Windows.Forms.Button deleteButton;
    }
}

